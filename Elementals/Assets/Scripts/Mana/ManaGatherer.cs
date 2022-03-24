using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ManaGatherer : MonoBehaviour
{
   public ElementContainer element;
   [SerializeField] private float consumeRadius = 1;
   [SerializeField] private float forceStrength = 5;
   public Events manaGatherEvents;


   public float ConsumeRadius
   {
      get => consumeRadius;
      set => consumeRadius = value;
   }

   public float ForceStrength
   {
      get => forceStrength;
      set => forceStrength = value;
   }

   private IManaSource _manaSource;
   private CasterState _state;

   private bool _gathering;

   public IManaFinder Finder { get; set; }
   private bool Gathering
   {
      get => _gathering;
      set
      {
         if (_gathering != value)
         {
            _gathering = value;
            if(_gathering)manaGatherEvents.onStartGathering?.Invoke();
            else manaGatherEvents.onStopGathering?.Invoke();
         }
      }
   }

   private void Awake()
   {
      Finder = GetComponentInChildren<IManaFinder>();
      _manaSource = GetComponentInChildren<IManaSource>();
      _state = GetComponent<CasterState>();
   }

   private void Update()
   {
      Gathering = _state.Gathering;//this invokes the start/stop gathering events whenever the gathering state changes 
      
      if (!Gathering) 
         return;
      GatherMana();
   }

   private void GatherMana()
   {
      var nearbyMana = Finder.GetManaNearby(element.Element).ToList();
      var closeRadius = this.consumeRadius;

      for (int i = 0; i < nearbyMana.Count; i++)
      {
         var mana = nearbyMana[i];

         if (IsInsideRadius(mana, closeRadius))
         {
            if (_manaSource.CurrentValue < _manaSource.MaxValue)
            {
               ConsumeMana(mana);
            }
         }
         else
         {
            ForcePullManaPickup(mana);
         }
      }
   }


   private void ConsumeMana(Mana mana)
   {
      _manaSource.AddMana(1);
      manaGatherEvents?.onManaGathered?.Invoke(element.Element);
      Destroy(mana.gameObject);
   }
   private void ForcePullManaPickup(Mana mana)
   {
      var rb = mana.GetComponent<Rigidbody2D>();
      var dir = ((Vector2) transform.position - rb.position).normalized;
      var force = dir * forceStrength;
      rb.AddForce(force);
   }

   private bool IsInsideRadius(Mana mana, float radius)
   {
      var dist = Vector2.Distance(mana.transform.position, transform.position);
      return dist < radius;
   }

   
   [Serializable]
   public class Events
   {
      public UnityEvent onStartGathering;
      public UnityEvent onStopGathering;
      public UnityEvent<Element> onManaGathered;
   }

}