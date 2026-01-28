using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class book : MonoBehaviour
{

    [SerializeField] 
    private int openingPage = 0;                                            // choose which page to open to the first time (0 for closed book)

    [SerializeField]
    private GameObject pagePrefab;                                          // used to add a new page

    public GameObject openBookObject, closedBookObject, bookObject;                    // the book gameObjects
    public GameObject rightPageContainer, leftPageContainer;               // game objects that have the pages as their children (left and right)
    private LinkedList<GameObject> rightPages, leftPages;                   // all existing pages 
    private LinkedListNode<GameObject> currentRightPage, currentLeftPage;   // current visible pages
    private static bool bookClosed = false;

    void Awake() {
        initializeScript();
    }

    public void initializeScript() {
        
        // make sure the correct book sprite is showing
        if(openingPage == 0){
            openBookObject.SetActive(false);
            closedBookObject.SetActive(true);
        }
        else {
            openBookObject.SetActive(true);
            closedBookObject.SetActive(false);
        }

        // make sure the page setup is valid
        checkIfSetupIsValid();

        // initialize the list of pages (the ones pre-exiting in the inspector)
        initializePages();

        // open the book to the chosen page
        openBookToPage(openingPage);
    }



    public void initializePages()
    {
        rightPages= new LinkedList<GameObject>();
        leftPages= new LinkedList<GameObject>();

        // initialize right pages
        int rightPageCount = rightPageContainer.transform.childCount;
        for (int i = 0; i < rightPageCount; i++)
        {
            GameObject pageToAdd = rightPageContainer.transform.GetChild(i).gameObject;
            rightPages.AddLast(pageToAdd);
        }
        currentRightPage = rightPages.First;

        // initialize left pages
        int leftPageCount = leftPageContainer.transform.childCount;
        for (int i = 0; i < leftPageCount; i++)
        {
            GameObject pageToAdd = leftPageContainer.transform.GetChild(i).gameObject;
            leftPages.AddLast(pageToAdd);
        }
        currentLeftPage = leftPages.Last;
    }


    public void pageTurn(string side)
    {

        // open book
        if(bookClosed && side == "right")
        {
            openBook();
            return;
        }
        
        // close book
        if(currentLeftPage.Previous == null && side == "left")
        { 
            closeBook();
            return;
        }

        // invalid cases
        if (currentLeftPage.Next == null && side == "right") return;
        if (bookClosed && side == "left") return;


        // normal page turn

        // close the 2 current pages
        currentLeftPage.Value.SetActive(false);
        if(currentRightPage != null)
            currentRightPage.Value.SetActive(false);
            
        // assign the next two pages
        if(side == "right")
        {
            currentRightPage = currentRightPage.Next;
            currentLeftPage = currentLeftPage.Next;
        }
        else
        {
            currentLeftPage = currentLeftPage.Previous;
            currentRightPage = (currentRightPage != null) ? currentRightPage.Previous : rightPages.Last;
        }

        // open the next 2 pages
        if(currentRightPage != null)
            currentRightPage.Value.SetActive(true);
        currentLeftPage.Value.SetActive(true);
    }



    public void closeBook()
    {
        bookClosed = true;

        rightPageContainer.SetActive(false);
        leftPageContainer.SetActive(false);
        
        openBookObject.SetActive(false);
        closedBookObject.SetActive(true);
    }



    public void openBook()
    {
        bookClosed = false;

        openBookObject.SetActive(true);
        closedBookObject.SetActive(false);

        currentLeftPage = leftPages.First;
        currentRightPage = rightPages.First;
        
        rightPageContainer.SetActive(true);
        leftPageContainer.SetActive(true);

        currentLeftPage.Value.SetActive(true);
        currentRightPage.Value.SetActive(true);
    }



    // use this for appetizer, meal, dessert pages
    public void openBookToPage(int pageNumber)
    {

        // invalid cases
        if(pageNumber < -1 || pageNumber > leftPages.Count-1)
        {
            Debug.LogWarning("Book: Page index is out of range");
            return;
        }

        // close book
        if(pageNumber == -1)
        {
            closeBook();
            return;
        }

        // open book
        if(pageNumber > 0)
            openBook();

        // close current pages
        currentLeftPage.Value.SetActive(false);
        if(currentRightPage != null)
            currentRightPage.Value.SetActive(false);

        // go to the specified page number

        // left page
        currentLeftPage = leftPages.First;
        for(int i = 0; i < pageNumber; i++)
        {
            currentLeftPage = currentLeftPage.Next;
        }
        currentLeftPage.Value.SetActive(true);

        // right page
        if(pageNumber <= rightPages.Count-1)
        {
            currentRightPage = rightPages.First;
            for(int i = 0; i < pageNumber; i++)
            {
                currentRightPage = currentRightPage.Next;
            }
            currentRightPage.Value.SetActive(true);
        }
        else
        {
            currentRightPage = null;
        }
    }



    public void checkIfSetupIsValid()
    {
        // since indexing starts at 0
        openingPage = openingPage - 1;

        // if there are no pages, you need to start with the book closed
        if(rightPageContainer.transform.childCount == 0 && openingPage != -1)
            openingPage = 0;

        // either the number of left and right pages must be the same
        // or there can be one more left page than right
        int rightPageCount = rightPageContainer.transform.childCount;
        int leftPageCount = leftPageContainer.transform.childCount;

        if(!(rightPageCount == leftPageCount) && !(rightPageCount == leftPageCount - 1))
        {
             Debug.LogWarning("!!! For your book pages, please make sure that either: 1) # right pages = # left pages 2) # right pages = # left pages - 1");
        }

        // make sure the page number to open to is correct
        if(openingPage < -1)
        {
            Debug.LogWarning("!!! The opening page for you book is set to " + (openingPage+1) + ". Please choose a page number between 0 and " + leftPageCount + ". (0 for closed book).");
            openingPage = -1;
        }
        else if(openingPage > leftPageCount - 1)
        {
            Debug.LogWarning("!!! The opening page for you book is set to " + (openingPage+1) + " but there are only " + leftPageCount + " pages. Please choose a page number between 0 and " + leftPageCount + ". (0 for closed book).");
            openingPage = -1;
        }
    }

    public void turnOff() {
        bookObject.SetActive(false);
    }

}
