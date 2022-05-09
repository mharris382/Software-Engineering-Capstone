
#define SPAWN_PREFABS
using System;
using System.Collections;
using AudioEvents;
using UnityEngine;
using UnityEngine.Serialization;

namespace SOHandlers
{
    /// <summary>
    /// this acts a message broker for player death event.  It also resets the player's position and health.
    /// Optionally provide a position to respawn at.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerDied", menuName = "Core/Event Handlers/Player Death Handler", order = 0)]
    public class PlayerDeathHandler : ScriptableObject
    {
        [SerializeField] private float delay= 1f;
        [SerializeField] private AudioEvent deathSound;
        private Vector3 resetPosition;

        
        public void HandlePlayerDied(GameObject player)
        {
            if (deathSound != null) {
                var go = new GameObject("Death Sound");
                go.transform.position = player.transform.position;
                var audioSource = go.AddComponent<AudioSource>();
                deathSound.Play(audioSource);
            }
            player.GetComponent<MonoBehaviour>().StartCoroutine(WaitThenRespawn(player));
        } 
        public void HandlePlayerDied(GameObject player, Vector2 respawnPosition)
        {
            if (deathSound != null) {
                var go = new GameObject("Death Sound");
                go.transform.position = player.transform.position;
                var audioSource = go.AddComponent<AudioSource>();
                deathSound.Play(audioSource);
            }
            player.GetComponent<MonoBehaviour>().StartCoroutine(WaitThenRespawn(player, respawnPosition));
        }

        IEnumerator WaitThenRespawn(GameObject player, Vector2? respawnPoint = null)
        {
            SpawnDeathPrefabs(player.transform.position);
            player.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(delay);
            player.transform.position = respawnPoint ?? resetPosition;
            player.transform.GetChild(0).gameObject.SetActive(true);
            var hp = player.GetComponentInChildren<HealthState>();
            hp.CurrentValue = hp.MaxValue;
            var mana = player.GetComponentInChildren<ManaState>();
            mana.CurrentValue = mana.MaxValue;
        }
        
        void SpawnDeathPrefabs(Vector3 transformPosition)
        {
#if SPAWN_PREFABS
            foreach (var prefab in onDeathPrefabs)
            {
                prefab.Spawn(transformPosition);
            }
#endif
        }
#if SPAWN_PREFABS        


        
        [SerializeField]
        private SpawnOnDeath[] onDeathPrefabs;

        [Serializable]
        private class SpawnOnDeath
        {
            public GameObject prefab;
            public Vector3 position;

            public void Spawn(Vector3 pos)
            {
                if (prefab == null) return;
                Instantiate(prefab, pos + position, Quaternion.identity);
            }
        }
#endif
    }
}