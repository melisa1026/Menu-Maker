using UnityEngine;

[CreateAssetMenu(fileName = "AllCharacterInfo", menuName = "Scriptable Objects/AllCharacterInfo")]
public class CharacterCreationData : ScriptableObject
{
    public ClothingOptions shirts, pants, shoes;
    public BodyOptions body, eyes, nose, mouth, hairFront, hairBack, facialHair, eyebrows;
    public EthnicColourList colours;
    public CharacterNames names;

}
