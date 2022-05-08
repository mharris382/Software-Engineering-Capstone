using System;
using UnityEngine;
#if UNITY_EDITOR
using System.Text;
using UnityEditor;
// ReSharper disable InconsistentNaming

#endif
namespace AI
{
    public class AIZone : MonoBehaviour
    {
        public ZonePriority priority = ZonePriority.DisengagePriority;

        [Tooltip("ZONE PRIORITY 1: if the player enters this zone, the enemy will attack the player regardless of how far away the player is")]
        public EngagementZone hostileZone;

        [Tooltip("ZONE PRIORITY 3: if the player is in this zone the AI will attack if the player is close enough to the enemy")]
        public EngagementZone aggressiveZone;
        
        
        [Tooltip("ZONE PRIORITY 2: if the player is in this zone the AI will stop attacking the player and flee")]
        public EngagementZone disengageZone;



        
        
        public bool debug;

        #region [Enums]

    
        public enum ZonePriority
        {
            HostilePriority,
            DisengagePriority
        }

        public enum ZoneState
        {
            Not_In_Zones,
            In_Hostile_Zone,
            In_Aggressive_Zone,
            In_Disengage_Zone
        }

        #endregion


        private ZoneState _lastZoneState;
        public event Action<ZoneState> playerChangedZones;

        public ZoneState PlayerZoneState
        {
            set
            {
                if (value != _lastZoneState)
                {
                    _lastZoneState = value;
                    playerChangedZones?.Invoke(value);
                }
            }
            get => _lastZoneState;
        }

        private ZoneTimestamp hostileTimestamp, disengageTimestamp, aggressiveTimestamp;

        public bool IsInHostileZone => _target != null && hostileZone.IsTargetInZone;
        public bool IsInAggroZone => _target != null && aggressiveZone.IsTargetInZone;
        public bool IsInDisengageZone => _target != null && disengageZone.IsTargetInZone;

        private Transform _target;
        
        public Transform Target
        {
            set => _target = value;
            get => _target;
        }

        private void Awake()
        {
            ResetTimestamps();
            if (debug)
            {
                playerChangedZones += state =>
                {
                    Debug.Log($"Player zone state = <b>{state}</b>");
                };
            }
        }

        private void OnEnable()
        {
            ResetZones(true);
        }

        private void OnDisable()
        {
            ResetZones();
        }

        private void Update()
        {
            if (_target == null)
            {
                ResetZones();
                return;
            }
            switch (priority)
            {
                case ZonePriority.HostilePriority:
                    if(CheckEngageZone()) return;
                    if(CheckDisengageZone()) return;
                    break;
                case ZonePriority.DisengagePriority:
                    if(CheckDisengageZone()) return;
                    if (CheckEngageZone()) return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (CheckAggressiveZone()) return;
            PlayerZoneState = ZoneState.Not_In_Zones;
        }

        private void LateUpdate()
        {
            if (_target == null)return;
            hostileZone.Tick(Target);
            aggressiveZone.Tick(Target);
            disengageZone.Tick(Target);
        }


        private void ResetZones(bool suppressEvent = false)
        {
            if (!suppressEvent) PlayerZoneState = ZoneState.Not_In_Zones;
            else _lastZoneState = ZoneState.Not_In_Zones;
            hostileZone.Reset();
            aggressiveZone.Reset();
            disengageZone.Reset();
        }

        private void ResetTimestamps()
        {
            hostileTimestamp = new ZoneTimestamp(this, ZoneState.In_Hostile_Zone, true);
            disengageTimestamp = new ZoneTimestamp(this, ZoneState.In_Disengage_Zone, true);
            aggressiveTimestamp = new ZoneTimestamp(this, ZoneState.In_Aggressive_Zone, true);
        }
        
        private bool CheckAggressiveZone()
        {
            if (aggressiveZone.IsTargetInZone)
            {
                PlayerZoneState = ZoneState.In_Aggressive_Zone;
                return true;
            }
            return false;
        }

        private bool CheckEngageZone()
        {
            if (hostileZone.IsTargetInZone)
            {
                PlayerZoneState = ZoneState.In_Hostile_Zone;
               
                return true;
            }

            return false;
        }

        private bool CheckDisengageZone()
        {
            if (hostileZone.IsTargetInZone)
            {
                PlayerZoneState = ZoneState.In_Disengage_Zone;
                return true;
            }
            return false;
        }

        #region [Time Helper Class]

        class ZoneTimestamp
        {
            public int targetStateCount;
            public float timeEnteredTargetState;
            public float timeExitedTargetState;
            public bool isInTargetState;


            public float TimeSinceExitedZone => isInTargetState ? 0 : Time.time - timeExitedTargetState;
            public float TimeSinceEnteredZone => !isInTargetState ? 0 : Time.time - timeEnteredTargetState;
        
            public ZoneTimestamp(AIZone zone, ZoneState targetState, bool debug =false)
            {
                targetStateCount = 0;
                zone.playerChangedZones += state =>
                {
                    if (state == targetState)
                    {
                        if (!isInTargetState)
                        {
                            if(debug) Debug.Log($"Entered Target State: {targetState}");
                            isInTargetState = true;
                            timeEnteredTargetState = Time.time;
                        }
                    }
                    else
                    {
                        if (!isInTargetState) return;
                        if(debug) Debug.Log($"Exited Target State: {targetState}");
                        timeExitedTargetState = Time.time;
                        isInTargetState = false;
                    }
                };
            }
        }
        

        #endregion
        
        #region [EDITOR GIZMOS]

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            var hostileColor = Color.red;
            var aggressiveColor = Color.yellow;
            var disengageColor = Color.green;

            hostileZone.DrawGizmos(hostileColor);
            aggressiveZone.DrawGizmos(aggressiveColor);
            disengageZone.DrawGizmos(disengageColor);
        }
#endif

