using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    public void openRecipeURL(string link) {
        Application.OpenURL(link);
    }
}
