using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject dropPrefab;

    public int minNumberOfDrops = 1;
    public int maxNumberOfDrops = 10;
    public float maxRadius = .5f;
    void Drop()
    {
        var number = UnityEngine.Random.Range(minNumberOfDrops, maxNumberOfDrops);
        var position = transform.position;
        for (int i = 0; i < number; i++)
        {
            var rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
            var distance = UnityEngine.Random.Range(0, maxRadius);
            var offset = rotation * (Vector3.right * distance);
            Instantiate(dropPrefab, position + offset, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Drop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
