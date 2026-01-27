using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CharacterCarousel : MonoBehaviour
{
    
    public GameObject carouselOutlines, carouselSelections;
    public GameObject characterPrefab;
    public TMP_Text characterName;
    public GameObject intoleranceContainer, likesContainer;

    public GameObject[] intolerancePrefabs;
    public GameObject[] tastePrefabs;

    int currentSelection = 0;
    GameObject[] characters = null;


    public void InitializeCharacters(int numCharacters)
    {
        characters = new GameObject[numCharacters];

        // create the character outlines (the semi transparent ones)
        GameObject newCharacter = null;
        for(int i = 0; i < numCharacters; i++) 
        {
            // create a new character
            newCharacter = Instantiate(characterPrefab, carouselOutlines.transform);
            newCharacter.GetComponent<CharacterCreator>().CreateCharacter();
            newCharacter.GetComponent<CharacterForCarousel>().mask(true);
            newCharacter.transform.SetAsFirstSibling();

            characters[i] = newCharacter;
        }

        // create the character selections (un-transparent ones)
        for(int i = 0; i < numCharacters; i++) 
        {
            // copy the outline character
            newCharacter = Instantiate(characters[i], carouselSelections.transform);
            newCharacter.GetComponent<CharacterForCarousel>().mask(false);
            newCharacter.transform.SetAsFirstSibling();
            show(newCharacter, false);

            characters[i] = newCharacter;
        }

        // default selection: index 0 (only )
        show(characters[0], true);
    }

    public void scroll(bool isRight) {

        // hide the current selection
        show(characters[currentSelection], false);

        // get the new selection
        if(isRight) {
            currentSelection = (currentSelection + 1) % characters.Length;
        }
        else {
            currentSelection = (currentSelection - 1);

            if(currentSelection < 0)
                currentSelection = characters.Length - 1;
        }

        // show the new selection
        show(characters[currentSelection], true);
        updateCharacterInfoCard(characters[currentSelection]);

        // UI polish
        characters[currentSelection].GetComponent<CharacterForCarousel>().growOnSelect();
    }

    void show(GameObject character, bool show) {
        character.GetComponent<CharacterForCarousel>().show(show);
    }

    void updateCharacterInfoCard(GameObject selectedCharacter) {

        // get the CharacterCeator script
        CharacterCreator cc = selectedCharacter.GetComponent<CharacterCreator>();

        // set the character name
        characterName.text = cc.getName();

        // put the character intolerances and likes on the screen
        setCharacterIntolerances(selectedCharacter);
        setCharacterLikes(selectedCharacter);
    }

    void setCharacterIntolerances(GameObject selectedCharacter) {

        // 1. Clear the current intolerances
         foreach(Transform child in intoleranceContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 2. Get the new character's intolerances
        FoodDescription foodDescription = selectedCharacter.GetComponent<CharacterCreator>().foodDescription;
        bool[] intolerances = foodDescription.intolerances;

        // 3. Put the intolrances on screen
        for(int i = 0; i < intolerances.Length; i++) {
            if(intolerances[i]) {
                Instantiate(intolerancePrefabs[i], intoleranceContainer.transform);
            }
        }
    }

    void setCharacterLikes(GameObject selectedCharacter) {

        // 1. Clear the current likes
         foreach(Transform child in likesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 2. Get the new character's likes
        FoodDescription foodDescription = selectedCharacter.GetComponent<CharacterCreator>().foodDescription;
        bool[] likes = foodDescription.tastes;

        // 3. Put the likes on screen
        for(int i = 0; i < likes.Length; i++) {
            if(likes[i]) {
                Instantiate(tastePrefabs[i], likesContainer.transform);
            }
        }
    }
} 

