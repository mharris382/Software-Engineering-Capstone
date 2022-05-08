using System;
using UnityEngine;

namespace UI
{
    public class UISettingsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject settingsMenu;
        
        public void ToggleSettingsMenu()
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
        }
        
        public void CloseSettingsMenu()
        {
            settingsMenu.SetActive(false);
        }
        
        public void OpenSettingsMenu()
        {
            settingsMenu.SetActive(true);
        }

        private void Start()
        {
            CloseSettingsMenu();
        }
    }
}