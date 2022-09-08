using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public string dialog = "The fitness gram pacer test is a multi-stage capacity test that gets harder as it progresses..";
    private string displayedDialog = "......";
    private bool displayDialog = false;
    [SerializeField] private GUISkin layout;
    private void OnGUI()
    {
        if (displayDialog)
        {
            GUI.skin = layout;
            GUI.Box(new Rect(Screen.width / 4, Screen.height - 90, Screen.width / 2, Screen.height / 4), "");
            GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.02f, Screen.height - 90, Screen.width / 2.2f, 90), displayedDialog);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!displayDialog)
        {
            displayedDialog = "";
            StartCoroutine(DisplayDialog(dialog));
            displayDialog = true;
        }
    }

    public IEnumerator DisplayDialog(string dialog)
    {
        displayDialog = true;
        foreach (char letter in dialog)
        {
            if (letter == '^')
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                displayedDialog = displayedDialog + letter;
                yield return new WaitForSeconds(0.03f);
            }
        }
        yield return new WaitForSeconds(5f);
        displayDialog = false;
    }
}
