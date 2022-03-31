using UnityEngine;

public class MoveSpeed : MonoBehaviour
{
    public float baseMoveSpeed = 10;
    
    private void OnTransformChildrenChanged()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("SpeedModifier"))
            {
                
            }
        }
    }
    
    
    
}