using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string message;
    public string textMessage { get => message; set => message = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            MessageSystem.instance.ShowMessageArea();
            MessageSystem.instance.SetText(textMessage);
        }
    }
}
