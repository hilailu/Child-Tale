using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeCode : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    [SerializeField] Animator animator;

    private string answer = "12345";
    public int maxNumbers = 7;


    public void checkAnswer()
    {
        if (inputField.text == answer)
        {
            animator.SetTrigger("Open");
            inputField.text = "Success";
        }
        else
        {
            inputField.text = "error";
        }
    }

    public void ClearInput()
        => inputField.text = string.Empty;
}
