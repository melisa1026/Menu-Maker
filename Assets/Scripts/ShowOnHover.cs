using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowOnHover : MonoBehaviour
{
    public GameObject objectToShow;

    private EventTrigger trigger;

    void Awake()
    {
        
        trigger = GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        AddEvent(EventTriggerType.PointerEnter, OnPointerEnter);
        AddEvent(EventTriggerType.PointerExit, OnPointerExit);
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
        objectToShow.SetActive(true);
    }

    
    void OnPointerExit(BaseEventData data)
    {
        objectToShow.SetActive(false);
    }
}
