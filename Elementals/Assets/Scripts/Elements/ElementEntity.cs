using OneLine;
using UnityEngine;

namespace Elements
{
    public class ElementEntity : MonoBehaviour
    {
        [SerializeField, OneLine]
        private OptionalElementContainer element;
    }
}