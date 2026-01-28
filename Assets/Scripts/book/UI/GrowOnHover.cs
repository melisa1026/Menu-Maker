using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GrowOnHover : MonoBehaviour
{
    Vector3 initialScale;
    private EventTrigger trigger;

    public float scaleAmount = 1.1f;

    void Awake()
    {
        initialScale = GetComponent<Transform>().localScale;
        
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
        GetComponent<Transform>().DOScale(initialScale*scaleAmount, 0.3f);
    }

    
    void OnPointerExit(BaseEventData data)
    {
        GetComponent<Transform>().DOScale(initialScale, 0.3f);
    }

}
