using UnityEngine;
using TMPro;


public class buttonSafeNumber : MonoBehaviour
{
    [SerializeField] SafeCode safeCode;
    [SerializeField] TMP_Text textButton;


    public void ButtonNumberClick()
    {
        if (safeCode.maxNumbers > safeCode.inputField.text.Length && safeCode.inputField.text != "success")
        {
            if (safeCode.inputField.text == "error")
                safeCode.ClearInput();

            safeCode.inputField.text += textButton.text;
        }
    }
}
