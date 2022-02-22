using UnityEngine;
using UnityEngine.UI;

public class UIstatusBar : MonoBehaviour
{
    public Image uiProgressBar;
    public Image uiManaProgressBar;
    public StatusValue target;
    public StatusValue manaTarget;
    
    
    private void Update()
    {
        uiProgressBar.fillAmount = target.CurrentValue / target.MaxValue;
        uiManaProgressBar.fillAmount = manaTarget.CurrentValue / manaTarget.MaxValue;
    }
}