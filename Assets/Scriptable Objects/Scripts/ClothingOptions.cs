using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ClothingOption", menuName = "Scriptable Objects/ClothingOption")]
public class ClothingOptions : SpriteCategoryOptions
{
    [System.Serializable]
    public struct Options
    {
        public Sprite[] coloured, uncoloured;
    }
    public Options girls, boys, unisex;

    public Color[] colourOptions; 

    public Tuple<Sprite, Color> chooseOption(bool isGirl) {

        // choose between coloured (0) or uncoloured (1) 
        int choice = UnityEngine.Random.Range(0, 2);

        Sprite[] colouredOptions = combineArrays(getGeneredColoured(isGirl), unisex.coloured);
        Sprite[] uncolouredOptions = combineArrays(getGenderedUncoloured(isGirl), unisex.uncoloured);
        
        // return a sprite

        if(uncolouredOptions.Length == 0)                                                       // coloured
            return Tuple.Create(chooseRandom(colouredOptions), Color.white);

        if(colouredOptions.Length == 0)
            return Tuple.Create(chooseRandom(uncolouredOptions), chooseColour());            // uncoloured

        if(choice == 0)                                                                         // coloured
            return Tuple.Create(chooseRandom(colouredOptions), Color.white);
        
        return Tuple.Create(chooseRandom(uncolouredOptions), chooseColour());         // uncoloured
    }

    Sprite[] getGeneredColoured(bool isGirl) {
        if(isGirl)
            return girls.coloured;
        return boys.coloured;
    }

    Sprite[] getGenderedUncoloured(bool isGirl) {
        if(isGirl)
            return girls.uncoloured;
        return boys.uncoloured;
    }
    
    Color chooseColour() {
        int choice = UnityEngine.Random.Range(0, colourOptions.Length);
        return colourOptions[choice];
    }
}
