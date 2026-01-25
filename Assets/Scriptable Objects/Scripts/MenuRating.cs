using UnityEngine;

public class MenuRating : MonoBehaviour
{

    // !!! testing purposes
    void Start() {

        FoodDescription dish = ScriptableObject.CreateInstance<FoodDescription>();
        dish.initialize();
        FoodDescription character = ScriptableObject.CreateInstance<FoodDescription>();
        character.initialize();
        
        dish.addIntolerance(FoodIcon.foodIcon.GlutenFree);

        character.addIntolerance(FoodIcon.foodIcon.GlutenFree);

        character.addIntolerance(FoodIcon.foodIcon.Vegan);
    }

    public bool canEatDish(FoodDescription dishDescription, FoodDescription characterDescription) {

        // go through each of the character's food intolerances
        // make sure they are in not in the dish

        bool[] dish = dishDescription.intolerances;
        bool[] character = characterDescription.intolerances;

        for (int i = 0; i < character.Length; i++) {
            if(character[i] && !dish[i])
                return false;
        }

        return true;
    }

    public bool canEatSomething(FoodDescription[] dishes, FoodDescription character) {
        
        foreach (FoodDescription dish in dishes) {
            if(canEatDish(dish, character))
                return true;
        }

        return false;
    }

    public bool canEatEverything(FoodDescription[] dishes, FoodDescription character) {
        
        foreach (FoodDescription dish in dishes) {
            if(!canEatDish(dish, character))
                return false;
        }

        return true;

    }

    // TODO 
    public bool lovesDish() {
        return true;
    }

    // TODO
    public bool lovesSomething() {
        return true;
    }

    // TODO
    public bool lovesEverything() {
        return true;
    }
}
