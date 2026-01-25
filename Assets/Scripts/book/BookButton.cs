using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookButton : MonoBehaviour
{
    public GameObject bookCoverObject, book;
    public Sprite bookCover;
    
    private EventTrigger trigger;

    void Awake()
    {
        // Set up hover events
        trigger = GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        AddEvent(EventTriggerType.PointerEnter, OnPointerEnter);
        AddEvent(EventTriggerType.PointerExit, OnPointerExit);
        AddEvent(EventTriggerType.PointerClick, OnPointerClick);
    }

    void AddEvent(EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    
    void OnPointerEnter(BaseEventData data)
    {
        bookCoverObject.SetActive(true);
        bookCoverObject.GetComponent<Image>().sprite = bookCover;
        transform.SetAsLastSibling();
    }

    
    void OnPointerExit(BaseEventData data)
    {
        bookCoverObject.SetActive(false);
    }

    void OnPointerClick(BaseEventData data) 
    {
        book.SetActive(true);
    }

}
