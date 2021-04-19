using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour, IPointerClickHandler
{
    public string textMessage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            MessageSystem.instance.ShowMessageArea();
            MessageSystem.instance.SetText(textMessage);
        }
    }
}
