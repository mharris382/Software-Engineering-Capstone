using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public float UniformScale
    {
        set
        {
            var scale = new Vector3(value, value, value);
            transform.localScale = scale;
        }
    }
}

