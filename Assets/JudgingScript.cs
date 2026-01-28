using UnityEngine;
using System.Collections;

public class JudgingScript : MonoBehaviour
{
    /*
        What I need for this scene:
            - Characters -> GameObjects for display and FoodDescriptions
            - Recipes -> FoodDescriptions
    */

    public GameObject displayCharacter;

    void Awake() {

        // hide the screen canvas that was brought from the other scene
        GameObject uiLibraryCanvas= GameObject.Find("Library UI Canvas").gameObject;
        uiLibraryCanvas.SetActive(false);

        // get the characters
        CharacterCreator[] characters = uiLibraryCanvas.GetComponent<UiLibraryCanvas>().getCharacters();

        // test
        StartCoroutine(displayAllCharacters(characters));

        // get the recipes
    }

    IEnumerator displayAllCharacters(CharacterCreator[] characters) {

        for (int i = 0; i < characters.Length; i++) {
            displayCharacter.GetComponent<CharacterCreator>().copyCharacter(characters[i]);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
