using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class IMGUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Box(new Rect(40, 40, 100, 90), "Loader");

        if (GUI.Button(new Rect(10, 30, 70, 20), "reset level"))
        {
            EditorSceneManager.LoadScene(0);
        }

        if(Time.time % 2 < 1)
        {
            if(GUI.Button(new Rect(10, 50, 70, 20), "Test")){
                Debug.Log("Clicked");
            }
        }
    }
}
