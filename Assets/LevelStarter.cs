using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public MenuInfoCard menuInfoCard;
    public CharacterCarousel characterCarousel;
    public GameObject characterTab, menuTab;
    public static int difficulty = 0;

    void Awake()
    {
        // 1. create the Event on the Menu Info Card
        menuInfoCard.initialize(difficulty);
        MenuInfoCard.HostEvent hostEvent = menuInfoCard.getEvent();

        // 2. initialize the characters
        int numCharacters = hostEvent.numGuests;
        characterCarousel.InitializeCharacters(numCharacters);

        // 3. enable the windows
        characterTab.SetActive(true);
        menuTab.SetActive(true);
    }
}
