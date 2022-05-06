using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ManaSystem
{
    /// <summary>
    /// Service provider singleton object which allows objects to request that mana of a specific element be dropped at the requested location <see> <cref>ManaDropper.DropMana</cref> </see>
    /// the ManaDropper internally handles all the logic involved spawning mana objects  
    /// </summary>
    public class ManaDropper : MonoBehaviour
    {
        [Tooltip("Maximum mana that can be dropped at once")]
        [SerializeField] private int maxDropAmount = 200; 
        [Tooltip("Drop Info that is used to spawn mana when the caller does not specify the parameters of the mana drop")]
        [SerializeField]
        private ManaDropInfo defaultDrop = new ManaDropInfo()
        {
            radius = new RangeF(0,1),
            dropAmount = new RangeI(3,6),
            angle = new RangeF(0,360)
        };
        
        
        private static ManaDropper _instance;


        private Dictionary<Element, Mana> _manaDropPrefabs = new Dictionary<Element, Mana>();


        private Vector2[] _dropPoints;

        private Transform _dropParent;

        public static ManaDropper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ManaDropper>();
                    if (_instance == null)
                    {
                        var go = new GameObject("<<Mana Dropper>>");
                        _instance = go.AddComponent<ManaDropper>();
                    }
                }
                return _instance;
            }
        }

        public Transform DropParent
        {
            get
            {
                if (_dropParent == null)
                {
                    _dropParent = new GameObject("__ManaDrops(DYNAMIC)__").transform;
                }
                return _dropParent;
            }
        }
        
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _dropPoints = new Vector2[maxDropAmount];
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start() => LoadManaPrefabs();

        private void OnDestroy()
        {
            DeSpawnAllPickups();
            if (_dropParent != null) GameObject.Destroy(_dropParent.gameObject);
            
            _dropParent = null;
            _dropPoints = null;
            _manaDropPrefabs = null;
            _instance = null;
        }

        Mana GetElementPrefab(Element e)
        {
            if(!_manaDropPrefabs.ContainsKey(e))
                LoadManaPrefabs();
            Debug.Assert(_manaDropPrefabs.ContainsKey(e));
            Debug.Assert(_manaDropPrefabs[e]!=null);
            return _manaDropPrefabs[e];
        }

        private void DeSpawnAllPickups()
        {
            var list = new List<GameObject>();
            for (int i = 0; i < DropParent.childCount; i++)
            {
                list.Add(DropParent.GetChild(i).gameObject);
            }
            foreach (var drop in list) Destroy(drop);

        }

        //TODO: should only need to call this once per game, but ManaSystem shouldn't be DoNotDestroyOnLoad, solution is to move loading into separate class
        private void LoadManaPrefabs()
        {
            var path = "ManaPrefabs";
            var prefabs = Resources.LoadAll<GameObject>(path);

            Mana GetPrefab(Element e)
            {
                var name = $"ManaObject_{e.ToString()}";
                var go = prefabs.FirstOrDefault(t => t.name == name);
                if (go == null)
                {
                    Debug.LogError($"Couldn't find mana prefab in Resources/ManaPrefabs/{name}");
                    throw new NullReferenceException("Missing Mana Prefab!");
                }
                var mana =go.GetComponent<Mana>(); 
                if(mana == null)throw new MissingComponentException($"Prefab {name} is missing Mana Component!");
                Debug.Assert(mana.element == e, $"Element Mismatch, name {name} expected {e} but found element {mana.element}", mana);
                return mana;
            }

            void AddElementPrefabToDictionary(Element e)
            {
                if (_manaDropPrefabs.ContainsKey(e))
                {
                    if (_manaDropPrefabs[e] == null) _manaDropPrefabs.Remove(e);
                    else return;
                }
                _manaDropPrefabs.Add(e, GetPrefab(e));
            }
            AddElementPrefabToDictionary(Element.Air);
            AddElementPrefabToDictionary(Element.Water);
            AddElementPrefabToDictionary(Element.Fire);
            AddElementPrefabToDictionary(Element.Earth);
            AddElementPrefabToDictionary(Element.Thunder);
        }



        public static void DropMana(Element element, Vector2 position, ManaDropInfo dropInfo)
        {
            if (dropInfo.dropPosition != position)
                dropInfo = dropInfo.ToPosition(position);
            
            var dropPoints = Instance._dropPoints;
            var dropCount = dropInfo.GetDropsNonAlloc(dropPoints);
            var prefab = Instance.GetElementPrefab(element);
            
            for (int i = 0; i < dropCount; i++)
            {
                Instantiate(prefab, dropPoints[i], Quaternion.identity, Instance.DropParent);
            }
        }
        
        public static void DropMana(Element element, Vector2 position)
        {
            var dropInfo = Instance.defaultDrop.ToPosition(position);
           DropMana(element, position, dropInfo);
        }

        public static void DropMana(Element element, Vector2 position, RangeI dropAmountRange) => DropMana(element, position, dropAmountRange.min, dropAmountRange.max);

        public static void DropMana(Element element, Vector2 position, int amountMin, int amountMax)
        {
            var dropInfo = Instance.defaultDrop.ToPosition(position);
            dropInfo.MinDropAmount = amountMin;
            dropInfo.MaxDropAmount = amountMax;
            DropMana(element, position, dropInfo);
        }
    }

    [Serializable]
    public struct RangeI
    {
        public int min;
        public int max;

        public int GetRand() => UnityEngine.Random.Range(min, max + 1);
        public RangeI(int value) : this(value, value) { }
        public RangeI(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }

    [Serializable]
    public struct RangeF
    {
        public float min;
        public float max;

        public float GetRand() => UnityEngine.Random.Range(min, max+1);

        public RangeF(float value) :this(value,value) { }
        public RangeF(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
    
}


public class MinMaxRangeAttribute : PropertyAttribute
{
    public float min, max;

    public MinMaxRangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}