using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaGatherer : MonoBehaviour
{
   [SerializeField] private float consumeRadius = 1;
   [SerializeField] private float forceStrength = 5;
   public IManaFinder Finder { get; set; }
   public ElementContainer element;
   private IManaSource _manaSource;
   private CasterState _state;
   
   
   private void Awake()
   {
      Finder = GetComponentInChildren<IManaFinder>();
      _manaSource = GetComponentInChildren<IManaSource>();
      _state = GetComponent<CasterState>();
   }

   private void Update()
   {
      if (_state.Gathering)
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
   }
   
   
   private void ConsumeMana(Mana mana)
   {
      _manaSource.AddMana(1);
      //TODO: add a consumption event to trigger pickup feedback FX
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

}