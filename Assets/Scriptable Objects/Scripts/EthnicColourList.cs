using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ColourList", menuName = "Scriptable Objects/ColourList")]
public class EthnicColourList : ScriptableObject
{
    [System.Serializable]
    public struct ColourOptions
    {
        public Color[] colours;
        public Color[] dye;
        public int ambiguous, nativeAmerica, southAsia, eastAsia, middleEast, northernEurope, italy, france, africa, southAmerica;
    }
    
    [SerializeField]
    ColourOptions skinColours, hairColours;

    List<int> allHairOptions, allSkinOptions; 

    void InitializeHairOptionsList() {

        allHairOptions = new List<int>();

        // this list is in the same order as the CharacterCreator.Region enum
        allHairOptions.Add(hairColours.ambiguous);
        allHairOptions.Add(hairColours.nativeAmerica);
        allHairOptions.Add(hairColours.southAmerica);
        allHairOptions.Add(hairColours.eastAsia);
        allHairOptions.Add(hairColours.southAsia);
        allHairOptions.Add(hairColours.italy);
        allHairOptions.Add(hairColours.middleEast);
        allHairOptions.Add(hairColours.france);
        allHairOptions.Add(hairColours.africa);
        allHairOptions.Add(hairColours.northernEurope);
    }

    void InitializeSkinOptionsList() {

        allSkinOptions = new List<int>();

        // this list is in the same order as the CharacterCreator.Region enum
        allSkinOptions.Add(skinColours.ambiguous);
        allSkinOptions.Add(skinColours.nativeAmerica);
        allSkinOptions.Add(skinColours.southAmerica);
        allSkinOptions.Add(skinColours.eastAsia);
        allSkinOptions.Add(skinColours.southAsia);
        allSkinOptions.Add(skinColours.italy);
        allSkinOptions.Add(skinColours.middleEast);
        allSkinOptions.Add(skinColours.france);
        allSkinOptions.Add(skinColours.africa);
        allSkinOptions.Add(skinColours.northernEurope);

    }


    public Color chooseSkinColour(CharacterCreator.Region region) {
        
        InitializeSkinOptionsList();

        return chooseColour(skinColours, allSkinOptions, region);
    }

    public Color chooseHairColour(CharacterCreator.Region region) {

        InitializeHairOptionsList();
        
        // small chance (1/15) that hair is dyed
        int chance = Random.Range(0, 15);
        if(chance == 0) {
            int choice = Random.Range(0, hairColours.dye.Length);
            return hairColours.dye[choice];
        }

        // otherwise, choose based on region
        return chooseColour(hairColours, allHairOptions, region);
    }

    public Color chooseColour(ColourOptions colourOptions, List<int> optionIndexes, CharacterCreator.Region region) {
        
        int regionIndex = (int) region;

        // the centroid indicates the median colour in the colour list
        int colourCentroid = optionIndexes[regionIndex];

        // choose a colour that's +- 3 from the selected index
        int choice = Random.Range(-3, 4);
        choice = colourCentroid + choice;
        if(choice < 0 || choice >= colourOptions.colours.Length)
            choice = colourCentroid;

        return colourOptions.colours[choice];
    }
}
