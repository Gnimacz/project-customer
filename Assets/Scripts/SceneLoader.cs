using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float waitTime = 1.2f;
    public Animator controller;
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneDelay(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadSceneDelay(int index)
    {
        controller.SetBool("Fade", true);
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(index);
    }
}