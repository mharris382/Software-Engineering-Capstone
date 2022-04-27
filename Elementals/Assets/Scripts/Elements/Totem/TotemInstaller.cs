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
        private GameplayTotemController _gameController;
        private UITotemController _uiController;
        private TotemStateData _stateData;
        
        private void Awake()
        {
            _uiController = GetComponent<UITotemController>();
            _uiController.InputHandler = GetTotemInputHandler();
            _gameController = GetComponent<GameplayTotemController>();
            
            
            var dependents = GetComponentsInChildren<ITotemDependent>();
            _stateData = new TotemStateData();
            foreach (var dependent in dependents) 
                dependent.InjectSharedTotemState(_stateData);
        }

        //TODO: implement totem input handler
        ITotemInputHandler GetTotemInputHandler()
        {
            throw new NotImplementedException();
        }
        
        //TODO: implement totem player detection
        ITotemPlayerDetection GetTotemPlayerDetection()
        {
            throw new NotImplementedException();
        }
    }
}