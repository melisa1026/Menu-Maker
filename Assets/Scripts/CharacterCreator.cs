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
    public Character character;

    public string name;

    public GameObject[] easyLoves, mediumLoves, hardLoves, easyIntolerances, hardIntolerances;

    public FoodDescription foodDescription;

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
        setFoodRestrictions(LevelStarter.difficulty);
        setLikes(LevelStarter.difficulty);
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

    void setLikes(int difficulty) {

        // higher difficulty = more Loves

        int num = 0;

        if(difficulty == 0) {

            /* 
                difficulty 0:
                - 3 loves 
                - 20% sweet
                - 10% saucy
                - 10% cheesy
                - 10% meaty
                - 10% garlicky
                - 5% crunchy
                - 5% citrusy
                - 5% chewy
                - 5% creamy
                - 5% fruity
                - 5% salty
                - 5% spicy
        */
            for (int i = 0; i < 3; i++) {
                num = UnityEngine.Random.Range(0, 100);
                if(num <= 20)
                    foodDescription.addTaste(FoodIcon.foodIcon.Sweet);
                else if(num <= 30) 
                    foodDescription.addTaste(FoodIcon.foodIcon.Saucy);
                else if(num <= 40)
                    foodDescription.addTaste(FoodIcon.foodIcon.Cheesy);
                else if(num <= 50)
                    foodDescription.addTaste(FoodIcon.foodIcon.Meaty);
                else if(num <= 60)
                    foodDescription.addTaste(FoodIcon.foodIcon.Garlicky);
                else if(num <= 70)
                    foodDescription.addTaste(FoodIcon.foodIcon.Crunchy);
                else if(num <= 75)
                    foodDescription.addTaste(FoodIcon.foodIcon.Citrusy);
                else if(num <= 80)
                    foodDescription.addTaste(FoodIcon.foodIcon.Chewy);
                else if(num <= 85)
                    foodDescription.addTaste(FoodIcon.foodIcon.Creamy);
                else if(num <= 90)
                    foodDescription.addTaste(FoodIcon.foodIcon.Fruity);
                else if(num <= 95)
                    foodDescription.addTaste(FoodIcon.foodIcon.Salty);
                else
                    foodDescription.addTaste(FoodIcon.foodIcon.Spicy);
            }

        }
        else if (difficulty == 1) {
            /* 
                difficulty 2:
                - 2-3 loves 
                - 10% sweet
                - 10% saucy
                - 10% cheesy
                - 5% meaty
                - 5% garlicky
                - 5% crunchy
                - 5% citrusy
                - 5% chewy
                - 5% creamy
                - 10% fruity
                - 5% salty
                - 5% spicy
                - 20% none
            */

            for(int i = 0; i < 3; i++) {
                num = UnityEngine.Random.Range(0, 100);
                if(num <= 10)
                    foodDescription.addTaste(FoodIcon.foodIcon.Sweet);
                else if(num <= 20) 
                    foodDescription.addTaste(FoodIcon.foodIcon.Saucy);
                else if(num <= 30)
                    foodDescription.addTaste(FoodIcon.foodIcon.Cheesy);
                else if(num <= 35)
                    foodDescription.addTaste(FoodIcon.foodIcon.Meaty);
                else if(num <= 40)
                    foodDescription.addTaste(FoodIcon.foodIcon.Garlicky);
                else if(num <= 45)
                    foodDescription.addTaste(FoodIcon.foodIcon.Crunchy);
                else if(num <= 50)
                    foodDescription.addTaste(FoodIcon.foodIcon.Citrusy);
                else if(num <= 55)
                    foodDescription.addTaste(FoodIcon.foodIcon.Chewy);
                else if(num <= 60)
                    foodDescription.addTaste(FoodIcon.foodIcon.Creamy);
                else if(num <= 70)
                    foodDescription.addTaste(FoodIcon.foodIcon.Fruity);
                else if(num <= 75)
                    foodDescription.addTaste(FoodIcon.foodIcon.Salty);
                else if(num <= 80)
                    foodDescription.addTaste(FoodIcon.foodIcon.Spicy);
            }
        }
        else if (difficulty == 2) {
            /* 
                difficulty 2:
                - 1-3 loves 
                - equal everything else
                - 30% none
            */
            
            for(int i = 0; i < 3; i++) {

                num = UnityEngine.Random.Range(0, 100);

                if(num <= 70) {
                    num = UnityEngine.Random.Range(0, 13);
                    if(num != 10) // no sour recipes yet
                        foodDescription.addTaste((FoodIcon.foodIcon) num);
                }
            }
        }

    }

    public string getName() {
        return name;
    }

    public void copyCharacter(CharacterCreator other) {
        
        // copy sprites
        character.shirt.sprite = other.character.shirt.sprite;
        character.pants.sprite = other.character.pants.sprite;
        character.shoes.sprite = other.character.shoes.sprite;
        character.eyebrows.sprite = other.character.eyebrows.sprite;
        character.body.sprite = other.character.body.sprite;
        character.eyes.sprite = other.character.eyes.sprite;
        character.nose.sprite = other.character.nose.sprite;
        character.mouth.sprite = other.character.mouth.sprite;
        character.hairFront.sprite = other.character.hairFront.sprite;
        character.hairBack.sprite = other.character.hairBack.sprite;

        // copy colours
        Color hairColour = other.character.hairFront.color;
        character.hairFront.color = hairColour;
        character.hairBack.color = hairColour;
        character.eyebrows.color = hairColour;

        character.body.color = other.character.body.color;

        character.shirt.color = other.character.shirt.color;
        character.pants.color = other.character.pants.color;
    }
}
