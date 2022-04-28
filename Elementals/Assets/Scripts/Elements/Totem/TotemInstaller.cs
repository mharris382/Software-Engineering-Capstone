using System;
using Elements.Totem.Gameplay;
using Elements.Totem.UI;
using UnityEngine;

namespace Elements.Totem
{
    
    /// <summary>
    /// handles Dependency Injection for all totem systems: Gameplay Systems, View Systems, and UI Systems
    ///
    /// <para>Dependency Injection is an alternative approach for decoupling systems within unity. This is apposed to IoC (Inversion of Control) which is used
    /// in the majority of this project's systems. One advantage of Dependency Injection as opposed to IoC is that writing Unit Tests is much easier, because the
    /// Unit Test can take the place of the installer, and resolve all dependencies using custom test objects instead of external systems which cannot be used in
    /// automated testing such as user input</para>
    /// </summary>
    [RequireComponent(typeof(UITotemController), typeof(GameplayTotemController))]
    public class TotemInstaller : MonoBehaviour
    {
        [SerializeField] private TotemConfig config;

        
        private TotemStateData _stateData;
        
        private GameplayTotemController _gameController;
        private UITotemController _uiController;
        
        private ITotemPlayerDetection _playerDetection;
        private ITotemInputHandler _inputHandler;

        private void Awake()
        {
            void InstallAllDepdencies()
            {
                _gameController.PlayerDetection = _playerDetection;
                _uiController.InputHandler = _inputHandler;
                _uiController.StateData = _stateData;
                _gameController.StateData = _stateData;
            }

            void LoadAllDependencies()
            {
                _stateData = new TotemStateData(config);
                _uiController = GetComponent<UITotemController>();
                _gameController = GetComponent<GameplayTotemController>();
                _playerDetection = GetComponentInChildren<ITotemPlayerDetection>();
                _inputHandler = new TotemMouseWheelInput();
                Debug.Assert(_playerDetection != null, $"No ITotemPlayerDetection found on Totem: {name}", this);
            }

            void InitializeDependents()
            {
                var dependents = GetComponentsInChildren<ITotemDependent>();
                foreach (var dependent in dependents) dependent.InjectSharedTotemState(_stateData);
                _uiController.Initialize();
                _gameController.Initialize();
            }

            LoadAllDependencies();
            InstallAllDepdencies();
            InitializeDependents();
        }

        private void OnDrawGizmosSelected()
        {
            var radius = config.totemRadius;
            var color = Color.magenta;
            color.a = 0.4f;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}