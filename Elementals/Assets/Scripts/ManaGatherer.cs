using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaGatherer : MonoBehaviour
{
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
         
      }
   }
}