using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class EditorStartInit
{
    static EditorStartInit()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        UnityEditor.SceneManagement.EditorSceneManager.playModeStartScene = sceneAsset;
        Debug.Log(pathOfFirstScene + " was set as the first scene");
    }
}