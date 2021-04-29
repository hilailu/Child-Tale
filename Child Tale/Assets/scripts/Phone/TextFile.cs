using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class TextFile : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private GameObject file;
    private string filetext = null;
    private TMP_InputField input;

    public static bool isFileOpen;

    public void Awake()
        => input = file.GetComponent<TMP_InputField>();

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        if (clickCount == 2)
        {
            isFileOpen = true;
            file.SetActive(true);

            if(!string.IsNullOrEmpty(filetext))
                input.text = filetext;
        }
    }

    public void SaveFile()
        => filetext = input.text;

    public void Close()
    {
        file.SetActive(false);
        isFileOpen = false;
    }
}
