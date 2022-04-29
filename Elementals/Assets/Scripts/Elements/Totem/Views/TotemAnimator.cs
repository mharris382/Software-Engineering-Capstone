using UnityEngine;

namespace Elements.Totem.Views
{
    [RequireComponent(typeof(Animator))]
    public class TotemAnimator : MonoBehaviour, IDisplayTotemElement, IDisplayTotemChargeState
    {
        private Animator _anim;
        private static readonly int Element1 = Animator.StringToHash("element");
        private static readonly int Charging = Animator.StringToHash("charging");

        private Animator anim
        {
            get
            {
                if (_anim == null)
                {
                    _anim = GetComponent<Animator>();
                }
                return _anim;
            }
        }


        public Element Element
        {
            set
            {
                anim.SetInteger(Element1, (int)value);
            }
        }

        public float radius
        {
            set
            {
                
            }
        }

        public bool IsCharging
        {
            set
            {
                anim.SetBool(Charging, value);
            }
        }
    }
}