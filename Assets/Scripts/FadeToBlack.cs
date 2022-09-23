using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public Animator controller;
    public float waitTime = 2.5f;

    public void Fade()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        controller.SetBool("Fade", true);
        yield return new WaitForSeconds(waitTime);
        controller.SetBool("Fade", false);
    }
}
