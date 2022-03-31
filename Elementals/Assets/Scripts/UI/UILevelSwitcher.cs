using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelSwitcher : MonoBehaviour
{
    public void JumpToLevel(int level)
    {
        SceneManager.LoadScene(Mathf.Clamp(level, 1, 3));
    }
}
