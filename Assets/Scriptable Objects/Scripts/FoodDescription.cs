using UnityEngine;

// this script has a boolean array that's true for every intolerance or taste description assiciated to a food or character

// For reference, enum is in FoodIcon 

[CreateAssetMenu(fileName = "FoodDescription", menuName = "Scriptable Objects/FoodDescription")]
public class FoodDescription : ScriptableObject
{
    public bool[] tastes, intolerances;

    public void initialize() {
        tastes = new bool[13];
        intolerances = new bool[4];

        for (int i = 0; i < tastes.Length; i++)
            tastes[i] = false;
            
        for (int i = 0; i < intolerances.Length; i++)
            intolerances[i] = false;
    }


    public void addTaste(FoodIcon.foodIcon taste) {

        tastes[(int)taste] = true;
    }
    
    public void addIntolerance(FoodIcon.foodIcon intolerance) {

        intolerances[(int)intolerance - 13] = true;
    }

    public void printTaste() {
        Debug.Log("Tastes:");
        foreach (bool i in tastes) {
            Debug.Log(i);
        }
    }

    public void printIntolerances() {
        Debug.Log("Intolerances");
        foreach (bool i in intolerances) {
            Debug.Log(i);
        }
    }

    public void printDescription() {
        Debug.Log("Taste:");
        printTaste();
        Debug.Log("Intolerances:");
        printIntolerances();
    }

}
