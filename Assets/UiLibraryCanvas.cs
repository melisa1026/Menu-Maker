using UnityEngine;
using System.Collections.Generic;

public class UiLibraryCanvas : MonoBehaviour
{
    public GameObject characterContainer;

    public CharacterCreator[] getCharacters() {

        CharacterCreator[] characters = new CharacterCreator[characterContainer.transform.childCount];
        
        for (int i = 0; i < characters.Length; i++)
        {

            characters[i] = characterContainer.transform.GetChild(i).gameObject.GetComponent<CharacterCreator>();
        }

        return characters;

    }
}
