using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaGatherer : MonoBehaviour
{
   [SerializeField] private float consumeRadius = 1;
   [SerializeField] private float forceStrength = 5;
   public IManaFinder Finder { get; set; }
   public ElementContainer element;
   private ManaState _mana;
   private CasterState _state;

   private void Awake()
   {
      Finder = GetComponent<IManaFinder>();
      _mana = GetComponentInChildren<ManaState>();  
      _state = GetComponent<CasterState>();

      if (_state.Gathering)
      {
         var nearbyMana = Finder.GetManaNearby(element.Element);
         var closeRadius = gameObject.AddComponent<ManaGatherer>();
         foreach (var mana in nearbyMana)
         {
            //TODO: check if we are close enough to pick it up
            if (IsInsideRadius(mana, closeRadius.consumeRadius))
            {
               Debug.Log("..........Yep we can get it.....");
               ConsumeMana(mana);
            }
            else  
            {
               //TODO: otherwise force pull it
               ForcePullManaPickup(mana);
               ConsumeMana(mana);
            }
            
            
         }
      }
   }



   private void ConsumeMana(Mana mana)
   {
      //TODO: add mana to state
      _mana.CurrentValue += 1;
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