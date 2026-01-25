using UnityEngine;

[CreateAssetMenu(fileName = "CategoryOptions", menuName = "Scriptable Objects/CategoryOptions")]
public class SpriteCategoryOptions : ScriptableObject
{

    public static Sprite[] combineArrays(Sprite[] arr1, Sprite[] arr2) {

        Sprite[] newArr = new Sprite[arr1.Length + arr2.Length];
        arr1.CopyTo(newArr, 0);
        arr2.CopyTo(newArr, arr1.Length);

        return newArr;
    }

    public static string[] combineArrays(string[] arr1, string[] arr2) {

        string[] newArr = new string[arr1.Length + arr2.Length];
        arr1.CopyTo(newArr, 0);
        arr2.CopyTo(newArr, arr1.Length);

        return newArr;
    }

    public static Sprite chooseRandom(Sprite[] arr) {

        int choice = UnityEngine.Random.Range(0,arr.Length);

        return arr[choice];
    }
    
    public static string chooseRandom(string[] arr) {

        int choice = UnityEngine.Random.Range(0,arr.Length);
        return arr[choice];
    }

}
