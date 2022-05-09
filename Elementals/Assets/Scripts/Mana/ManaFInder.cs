using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IManaFinder
{
    IEnumerable<Mana.Mana> GetManaNearby(Element e);
}
public class ManaFInder : MonoBehaviour, IManaFinder
{
    Dictionary<Element, List<Mana.Mana>> foundMana;
    [Multiline(4)]
   [SerializeField] private string json;
    public void Awake()
    {
        foundMana = new Dictionary<Element, List<Mana.Mana>>();
        foundMana.Add(Element.Air, new List<Mana.Mana>());
        foundMana.Add(Element.Earth, new List<Mana.Mana>());
        foundMana.Add(Element.Fire, new List<Mana.Mana>());
        foundMana.Add(Element.Water, new List<Mana.Mana>());
        foundMana.Add(Element.Thunder, new List<Mana.Mana>());
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var mana = col.GetComponent<Mana.Mana>();
        if (mana != null)
        {
            foundMana[mana.element].Add(mana);
            Debug.Log("I found the mana!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var mana = other.GetComponent<Mana.Mana>();
        if (mana != null)
        {
            foundMana[mana.element].Remove(mana);
        }
    }
    
    public IEnumerable<Mana.Mana> GetManaNearby(Element e)
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
