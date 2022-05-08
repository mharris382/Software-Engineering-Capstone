using System;
using AudioEvents;
using UnityEngine;

namespace Spell_Casting.Spells
{
    public class ExplodeOnCollision : MonoBehaviour
    {
        public GameObject explodePrefab;
        public AudioEvent explodeSound;
        public bool rotatePrefabToNormal;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Quaternion rotation = Quaternion.identity;
            var pos = other.contacts[0].point;
            if (rotatePrefabToNormal)
            {
                var dir = other.contacts[0].normal;
                var ang = Vector2.SignedAngle(dir, Vector2.right);
                rotation = Quaternion.Euler(0,0,ang);
            }
       
            if (explodeSound != null)
            {
                var go = new GameObject("ExplodeSound");
                go.transform.position = pos;
                var audioSource = go.AddComponent<AudioSource>();
                explodeSound.Play(audioSource);
            }
            else {
                Debug.LogWarning("Missing explode sound");
            }
            var prefab = Instantiate(explodePrefab, pos, rotation);
            Destroy(this.gameObject);
     
            
        }
    }
}