        #endregion
    }


    #region [CUSTOM EDITOR HANDLES]

#if UNITY_EDITOR
    [CustomEditor(typeof(AIZone))]
    public class AIZoneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
           
            string label = "";
            var zone = target as AIZone;
            var aggZone = zone.aggressiveZone;
            var hosZone = zone.hostileZone;
            var safeZone = zone.disengageZone;

            StringBuilder sb = new StringBuilder();

            bool foundError = false;
            AddZoneLabel(sb, "Safe Zone",safeZone, ref foundError);
            AddZoneLabel(sb, "Guard Zone",aggZone, ref foundError);
            AddZoneLabel(sb, "Wall", hosZone,ref foundError);


            void AddZoneLabel(StringBuilder msb, string zoneName, EngagementZone ez, ref bool hasError)
            {
                if (ez.zoneType == EngagementZone.ZoneType.Trigger)
                {
                    if(ez.triggerArea != null)
                        msb.AppendLine($"Custom {zoneName}: {ez.triggerArea.name}");
                    else
                    {
                        hasError = true;
                        msb.AppendLine($"WARNING! {zoneName} is missing trigger area!");
                    }
                }
                else
                {
                    if (ez.zoneType == EngagementZone.ZoneType.VerticalPlane)
                    {
                        string h = ez.negativePlane ? "Down" : "Up";
                        msb.AppendLine($"{zoneName} {h}");
                    }
                    else
                    {
                        string v = ez.negativePlane ? "Left" : "Right";
                        msb.AppendLine($"{zoneName} {v}");
                    }
                }
            }

            label = sb.ToString();
            EditorGUILayout.HelpBox(label, foundError ? MessageType.Error : MessageType.None);
            
            EditorGUILayout.Space(5);
            base.OnInspectorGUI();
            /*
             *  Hostile zone purpose: prevents player from entering this area before killing all enemies
             *  Aggro zone purpose: increases pressure on player
             *  Safe Zone purpose: prevents player from exploiting slow  enemies by leading the enemies endlessly, also allows designers to construct zones to
             *  avoid enemies of different elements fighting player at the same tie and damaging each other accidentally
             *
             *  Zone has intended Directional flow Safe to Aggressive to Hostile
             *
             * - hostile can be thought of as a wall or a gate
             * - aggro can be thought of as the area the ai is guarding
             * - safe can be thought of as the area the ai avoids
             */
        }

        private void OnSceneGUI()
        {
            var hostileColor = Color.red;
            var aggressiveColor = Color.yellow;
            var disengageColor = Color.green;

            var zone = target as AIZone;
            var z1 = zone.aggressiveZone;
            Handles.color = hostileColor;
            DrawZoneHandle(zone.hostileZone, hostileColor);
            Handles.color = aggressiveColor;
            DrawZoneHandle(zone.aggressiveZone, aggressiveColor);
            Handles.color = disengageColor;
            DrawZoneHandle(zone.disengageZone, disengageColor);
            var z2 = zone.disengageZone;
            var z3 = zone.hostileZone;
        }

        private void DrawZoneHandle(EngagementZone zone, Color c)
        {
            if (zone.zoneType == EngagementZone.ZoneType.Trigger) return;

            var position = zone.planeOrigin;
            var handleSize = HandleUtility.GetHandleSize(zone.planeOrigin) * 0.08f;
            var newPos = (Vector2) Handles.FreeMoveHandle(position, Quaternion.identity, handleSize, Vector3.zero, Handles.DotHandleCap);
            if (newPos != position)
            {
                Undo.RecordObject(target, "Moved Plane Origin");
                zone.planeOrigin = newPos;
            }
        }
    }
#endif

    #endregion
}