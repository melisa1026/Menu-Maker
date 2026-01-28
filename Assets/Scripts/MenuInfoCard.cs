using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MenuInfoCard : MonoBehaviour
{
    public TMP_Text eventNameText;
    public GameObject checklistContainer;
    public GameObject checklistItemPrefab;
    public MealManager MealManagerScript;
    public GameObject finishButton;

    MealManager.HostEvent hostEvent;

    List<MealManager.HostEvent> easyEvents, mediumEvents, hardEvents;

    List<ChecklistItem> appetizersListItems, mealListItems, dessertListItems; 


    public void initialize(int difficulty) {

        easyEvents = new List<MealManager.HostEvent>();
        mediumEvents = new List<MealManager.HostEvent>();
        hardEvents = new List<MealManager.HostEvent>();

        appetizersListItems = new List<ChecklistItem>();
        mealListItems = new List<ChecklistItem>();
        dessertListItems = new List<ChecklistItem>();

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

        MealManagerScript.setHostEvent(hostEvent);

    }

    
    void displayEvent(int difficulty) {

        // 1. setup the Event Info Card
        eventNameText.text = hostEvent.eventName;

        // 2. add all the checklist items to the card
        
        // add the checklist items
        addItems(hostEvent.numAppetizers, MealManager.FoodType.appetizer);
        addItems(hostEvent.numMeals, MealManager.FoodType.meal);
        addItems(hostEvent.numDesserts, MealManager.FoodType.dessert);
    }

    void addItems(int numItems, MealManager.FoodType foodType) {
        
        for(int i = 0; i < numItems; i++) {

            // create a new checklist item
            GameObject checklistItemObj = Instantiate(checklistItemPrefab, checklistContainer.transform);
            ChecklistItem checklistItem = checklistItemObj.GetComponent<ChecklistItem>();
            checklistItem.initialize(foodType);

            // put it in its corresponding list
            if(foodType == MealManager.FoodType.appetizer)
                appetizersListItems.Add(checklistItem);
            else if(foodType == MealManager.FoodType.meal)
                mealListItems.Add(checklistItem);
            else   
                dessertListItems.Add(checklistItem);
        }

    }


    void addEvent(string eventName, int numAppetizers, int numMeals, int numDesserts, int numGuests, int difficulty) {

        MealManager.HostEvent e = new MealManager.HostEvent(eventName, numAppetizers, numMeals, numDesserts, numGuests);

        if (difficulty == 0)
            easyEvents.Add(e);
        else if (difficulty == 1)
            mediumEvents.Add(e);
        else   
            hardEvents.Add(e);
    }

    MealManager.HostEvent chooseEvent(int difficulty) {

        List<MealManager.HostEvent> events;
        if (difficulty == 0) 
            events = easyEvents;
        else if(difficulty == 1)
            events = mediumEvents;
        else
            events = hardEvents;

        int randomNum = UnityEngine.Random.Range(0, events.Count);
        return events[randomNum];
    }

    public MealManager.HostEvent getEvent() {
        return hostEvent;
    }

    public void checkItem(MealManager.FoodType foodType, Recipe recipe) {

        if(foodType == MealManager.FoodType.appetizer)
            checkItem(appetizersListItems, recipe);
        else if(foodType == MealManager.FoodType.meal)
            checkItem(mealListItems, recipe);
        else
            checkItem(dessertListItems, recipe);
    }

    public void checkItem(List<ChecklistItem> items, Recipe recipe) {
        foreach(ChecklistItem i in items) {
            if(!i.getIsChecked()) {
                i.check(recipe);
                break;
            }
        }
    }

    public void showFinishButton() {
        finishButton.SetActive(true);
    }

    public void hideFinishButton() {
        finishButton.SetActive(false);
    }

}
