using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MenuInfoCard : MonoBehaviour
{
    public TMP_Text eventNameText;
    public GameObject checklistContainer;
    public GameObject checklistItemPrefab;

    HostEvent hostEvent;

    List<HostEvent> easyEvents, mediumEvents, hardEvents;

    public class HostEvent {
        public string eventName;
        public int numAppetizers, numMeals, numDesserts, numGuests;

        public HostEvent(string eventName, int numAppetizers, int numMeals, int numDesserts, int numGuests) {
            this.eventName = eventName;
            this.numAppetizers = numAppetizers;
            this.numMeals = numMeals;
            this.numDesserts = numDesserts;
            this.numGuests = numGuests;
        }
    }


    public void initialize(int difficulty) {

        easyEvents = new List<HostEvent>();
        mediumEvents = new List<HostEvent>();
        hardEvents = new List<HostEvent>();

        // easy events
        addEvent("Study Session", 2, 0, 0, 2, 0);
        addEvent("Movie Night", 1, 0, 1, 2, 0);
        addEvent("Friday Night Hangout", 2, 0, 1, 2, 0);
        addEvent("Weekly Date Night", 0, 1, 1, 2, 0);
        addEvent("Birthday Hangout", 0, 1, 1, 3, 0);

        // medium events
        addEvent("Birthday Dinner", 1, 1, 1, 3, 1);
        addEvent("Family Dinner", 1, 1, 1, 3, 1);
        addEvent("Anniversary Dinner", 2, 1, 1, 2, 1);
        addEvent("Valentines Day Dinner", 1, 1, 2, 2, 1);
        addEvent("Late Night Party", 3, 0, 0, 3, 1);
        addEvent("Small Work Party", 3, 0, 0, 3, 1);

        // hard events
        addEvent("Birthday Party", 2, 1, 1, 5, 2);
        addEvent("Holiday Dinner", 2, 2, 1, 4, 2);
        addEvent("Grad party", 2, 1, 2, 4, 2);
        addEvent("Baby Shower", 1, 1, 2, 5, 2);
        addEvent("New Year's Party", 3, 0, 2, 4, 2);
        addEvent("Retirement Party", 1, 1, 1, 6, 2);
        
        // choose the event
        hostEvent = chooseEvent(difficulty);

        displayEvent(difficulty);

    }

    
    void displayEvent(int difficulty) {

        // 1. setup the Event Info Card
        eventNameText.text = hostEvent.eventName;

        // 2. add all the checklist items to the card
        
        // add the checklist items
        addItems(hostEvent.numAppetizers, ChecklistItem.FoodType.appetizer);
        addItems(hostEvent.numMeals, ChecklistItem.FoodType.meal);
        addItems(hostEvent.numDesserts, ChecklistItem.FoodType.dessert);
    }

    void addItems(int numItems, ChecklistItem.FoodType foodType) {
        for(int i = 0; i < numItems; i++) {
            GameObject checklistItem = Instantiate(checklistItemPrefab, checklistContainer.transform);
            checklistItem.GetComponent<ChecklistItem>().initialize(foodType);
        }
    }


    void addEvent(string eventName, int numAppetizers, int numMeals, int numDesserts, int numGuests, int difficulty) {

        HostEvent e = new HostEvent(eventName, numAppetizers, numMeals, numDesserts, numGuests);

        if (difficulty == 0)
            easyEvents.Add(e);
        else if (difficulty == 1)
            mediumEvents.Add(e);
        else   
            hardEvents.Add(e);
    }

    HostEvent chooseEvent(int difficulty) {

        List<HostEvent> events;
        if (difficulty == 0) 
            events = easyEvents;
        else if(difficulty == 1)
            events = mediumEvents;
        else
            events = hardEvents;

        int randomNum = UnityEngine.Random.Range(0, events.Count);
        return events[randomNum];
    }

    public HostEvent getEvent() {
        return hostEvent;
    }


}
