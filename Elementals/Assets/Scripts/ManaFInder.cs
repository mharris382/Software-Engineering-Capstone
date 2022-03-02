using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public interface IManaFinder
{
    IEnumerable<Mana> GetManaNearby(Element e);
}
public class ManaFinder : MonoBehaviour, IManaFinder
{
    Dictionary<Element, List<Mana>> foundMana;
    [Multiline(4)]
   [SerializeField] private string json;
    public void Awake()
    {
        foundMana = new Dictionary<Element, List<Mana>>();
        foundMana.Add(Element.Air, new List<Mana>());
        foundMana.Add(Element.Earth, new List<Mana>());
        foundMana.Add(Element.Fire, new List<Mana>());
        foundMana.Add(Element.Water, new List<Mana>());
        foundMana.Add(Element.Thunder, new List<Mana>());
    }

    private void Start()
    {
        InvokeRepeating("WriteJson", 0.1f, 0.5f);
    }

    void WriteJson()
    {
        var dic = foundMana.Select(t => new KeyValuePair<Element, string>(t.Key, t.Value.Count.ToString()));
        var d = new Dictionary<Element, string>();
        foreach (var keyValuePair in dic)
        {
            d.Add(keyValuePair.Key, keyValuePair.Value);
        }
        json = JsonConvert.SerializeObject(d);
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
