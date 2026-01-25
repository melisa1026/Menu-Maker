using UnityEngine;
using System.Collections.Generic;

// *** North america is changed to English/western. 
//      -> But I didn't rename it because I'd have to re-write all the names in the inspector 

[CreateAssetMenu(fileName = "CharacterNames", menuName = "Scriptable Objects/CharacterNames")]
public class CharacterNames : SpriteCategoryOptions
{
    [System.Serializable]
    public struct Options
    {
        public string[] northAmerica, naticeAmerica, southAsia, eastAsia, middleEast, northernEurope, france, italy,
                africa, southAmerica;
    }

    [SerializeField, Space(10)]
    Options girlOptions, boyOptions, lastNames;

    List<string[]> allOptions; 
    
    void InitializeOptionsList(Options o) {

        allOptions = new List<string[]>();

        // this list is in the same order as the CharacterCreator.Region enum
        allOptions.Add(o.northAmerica);
        allOptions.Add(o.naticeAmerica);
        allOptions.Add(o.southAmerica);
        allOptions.Add(o.eastAsia);
        allOptions.Add(o.southAsia);
        allOptions.Add(o.italy);
        allOptions.Add(o.middleEast);
        allOptions.Add(o.france);
        allOptions.Add(o.africa);
        allOptions.Add(o.northernEurope);
    }

    public string chooseName(bool isGirl, CharacterCreator.Region region) {

        // first name
        string firstName = getFirstName(isGirl, region);

        // last name
        string lastName = getLastName(region);

        return firstName + " " + lastName;
    }

    public string getFirstName(bool isGirl, CharacterCreator.Region region) {
        
        if(isGirl) {
            InitializeOptionsList(girlOptions);
        }
        else {
            InitializeOptionsList(boyOptions);
        }

        string[] options = combineRegionAndAmbiguous(region);

        return chooseRandom(options);
    }

    public string getLastName(CharacterCreator.Region region) {
        
        InitializeOptionsList(lastNames);

        int regionIndex = (int) region;
        string[] options = allOptions[regionIndex];

        return chooseRandom(options);
    }

    public string[] combineRegionAndAmbiguous(CharacterCreator.Region region) {
        int regionIndex = (int) region;
        string[] regionNames = allOptions[regionIndex];
        string[] ambiguousNames = allOptions[0];
        string[] combined = combineArrays(regionNames, ambiguousNames);

        return combined;
    }
}
