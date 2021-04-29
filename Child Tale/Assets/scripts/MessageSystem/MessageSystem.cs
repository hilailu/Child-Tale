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
        if (!Photon.Pun.PhotonNetwork.OfflineMode || EndGame.isGameEnd) return;

        string time = CustomTime.ToStringTime();

        if (time == "09:05" && indexOfMessage == 0)           // мама
            NewMessage();

        else if (time == "09:30" && indexOfMessage == 1)      // друг
            NewMessage();

        else if (time == "10:10" && indexOfMessage == 2)      // костян_1 (буллер)
            NewMessage();

        else if (time == "11:40" && indexOfMessage == 3)      // костян_2
            NewMessage();

        else if (time == "13:00" && indexOfMessage == 4)      // костян_3
            NewMessage();

        else if (time == "14:30" && indexOfMessage == 5)      // костян_4
            NewMessage();

        else if (time == "16:00" && indexOfMessage == 6)      // костян_5
            NewMessage();
    }

    public void Save()
        => PlayerData.instance.messages = indexOfMessage;

    public void Load()
    {
        indexOfMessage = PlayerData.instance.messages;
        for (int i = 0; i < indexOfMessage; i++)
        {
            Instantiate(messages[i], parentOfMessage.transform);
        }
    }
}
