using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
public class SceneNavigator : EditorWindow
{
    public class SceneList
    {
        public string sceneName;
        public bool enable;

        public SceneList(string _sceneName, bool _enable)
        {
            sceneName = _sceneName;
            enable = _enable;
        }
    }
    static List<SceneList> sceneLists;
    static string sceneName;
    [MenuItem("Window/Scene Navigator")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(SceneNavigator), false, "Scene Navigator", false);
        sceneLists = new();
        int NoOfScene = EditorBuildSettings.scenes.Length;
        sceneLists.Clear();
        for (int i = 0; i < NoOfScene; i++)
        {
            sceneLists.Add(new SceneList(EditorBuildSettings.scenes[i].path, EditorBuildSettings.scenes[i].enabled));
        }
    }
    static void SceneLoader()
    {
        if (sceneLists == null || sceneLists.Count == 0)
        {
            Init();
        }
        GUIStyle style = new(GUI.skin.button);
        var _text = style.normal.textColor;
        for (int i = 0; i < sceneLists.Count; i++)
        {
            sceneName = sceneLists[i].sceneName[(sceneLists[i].sceneName.LastIndexOf('/') + 1)..];
            if (!sceneLists[i].enable)
            {
                style.normal.textColor = _text - new Color(0, 0, 0, 0.5f);
            }
            else
            {
                style.normal.textColor = _text;
            }
            if (GUILayout.Button(sceneName, style))
            {
                if (!EditorApplication.isPlaying)
                {
                    if (EditorUtility.DisplayDialog("Confirm \n Are you sure to open  " + sceneName + "?", "", "Yes", "No"))
                    {
                        string[] path = EditorSceneManager.GetActiveScene().path.Split(char.Parse("/"));
                        bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                        if (saveOK)
                        {
                            EditorSceneManager.OpenScene(sceneLists[i].sceneName);
                        }
                        else
                        {
                            Debug.LogError("Error");
                        }
                    }
                }
            }
        }
    }
    private GUIStyle _toolbarButtonStyle;
    private void ShowButton(Rect position)
    {
        // button style
        if (_toolbarButtonStyle == null)
        {
            _toolbarButtonStyle = new GUIStyle()
            {
                padding = new RectOffset(),
            };
        }
        // draw button
        if (GUI.Button(position, EditorGUIUtility.IconContent("Refresh"), _toolbarButtonStyle))
        {
            Init();
        }
    }
    void OnGUI()
    {
        SceneLoader();
    }
}