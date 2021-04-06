using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class buttonSafeNumber : MonoBehaviour
{
    [SerializeField] SafeCode safeCode;
    private TextMeshPro textButton;


    private void Start()
    {
        textButton = GetComponentInChildren<TextMeshPro>();
    }

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
