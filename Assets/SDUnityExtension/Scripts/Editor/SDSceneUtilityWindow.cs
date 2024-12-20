using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SDUnityExtension.Scripts.Editor
{
    public class SDSceneUtilityWindow : EditorWindow
    {
        private Vector2 scrollPos;
        
        [MenuItem("SDUnityExtension/Open Scene Utility Window")]
        internal static void Init()
        {
            var window = (SDSceneUtilityWindow)GetWindow(typeof(SDSceneUtilityWindow), false, "SD Scene Utility");
            window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
        }
        
        internal void OnGUI()
        {
            // Scenes View ===================================================================================
            EditorGUILayout.BeginVertical();
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

            GUILayout.Label("Scenes In Build", EditorStyles.boldLabel);
            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var scene = EditorBuildSettings.scenes[i];
                if (scene.enabled)
                {
                    var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                    var pressed = GUILayout.Button(i + ": " + sceneName, new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft });
                    if (pressed == false) continue;
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorSceneManager.OpenScene(scene.path);
                    }
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            // ===============================================================================================
            
            EditorGUILayout.Separator();
        }
    }
}