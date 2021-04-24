using UnityEngine;
using TMPro;


public class buttonSafeNumber : MonoBehaviour
{
    [SerializeField] SafeCode safeCode;
    [SerializeField] TMP_Text textButton;


    public void ButtonNumberClick()
    {
        if (safeCode.maxNumbers > safeCode.inputField.text.Length)
        {
            if (!string.IsNullOrEmpty(safeCode.inputField.text) && !char.IsDigit(safeCode.inputField.text[0])) 
                safeCode.ClearInput();

            safeCode.inputField.text += textButton.text;
        }
    }
}
