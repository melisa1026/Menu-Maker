using UnityEngine;
using TMPro;

// !!! The intolerances I took out of the list (when there's 4) aren't detected (obviously!)

public class Recipe : MonoBehaviour
{
    string recipeName;
    FoodDescription description;

    public MealManager.FoodType foodType;
    public GameObject tasteContainer, intoleranceContainer;
    public TMP_Text chooseRecipeButtonText;

    public int pageNumber;
    public GameObject book;

    void Awake() {

        getRecipeName();

        getFoodDescription();
        
        // make the button say the food type (ex: "Choose Appetizer")
        chooseRecipeButtonText.text = "Choose " + foodType.ToString();

        setPageNumber();
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

    public void chooseItem() {
        MealManager mealManager = GameObject.Find("Meal Manager").gameObject.GetComponent<MealManager>();
        mealManager.chooseMeal(this);
    }

    public void setPageNumber() {
        pageNumber = transform.GetSiblingIndex();
    }
}
