using System;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public static class CustomMenuButtons
    {
        private const float WINDOW_WIDTH = 400;
        private const float BUTTON_HEIGHT = 200;
        private const float FIELD_HEIGHT = 30;
        private const float Y_PADDING = 5;
        private const float OFFSET = 3;
        
        
        [MenuItem("GameObject/New Divider Transform")]
        public static void AddNewDividerObject()
        {
            FolderNameWindow window = (FolderNameWindow)EditorWindow.GetWindow(typeof(FolderNameWindow));
            var windowPos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            
            var screen_x = Screen.width;
            
            
            
            
            var windowWidth = WINDOW_WIDTH + (OFFSET * 2);
            var windowHeight = BUTTON_HEIGHT + FIELD_HEIGHT + Y_PADDING + (OFFSET * 2);
            var windowSize = new Vector2(windowWidth, windowHeight);
            
            if (screen_x - (windowPos.x*screen_x) < windowWidth)
            {
                windowPos.x += screen_x - (windowPos.x * screen_x);
            }
            var windowRect = new Rect(windowPos, windowSize);
            var fieldRect = new Rect(OFFSET, OFFSET, WINDOW_WIDTH-OFFSET, FIELD_HEIGHT);
            var buttonRect = new Rect(OFFSET, fieldRect.min.y + Y_PADDING, WINDOW_WIDTH-OFFSET, BUTTON_HEIGHT);
            
            
            window.position = windowRect;
            window.fieldRect = fieldRect;
            window.buttonRect = buttonRect;
            window.ShowPopup();
            
        }
    }

    public class FolderNameWindow : EditorWindow
    {
        
        private string folderName;
        public Rect fieldRect;
        public Rect buttonRect;
        private void OnGUI()
        {
            folderName = EditorGUI.TextField(fieldRect, folderName, EditorStyles.popup);
            if (GUI.Button(buttonRect, "Create"))
            {
                string line = "------------------------------------------------";
                var n = $"--[{folderName.ToUpper()}]";
                var name = $"{{n}}{line.Substring(n.Length)}";
                var go = new GameObject(name);
                folderName = "";
            }
            folderName = EditorGUILayout.TextField("", folderName, EditorStyles.popup);
            
        }
    }
}