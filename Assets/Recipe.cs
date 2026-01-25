using UnityEngine;


// !!! The intolerances I took out of the list (when there's 4) aren't detected (obviously!)

public class Recipe : MonoBehaviour
{
    string recipeName;
    FoodDescription description;
    
    public GameObject tasteContainer, intoleranceContainer;

    void Awake() {

        getRecipeName();

        getFoodDescription();
        
        Debug.Log("Intolerances: ");
        foreach(bool i in description.intolerances) {
            Debug.Log(i);
        }
    }

    // automatically get the recipe name
    void getRecipeName() {
        recipeName = gameObject.name;
    }

    // automatically create the food description
    void getFoodDescription() {

        description = ScriptableObject.CreateInstance<FoodDescription>();
        description.initialize();

        // set all the tastes
        foreach (Transform taste in tasteContainer.transform) {

            FoodIcon foodIcon = taste.Find("Food Icon").gameObject.GetComponent<FoodIcon>();
            description.addTaste(foodIcon.icon);
        }

        // set all the intolerances
        foreach (Transform intolerance in intoleranceContainer.transform) {
            FoodIcon foodIcon = intolerance.Find("Food Icon").gameObject.GetComponent<FoodIcon>();
            description.addIntolerance(foodIcon.icon);
        }
    }
}
