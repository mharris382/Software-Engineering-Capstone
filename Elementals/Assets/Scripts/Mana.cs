using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mana : MonoBehaviour
{
    public Element element;

    public bool updateColor = true;


    private void OnDrawGizmosSelected()
    {
        if (!updateColor) return;
        if (Application.isPlaying) return;
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        switch (element)
        {
            case Element.Fire:
                sr.color = new Color(255, 91, 59);
                break;
            case Element.Water:
                sr.color =  new Color(59, 214, 255);
                break;
            case Element.Earth:
                sr.color = new Color(6, 154, 0);
                break;
            case Element.Air:
                sr.color = Color.white;
                break;
            case Element.Thunder:
                sr.color = new Color(6, 154, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}


