using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EthnicOption", menuName = "Scriptable Objects/EthnicOption")]
public class BodyOptions : SpriteCategoryOptions
{
    [System.Serializable]
    public class Options
    {
        public Sprite[] girl, boy, unisex;
        
    }

    public Options ambiguous, nativeAmerica, southAsia, eastAsia, middleEast, northernEurope, italy, france,
                africa, southAmerica;

    List<Options> allOptions; 
    
    void InitializeOptionsList() {

        allOptions = new List<Options>();

        // this list is in the same order as the CharacterCreator.Region enum
        allOptions.Add(ambiguous);
        allOptions.Add(nativeAmerica);
        allOptions.Add(southAmerica);
        allOptions.Add(eastAsia);
        allOptions.Add(southAsia);
        allOptions.Add(italy);
        allOptions.Add(middleEast);
        allOptions.Add(france);
        allOptions.Add(africa);
        allOptions.Add(northernEurope);
    }


    public Sprite chooseOption(bool isGirl, CharacterCreator.Region region) {

        InitializeOptionsList();
        
        int regionIndex = (int) region;

        // combine the region options with the ambiguous options
        Sprite[] combinedOptions = combineArrays(getOptionsByGender(isGirl, allOptions[regionIndex]), getOptionsByGender(isGirl, ambiguous));

        return chooseRandom(combinedOptions);
    }

    public Sprite[] getOptionsByGender(bool isGirl, Options options) {
        Sprite[] genderedOptions;
        if(isGirl)
            genderedOptions = options.girl;
        else 
            genderedOptions = options.boy;

        return combineArrays(genderedOptions, options.unisex);
    }

}
