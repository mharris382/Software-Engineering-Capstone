using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStatusBar : MonoBehaviour
    {
        public Image uiProgressBar;
        // public Image uiManaProgressBar;
        public StatusValue target;
        // public StatusValue manaTarget;

        public Transform TargetTransform
        {
            set
            {
                if(value != null)
                    target = value.GetComponent<StatusValue>();
            }
        }
    
        private void Update()
        {
            if (target == null) return;
            uiProgressBar.fillAmount = target.CurrentValue / target.MaxValue;
            // uiManaProgressBar.fillAmount = manaTarget.CurrentValue / manaTarget.MaxValue;
        }
    }
}