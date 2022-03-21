using System.Collections;
using System.Collections.Generic;
using OneLine;
using UnityEngine;
using UnityEngine.Serialization;

public class ReadMe : MonoBehaviour
{
    [FormerlySerializedAs("note")] [TextArea(3, 50)]
    public string readme;

    [SerializeField]
    private UnityEngine.Object[] referencedObjects;
}
