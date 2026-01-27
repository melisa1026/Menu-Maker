using UnityEngine;
using TMPro;

public class ChecklistItem : MonoBehaviour
{
    public TMP_Text foodTypeText;

    FoodType foodType;

    public enum FoodType {
        appetizer,
        meal,
        dessert
    }

    public void initialize(FoodType foodType) {

        this.foodType = foodType;

        foodTypeText.text = foodType.ToString();
    }

    public void check() {

    }

    public void uncheck() {

    }
}
