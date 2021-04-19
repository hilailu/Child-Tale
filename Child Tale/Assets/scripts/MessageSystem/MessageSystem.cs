using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] GameObject messageShowArea;
    [SerializeField] TMP_Text textOfMessage;
    [SerializeField] GameObject parentOfMessage;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject[] messages;
    private int indexOfMessage = 0;

    #region Singleton
    public static MessageSystem instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    private MessageSystem() { }
    #endregion

    public void ShowMessageArea()
        => messageShowArea.SetActive(true);

    public void CloseMessageArea()
        => messageShowArea.SetActive(false);

    public void SetText(string message)
        => textOfMessage.text = message;

    private void NewMessage()
    {
        Instantiate(messages[indexOfMessage++], parentOfMessage.transform);
        animator.SetTrigger("Show");
        audioSource.Play();
    }

    private void Update()
    {
        if (CustomTime.ToStringTime() == "09:05" && indexOfMessage == 0)
            NewMessage();
        else if (CustomTime.ToStringTime() == "09:30" && indexOfMessage == 1)
            NewMessage();
    }
}
