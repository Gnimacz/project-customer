using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [TextArea] public List<string> dialogList = new List<string>();
    private string dialog = $"[NO DIALOG FOUND]";
    private string displayedDialog = "";
    private bool displayDialog = false;
    [SerializeField] private GUISkin layout;
    private int dialogAmount = 0;
    private int dialogProgress = 0;
    [SerializeField] private float characterdelay = 0.03f;
    [SerializeField] private float dialogdelay = 5f;
    [SerializeField] private int repeatTimes = 1;

    private void Start()
    {
        dialogAmount = dialogList.Count;
    }

    private void OnGUI()
    {
        if (displayDialog)
        {
            GUI.skin = layout;
            GUI.Box(new Rect(Screen.width / 4, Screen.height - 90, Screen.width / 2, Screen.height / 4), "");
            GUI.Label(new Rect(Screen.width / 4 + Screen.width * 0.02f, Screen.height - 90, Screen.width / 2.2f, 90), displayedDialog);
        }
    }

    public void StartDialog()
    {
        StopAllCoroutines();
        string dialog = dialogList[dialogProgress];
        if (repeatTimes > 0) StartCoroutine(DisplayDialog(dialog));
    }

    private IEnumerator DisplayDialog(string dialog)
    {
        displayedDialog = "";
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
                yield return new WaitForSeconds(characterdelay);
            }
        }
        yield return new WaitForSeconds(dialogdelay);
        if (dialogAmount > 0 && repeatTimes > 0)
        {
            dialogAmount--;
            repeatTimes--;
        }

        if (dialogAmount >= 1)
        {
            dialogProgress++;
        }
        displayDialog = false;
    }
}
