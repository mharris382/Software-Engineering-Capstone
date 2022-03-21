
#define SPAWN_PREFABS
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace SOHandlers
{
    [CreateAssetMenu(fileName = "PlayerDied", menuName = "Event Handlers/Player Death Handler", order = 0)]
    public class PlayerDeathHandler : ScriptableObject
    {
        [SerializeField] private float delay= 1f;
        private Vector3 resetPosition;

        
        public void HandlePlayerDied(GameObject player)
        {
            player.GetComponent<MonoBehaviour>().StartCoroutine(WaitThenRespawn(player));
        }

        IEnumerator WaitThenRespawn(GameObject player)
        {
            SpawnDeathPrefabs(player.transform.position);
            player.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(delay);
            player.transform.position = resetPosition;
            player.transform.GetChild(0).gameObject.SetActive(true);
            var hp = player.GetComponentInChildren<HealthState>();
            hp.CurrentValue = hp.MaxValue;
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