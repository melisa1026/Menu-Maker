using UnityEngine;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    public TMP_Text message;

    public void noneExpectedMessage(MealManager.FoodType foodType) {

        message.text = "No " + foodType.ToString() + "s are required for this event.\n\nClick the List Icon (top left) to view the requirements.";

    }

    public void noneLeftMessage(MealManager.FoodType foodType, int numExpected) {

        message.text = "You already picked " + numExpected + "/" + numExpected + " " + foodType.ToString() + "s!\n\nIf you want to add this " + foodType.ToString() + ", you can uncheck another:)";
    }
}
