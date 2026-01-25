using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CharacterCarousel : MonoBehaviour
{
    
    public GameObject carouselOutlines, carouselSelections;
    public GameObject characterPrefab;
    public TMP_Text characterName;
    public GameObject intoleranceContainer;

    int currentSelection = 0;
    GameObject[] characters = null;


    void Start() {
        InitializeCharacters(5);
    }

    void InitializeCharacters(int numCharacters)
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

        // put the character intolerances on the screen
        setCharacterIntolerances();
    }

    void setCharacterIntolerances() {

        // 1. Clear the current intolerances
         foreach(Transform child in intoleranceContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }



    /* 
    
    public GameObject characterCarouselPrefab;
    public TMP_Text characterName;

    GameObject selectedCharacter = null;
    bool characterInitialized = false;

    void Start() {
        Initialize(5);
    }

    void Update() {

       // only initially update the character info card when the selected character is loaded
        if(!characterInitialized && selectedCharacter != null && 
            selectedCharacter.transform.GetChild(0).gameObject.GetComponent<CharacterCreator>().characterInitialized) 
        {
            updateCharacterInfoCard(selectedCharacter);
            characterInitialized = true;
        }
    }

    void Initialize(int numCharacters)
    {
        GameObject newCharacter = null;
        for(int i = 0; i < numCharacters; i++) {

            newCharacter = Instantiate(characterCarouselPrefab, this.gameObject.transform);
            newCharacter.GetComponent<CharacterForCarousel>().characterCreatorScript.CreateCharacter;
            
            newCharacter.GetComponent<CharacterForCarousel>().unselect();
        }
        
        // default selection
        selectedCharacter = newCharacter;
        newCharacter.GetComponent<CharacterForCarousel>().select();
    }

    public void scroll(bool isRight) {

        // get the list of children
        List<GameObject> allCharacters = new List<GameObject>();
        foreach (Transform child in transform)
        {
            allCharacters.Add(child.gameObject);
        }


        // unselect the currently selected character
        allCharacters[transform.childCount - 1].GetComponent<CharacterForCarousel>().unselect();


        // get the next selection (from the left or right)
        GameObject newSelection;
        if(isRight) {
            allCharacters[transform.childCount - 1].GetComponent<RectTransform>().SetAsFirstSibling();
            newSelection = allCharacters[transform.childCount - 2];
        }
        else 
            newSelection = allCharacters[0];

        newSelection.GetComponent<CharacterForCarousel>().select();


        updateCharacterInfoCard(newSelection);
    }

    
    void updateCharacterInfoCard(GameObject selectedCharacter) {

        // get the CharacterCeator script
        CharacterCreator cc = selectedCharacter.transform.GetChild(0).gameObject.GetComponent<CharacterCreator>();

        // set the character name
        characterName.text = cc.getName();
    }

    */

}
