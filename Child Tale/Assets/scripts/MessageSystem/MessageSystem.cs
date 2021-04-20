using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageSystem : MonoBehaviour, ISaveable
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
        SaveSystem.onSave += Save;
        SaveSystem.onLoad += Load;
        if (instance != null)
            return;
        instance = this;
    }
    private MessageSystem() { }
    #endregion

    void OnDestroy()
    {
        SaveSystem.onSave -= Save;
        SaveSystem.onLoad -= Load;
    }

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

    public void Save()
    {
        PlayerData.instance.messages = indexOfMessage;
    }

    public void Load()
    {
        indexOfMessage = PlayerData.instance.messages;
        for (int i = 0; i < indexOfMessage; i++)
        {
            Instantiate(messages[i], parentOfMessage.transform);
        }
    }
}
