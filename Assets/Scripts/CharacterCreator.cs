using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public enum Region
    {
        Ambiguous = 0,  // AKA western english or NA, 
        NativeAmerica = 1, 
        SouthAmerica = 2,
        EastAsia = 3,
        SouthAsia = 4,
        Italy = 5,
        MiddleEast = 6,
        French = 7,
        Africa = 8,
        NorthernEurope = 9
    }

    public CharacterCreationData characterCreationData;
    public bool characterInitialized = false;

    [System.Serializable]
    public struct Character
    {
        public Image shirt, pants, shoes, eyebrows, body, eyes, nose, mouth, hairFront, hairBack, facialHair;
    }

    [SerializeField]
    Character character;

    public string name;

    public GameObject[] easyLoves, mediumLoves, hardLoves, easyIntolerances, hardIntolerances;

    FoodDescription foodDescription;

    public void CreateCharacter() {

        // 1. choose a random region
        Region region = (Region) UnityEngine.Random.Range(0, 10);

        // 2. choose boy or girl
        int gender = UnityEngine.Random.Range(0, 2);
        bool isGirl = true;
        if(gender == 0)
            isGirl = false;

        CreateCharacter(isGirl, region);

        // 3. Initialize the food description
        foodDescription = ScriptableObject.CreateInstance<FoodDescription>();
        foodDescription.initialize();
        setFoodRestrictions(1);
    }

    public void CreateCharacter(bool isGirl, Region region) {

        // 3.  get the clothes

        // shirt
        setClothingItem(character.shirt, characterCreationData.shirts, isGirl);

        // pants
        setClothingItem(character.pants, characterCreationData.pants, isGirl);

        // shoes
        setClothingItem(character.shoes, characterCreationData.shoes, isGirl);


        // 4. initialize the body parts

        // body + face shape
        setBodyPart(character.body, characterCreationData.body, isGirl, region);

        // back hair
        setBodyPart(character.hairBack, characterCreationData.hairBack, isGirl, region);

        // front hair
        setBodyPart(character.hairFront, characterCreationData.hairFront, isGirl, region);

        // eyes
        setBodyPart(character.eyes, characterCreationData.eyes, isGirl, region);

        // mouth
        setBodyPart(character.mouth, characterCreationData.mouth, isGirl, region);

        // nose
        setBodyPart(character.nose, characterCreationData.nose, isGirl, region);

        // // facial hair
        // // setBodyPart(character.facialHair, characterCreationData.facialHair, isGirl, region);
        
        // eyebrows
        setBodyPart(character.eyebrows, characterCreationData.eyebrows, isGirl, region);


        // 5. choose colours based on region

        // hair colour
        Color hairColour = characterCreationData.colours.chooseHairColour(region);
        character.hairFront.color = hairColour;
        character.hairBack.color = hairColour;
        character.eyebrows.color = hairColour;
        // character.facialHair.color = hairColour;

        // skin colour
        Color skinColour = characterCreationData.colours.chooseSkinColour(region);
        character.body.color = skinColour;
        
        // 6. choose a character name
        name = characterCreationData.names.chooseName(isGirl, region);

        // 7. mark that it's finished
        characterInitialized = true;
    }

    public void CreateCharacter(CharacterCreationPrompt_DeleteLater x) {
        CreateCharacter(x.isGirl, x.region);
    }

    void setClothingItem(Image clothingSprite, ClothingOptions clothingOptionScript, bool isGirl) {

        Tuple<Sprite, Color> choice = clothingOptionScript.chooseOption(isGirl);    // returns clothing option and its colour
        clothingSprite.sprite = choice.Item1;
        clothingSprite.color = choice.Item2;
    }

    void setBodyPart(Image bodyPartSprite, BodyOptions bodyOptionsScript, bool isGirl, Region region) {
        bodyPartSprite.sprite = bodyOptionsScript.chooseOption(isGirl, region);
    }

    // difficulties: 0, 1, 2
    void setLoves(int difficulty) {

    }

    // difficulties: 0, 1, 2
    void setFoodRestrictions(int difficulty) {

        int num = 0;

        if(difficulty == 0) {

            /* 
                difficulty 0: 
                    - 25% chance of a nut allergy
                    - 10% change of a vegetarian
                    - 5% change gluten free
                    - 5% change vegan
            */

            num = UnityEngine.Random.Range(0, 100);
            if(num <= 25)
                foodDescription.addIntolerance(FoodIcon.foodIcon.NutFree);
            else if(num <= 35) 
                foodDescription.addIntolerance(FoodIcon.foodIcon.Vegetatian);
            else if(num <= 40)
                foodDescription.addIntolerance(FoodIcon.foodIcon.GlutenFree);
            else if(num <= 45)
                foodDescription.addIntolerance(FoodIcon.foodIcon.Vegan);

        }
        else if(difficulty == 1) {

            /* 
                difficulty 1: 
                    - 30% 2 possible intolerances
                    - 15% chance of a nut allergy
                    - 15% change of a vegetarian
                    - 10% change gluten free
                    - 10% change vegan
            */

            num = UnityEngine.Random.Range(0, 100);
            int numPossibleIntolerances = (num < 30) ? 1 : 2;

            for(int i = 0; i < numPossibleIntolerances; i++) {
                num = UnityEngine.Random.Range(0, 100);
                if(num <= 15)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.NutFree);
                else if(num <= 30) 
                    foodDescription.addIntolerance(FoodIcon.foodIcon.Vegetatian);
                else if(num <= 40)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.GlutenFree);
                else if(num <= 50)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.Vegan);
            }
        }
        else {

            /* 
                difficulty 2: 
                    - 30% 3 possible intolerances
                    - 15% chance of a nut allergy
                    - 15% change of a vegetarian
                    - 15% change gluten free
                    - 15% change vegan
            */

            num = UnityEngine.Random.Range(0, 100);
            int numPossibleIntolerances = (num < 30) ? 1 : 3;

            for(int i = 0; i < numPossibleIntolerances; i++) {
                num = UnityEngine.Random.Range(0, 100);
                if(num <= 15)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.NutFree);
                else if(num <= 30) 
                    foodDescription.addIntolerance(FoodIcon.foodIcon.Vegetatian);
                else if(num <= 45)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.GlutenFree);
                else if(num <= 60)
                    foodDescription.addIntolerance(FoodIcon.foodIcon.Vegan);
            }

        }

    }

    public string getName() {
        return name;
    }
}
