using UnityEngine;

namespace Elements.Totem.UI
{
    public class TotemMouseWheelInput : ITotemInputHandler
    {
        public int GetElementSelectionInputAxis()
        {
            var input = Input.GetAxis("Mouse ScrollWheel");
            var res = Mathf.Abs(input) > 0.1f ? (int)Mathf.Sign(input) : 0;
            return res;
        }
    }
}