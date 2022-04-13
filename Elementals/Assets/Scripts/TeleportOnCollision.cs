using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public float minTeleportRadius = 4;
    public float maxTeleportRadius = 10f;
    public float speed = 5;
    public bool instantTeleport = false;
    public LineRenderer lineRenderer;
    public Color startColor  = Color.yellow; 
    private void OnCollisionEnter2D(Collision2D other)
    {
        var rigidbody = other.rigidbody;
        if (rigidbody == null) return;


        float r = UnityEngine.Random.Range(minTeleportRadius, maxTeleportRadius);
        float xTeleport = UnityEngine.Random.Range(0, r);
        float yTeleport = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow(xTeleport, 2));
        var teleport = new Vector2(xTeleport, yTeleport);

        var endPosition = rigidbody.position + teleport;
        var travelDistance = teleport.magnitude;
        var time = travelDistance  / speed;
        if (instantTeleport)
        {
            var lr = Instantiate(lineRenderer);
            lr.useWorldSpace = true;
            lr.SetPosition(0, rigidbody.position);
            lr.SetPosition(0, endPosition);
            lr.startColor =lr.endColor = startColor;
            var c1 = new Color2(startColor, startColor);
            var c2 = new Color2(Color.clear, Color.clear);
            
            lr.DOColor(c1, c2, speed)
                .SetEase(Ease.InExpo)
                .OnComplete(() =>
                {
                    Destroy(lr.gameObject);
                });
            
            rigidbody.MovePosition(endPosition);
        }
        else
        {
            StartCoroutine(TeleportBodyToPosition(rigidbody, endPosition, time));
        }
    }

    IEnumerator TeleportBodyToPosition(Rigidbody2D body, Vector2 position, float time)
    {
        float endTime = Time.time + time;
        float startTime = Time.time;
        var startPos = body.position;
        while (Time.time < endTime)
        {
            var percent = (Time.time - startTime) / time;
            body.MovePosition(Vector2.Lerp(startPos, position, percent));
            yield return new WaitForEndOfFrame();
            
            yield return new WaitForSeconds(1);
        }
    }
    
}