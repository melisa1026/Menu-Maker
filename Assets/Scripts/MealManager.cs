using UnityEngine;
using System.Collections.Generic;

public class MealManager : MonoBehaviour
{
    public MenuInfoCard menuInfoCard;
    public GameObject errorMessage;

    HostEvent hostEvent;
    List<Recipe> recipes;
    int numAppetizersChosen, numMealsChosen, numDessertsChosen;

    
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
    
    public enum FoodType {
        appetizer,
        meal,
        dessert
    }

    void Awake() {
        recipes = new List<Recipe>();
    }

    public void setHostEvent(HostEvent hostEvent) {
        this.hostEvent = hostEvent;
    }

    public void chooseMeal(Recipe recipe) {

        // 1. check if it's a meal, appetizer or dessert
        FoodType recipeFoodType = recipe.foodType;

        // 2. make sure that item is expected
        if((recipeFoodType == FoodType.appetizer && hostEvent.numAppetizers == 0) ||
            (recipeFoodType == FoodType.meal && hostEvent.numMeals == 0) ||
            (recipeFoodType == FoodType.dessert && hostEvent.numDesserts == 0))
        {
            errorMessage.SetActive(true);
            errorMessage.GetComponent<ErrorMessage>().noneExpectedMessage(recipeFoodType);
            return;
        }

        // 3. check if there's any spots left for the meal
        if(getNumExpected(recipeFoodType) == getNumChosen(recipeFoodType))
        {
            errorMessage.SetActive(true);
            errorMessage.GetComponent<ErrorMessage>().noneLeftMessage(recipeFoodType, getNumChosen(recipeFoodType));
            return;
        }

        // 4. check the item
        menuInfoCard.gameObject.SetActive(true);
        menuInfoCard.checkItem(recipeFoodType, recipe);
        countItem(recipeFoodType);
        

    }

    public void countItem(FoodType foodType) {
        if(foodType == FoodType.appetizer) {
            numAppetizersChosen++;
        }
        else if(foodType == FoodType.meal) {
            numMealsChosen++;
        }
        else {
            numDessertsChosen++;
        }
    }

    public int getNumExpected(FoodType foodType) {

        if(foodType == FoodType.appetizer) {
            return hostEvent.numAppetizers;
        }
        else if(foodType == FoodType.meal) {
            return hostEvent.numMeals;
        }
        else {
            return hostEvent.numDesserts;
        }
    }

    public int getNumChosen(FoodType foodType) {

        if(foodType == FoodType.appetizer) {
            return numAppetizersChosen;
        }
        else if(foodType == FoodType.meal) {
            return numMealsChosen;
        }
        else {
            return numDessertsChosen;
        }
    }

    public void uncount(FoodType foodType) {
        if(foodType == FoodType.appetizer) {
            numAppetizersChosen--;
        }
        else if(foodType == FoodType.meal) {
            numMealsChosen--;
        }
        else {
            numDessertsChosen--;
        }
    }
}
