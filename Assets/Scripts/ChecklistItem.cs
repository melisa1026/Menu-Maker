using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class ChecklistItem : MonoBehaviour
{
    public TMP_Text foodTypeText;
    public GameObject checkmark, backButton;
    MealManager.FoodType foodType;
    bool isChecked = false;
    Recipe associatedRecipe;

    public void initialize(MealManager.FoodType foodType) {

        this.foodType = foodType;

        foodTypeText.text = foodType.ToString();
    }

    public void check(Recipe associatedRecipe) {
        isChecked = true;
        this.associatedRecipe = associatedRecipe;

        // animate the check marking
        StartCoroutine(checkAnim());
    }

    IEnumerator checkAnim() {

        yield return new WaitForSeconds(0.5f);
        
        checkmark.SetActive(true);
        Vector3 size = checkmark.transform.localScale;
        checkmark.transform.localScale = size*0.5f;
        checkmark.transform.DOScale(size, 0.3f);

        yield return new WaitForSeconds(0.5f);
        
        backButton.SetActive(true);
    }

    public void uncheck() {
        isChecked = false;

        associatedRecipe = null;

        checkmark.SetActive(false);
        backButton.SetActive(false);

        GameObject.Find("Meal Manager").gameObject.GetComponent<MealManager>().uncount(foodType);
    }

    public bool getIsChecked() {
        return isChecked;
    }

    public Recipe getAssociatedRecipe() {
        return associatedRecipe;
    }

    public void viewItem() {

        // close all books
        GameObject.Find("Book Container").gameObject.GetComponent<BookContainer>().closeAllBooks();

        // open the correct book
        associatedRecipe.book.SetActive(true);

        // go to the right page
        associatedRecipe.book.GetComponent<BookPrefab>().getBookScript().openBookToPage(associatedRecipe.pageNumber);
    }
}
