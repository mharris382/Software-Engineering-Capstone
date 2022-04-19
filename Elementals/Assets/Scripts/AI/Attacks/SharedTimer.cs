using UnityEngine;

namespace AI.Attacks
{
    public class SharedTimer : MonoBehaviour
    {
        [Tooltip("Timer Duration in seconds")]
        [Range(0.1f, 60)]
        [SerializeField] private float duration = 1;
    }
}