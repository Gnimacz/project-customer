using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public string dialog = "The fitness gram pacer test is a multi-stage acrobatics test that gets harder as it progresses..";
    private string displayedDialog = "......";
    private bool displayDialog = false;
    private void OnGUI()
    {
        if (displayDialog)
        {
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 90, 300, 80), "");
            GUI.Label(new Rect(Screen.width/2 - 100, Screen.height - 90, 210, 90), displayedDialog);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        displayedDialog = "";
        StartCoroutine(DisplayDialog(dialog));
        displayDialog = true;
    }

    IEnumerator DisplayDialog(string dialog)
    {
        foreach (char letter in dialog)
        {
            displayedDialog = displayedDialog+ letter;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(5f);
        displayDialog = false;
    }
}
