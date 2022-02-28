using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManaFinder
{
    IEnumerable<Mana> GetManaNearby(Element e);
}
public class ManaFinder : MonoBehaviour, IManaFinder
{
    Dictionary<Element, List<Mana>> foundMana;

    public void Awake()
    {
        foundMana = new Dictionary<Element, List<Mana>>();
        foundMana.Add(Element.Air, new List<Mana>());
        foundMana.Add(Element.Earth, new List<Mana>());
        foundMana.Add(Element.Fire, new List<Mana>());
        foundMana.Add(Element.Water, new List<Mana>());
        foundMana.Add(Element.Thunder, new List<Mana>());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var mana = col.GetComponent<Mana>();
        if (mana != null)
        {
            foundMana[mana.element].Add(mana);
            Debug.Log("I found the mana!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var mana = other.GetComponent<Mana>();
        if (mana != null)
        {
            foundMana[mana.element].Remove(mana);
        }
    }
    
    public IEnumerable<Mana> GetManaNearby(Element e)
    {
        var nearbyManas = foundMana[e];
        foreach(var mana in nearbyManas)
        {
            if(mana != null){
                yield return mana;
            }
        }
    }
}
