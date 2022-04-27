using UnityEngine;

namespace Elements.Totem.Gameplay
{
    public class GameplayTotemController : MonoBehaviour
    {
        
        private ITotemPlayerDetection _playerDetection;
        public ITotemPlayerDetection InputHandler
        {
            get => _playerDetection ?? NullTotemPlayerDetection.Instance;
            set => _playerDetection = value;
        }
        
    }
}