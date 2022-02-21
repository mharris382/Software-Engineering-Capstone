using UnityEngine;
using UnityEngine.UI;

public class UIstatusBar : MonoBehaviour
{
    public Image uiProgressBar;
    public StatusValue target;

    private void Update()
    {
        uiProgressBar.fillAmount = target.CurrentValue / target.MaxValue;
    }
}