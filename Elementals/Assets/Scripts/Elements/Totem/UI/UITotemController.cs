using UnityEngine;

namespace Elements.Totem.UI
{
    public class UITotemController : MonoBehaviour
    {
        private ITotemInputHandler _inputHandler;
        public ITotemInputHandler InputHandler
        {
            get => _inputHandler ?? NullTotemInput.Instance;
            set => _inputHandler = value;
        }
        
    }
}