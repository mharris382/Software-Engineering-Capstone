using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
namespace AI
{
    public class SmartEnemyAI : MonoBehaviour
    {
        public DistanceFunction fearOverDistance;

        [Tooltip("c(1)>distance==maximum\nc(1)>distance==0")]
        AnimationCurve cfearOverDistance = AnimationCurve.Linear(0, 0, 1, 1);


        float GetFearIntensity(float distance, Vector2 fearRange)
        {
            float t = MathUtils.InverseLerpRadial(distance, fearRange.x, fearRange.y);
            t = cfearOverDistance.Evaluate(t);
            throw new NotImplementedException();
        }
    }


    [Serializable]
    public class DistanceFunction
    {
        public float minimumDistance = 1;
        public float maximumDistance = 10;


        public void DrawHandles(Vector2 center)
        {
#if UNITY_EDITOR

            float newMin = minimumDistance, newMax = maximumDistance;
            if (DrawRadiusHandles(center, ref newMin))
            {
                var changeMin = newMin - minimumDistance;
                maximumDistance += changeMin;
            }
            else if (DrawRadiusHandles(center, ref newMax))
            {
                var changeMax = newMax - maximumDistance;
                minimumDistance += changeMax;
            }

#endif
        }

        bool DrawRadiusHandles(Vector2 center, ref float r)
        {
            var size = HandleUtility.GetHandleSize(center) * 0.04f;

            float drawR = Mathf.Max(0.125f, r);
            Handles.DrawWireDisc(center, Vector3.forward, r);
            Vector3[] points = new[]
            {
                Vector3.down * drawR,
                Vector3.up * drawR,
                Vector3.right * drawR,
                Vector3.left * drawR,
            };


            foreach (var point in points)
            {
                var newPoint = Handles.FreeMoveHandle(point, Quaternion.identity, size, Event.current.control ? Vector3.one * 0.25f : Vector3.zero,
                    Handles.DotHandleCap);
                newPoint.z = 0;
                var d = Vector2.Distance(point, newPoint);
                if (d > Mathf.Epsilon)
                {
                    r = d;
                    return true;
                }
            }

            return false;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(SmartEnemyAI))]
    public class SmartEnemyEditor : Editor
    {
        private void OnSceneGUI()
        {
            serializedObject.Update();
            var smartEnemy = target as SmartEnemyAI;
            var dProp = serializedObject.FindProperty("fearOverDistance");
            var minProp = dProp.FindPropertyRelative("minimumDistance");
            var maxProp = dProp.FindPropertyRelative("maximumDistance");

            float min = minProp.floatValue;
            float max = maxProp.floatValue;

            const float absoluteMin = 0.125f;
            const float absoluteMax = 100;


            var m1 = Mathf.Min(min, max, absoluteMin);
            var m2 = Mathf.Max(min, max, absoluteMax);
            
            max = m1;
            min = m2;
            var center = smartEnemy.transform.position;
           
            bool dirty = false;
            min = minProp.floatValue;
            max = maxProp.floatValue;
            Handles.color = Color.green;
            Handles.DrawWireDisc(center, Vector3.forward, min);
            if (DrawX(smartEnemy.transform, Vector2.right, smartEnemy.transform.position, 1, ref min) ||
                                               DrawX(smartEnemy.transform, Vector2.left, smartEnemy.transform.position, -1, ref min) ||
                                               DrawX(smartEnemy.transform, Vector2.up, smartEnemy.transform.position, 1, ref min) ||
                                               DrawX(smartEnemy.transform,Vector2.down, smartEnemy.transform.position, -1, ref min))
            {
                minProp.floatValue = min;
                
                serializedObject.ApplyModifiedProperties();
            }
            Handles.color = Color.red;
            Handles.DrawWireDisc(center, Vector3.forward, max);
            if (DrawX(smartEnemy.transform, Vector2.right, smartEnemy.transform.position, 1, ref max) ||
                DrawX(smartEnemy.transform, Vector2.left, smartEnemy.transform.position, -1, ref max) ||
                DrawX(smartEnemy.transform, Vector2.up, smartEnemy.transform.position, 1, ref max) ||
                DrawX(smartEnemy.transform,Vector2.down, smartEnemy.transform.position, -1, ref max))
            {
                maxProp.floatValue = max;
            }
            serializedObject.ApplyModifiedProperties();
        }


        public void DrawHandles(Vector2 center, ref float minimumDistance, ref float maximumDistance)
        {
#if UNITY_EDITOR

            float newMin = minimumDistance, newMax = maximumDistance;
            if (DrawRadiusHandles(center, ref newMin))
            {
                var changeMin = newMin - minimumDistance;
                maximumDistance += changeMin;
            }
            else if (DrawRadiusHandles(center, ref newMax))
            {
                var changeMax = newMax - maximumDistance;
                minimumDistance += changeMax;
            }

#endif
        }

        bool DrawX(Transform t, Vector2 direction, Vector2 center, float dir, ref float dis)
        {

            var l2 =  center + direction * (dir * dis);
            var l1 =  center;
            var nP = Handles.FreeMoveHandle(l2, Quaternion.identity, .125f, Vector3.zero, Handles.DotHandleCap);
            var d =Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ?  nP.x - l2.x : nP.y - l2.y;
            if (Mathf.Abs(d) > 0)
            {
                dis = Mathf.Abs(d);
                return true;
            }
            return false;
            //l2 += pos; l1 += pos;
        }



        bool DrawRadiusHandles(Vector2 center, ref float r)
        {
            var size = HandleUtility.GetHandleSize(center) * 0.04f;

            float drawR = Mathf.Max(0.125f, r);
            Handles.DrawWireDisc(center, Vector3.forward, r);
            Vector3[] points = new[]
            {
                Vector3.down * drawR,
                Vector3.up * drawR,
                Vector3.right * drawR,
                Vector3.left * drawR,
            };


            foreach (var point in points)
            {
                var newPoint = Handles.FreeMoveHandle(point, Quaternion.identity, size, Event.current.control ? Vector3.one * 0.25f : Vector3.zero,
                    Handles.DotHandleCap);

                newPoint.z = 0;
                var d = Vector2.Distance(point, newPoint);
                if (d > Mathf.Epsilon)
                {
                    r = d;
                    return true;
                }
            }

            return false;
        }
    }
#endif
    public static class MathUtils
    {
        /// <summary>
        /// clamped inverse <see cref="Mathf.InverseLerp"/>
        /// </summary>
        /// <param name="distance">the point within the range you want to calculate</param>
        /// <param name="lowerBound">the lower bound of the range</param>
        /// <param name="upperBound">the upper bound of the range</param>
        /// <returns></returns>
        public static float InverseLerpRadial(float distance, float lowerBound, float upperBound)
        {
            ///distance = Mathf.Clamp(distance, Mathf.Min(lowerBound, upperBound), Mathf.Max(lowerBound, upperBound));
            float t = Mathf.InverseLerp(lowerBound, upperBound, distance);
            return t;
        }


        /// <summary>
        /// returns normalized distance between 0 and 1, where 0 means the distance =lowerBound, and where 1 means the distance == upperBound
        /// </summary>
        /// <param name="position">the object from which distance the is measured </param>
        /// <param name="center">the center point of the radius</param>
        /// <param name="lowerBound">0 means the distance =lowerBound</param>
        /// <param name="upperBound"> 1 means the distance == upperBound</param>
        /// <returns></returns>
        public static float InverseLerpRadial(Vector2 position, Vector2 center, float lowerBound, float upperBound)
        {
            return InverseLerpRadial(Vector2.Distance(center, position), lowerBound, upperBound);
        }
    }
}