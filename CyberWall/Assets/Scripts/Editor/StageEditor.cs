using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class StageEditor
{
    static StageEditor()
    {
        //デリゲートらしい
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.Window(1, new Rect(10, 30, 100, 100), Func, "位置調整");
        Handles.EndGUI();
    }

    static void Func(int id)
    {
        if( GUILayout.Button("位置リセット"))
        {
            SceneView.lastActiveSceneView.LookAt(new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            SceneView.lastActiveSceneView.pivot = new Vector3(0, 0, -490);
            SceneView.currentDrawingSceneView.size = 50;
        }
        //if(GUILayout.Button("確認用"))
        //{
        //    Debug.Log(
        //    SceneView.currentDrawingSceneView.size);
        //}
    }
}
