using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public class TextureToFile : EditorWindow
    {
        public RenderTexture renderTexture;
        public string filename;
        SerializedProperty p_renderTexture;
        private SerializedObject so;
        [MenuItem("Tools/TextureToFile")]
        private static void ShowWindow()
        {
            var window = GetWindow<TextureToFile>();
            window.titleContent = new GUIContent("TextureToFile");
            window.p_renderTexture = new SerializedObject(window).FindProperty("renderTexture");
            window.so = new SerializedObject(window);
            window.Show();
        }

        private void OnGUI()
        {
            so.Update();
            EditorGUILayout.PropertyField(p_renderTexture, new GUIContent( "RenderTexture"));
            renderTexture =  (RenderTexture)EditorGUILayout.ObjectField(renderTexture, typeof(RenderTexture), true);
            so.ApplyModifiedProperties();
            if (GUILayout.Button("Save") && renderTexture != null)
            {
                SaveTextureToFileUtility.SaveRenderTextureToFile(renderTexture, $@"B:\Unity_Projects\_repos2\Software-Engineering-Capstone\Elementals\Assets\Art\UI\Render\{renderTexture.name}", SaveTextureToFileUtility.SaveTextureFileFormat.PNG);
                var path = EditorUtility.SaveFilePanel("Save", "", "RenderTexture", "png");
                if (path.Length != 0)
                {
                    var tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, false);
                    var oldRt = RenderTexture.active;
                    RenderTexture.active = renderTexture;
                    tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                    tex.Apply();
                    RenderTexture.active = oldRt;
                    System.IO.File.WriteAllBytes(path, tex.EncodeToPNG());
                }
            }
        }
    }
}