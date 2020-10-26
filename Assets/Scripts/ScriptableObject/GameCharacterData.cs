using NoSuchStudio.Localization;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class GameCharacterData : ScriptableObject
{
    public string characterName;
    public bool translateName;
    public int characterLevel;
    public Sprite characterFace;
    public float voicePitch;

    public string GetFullName(bool withLevel)
    {
        string name = this.characterName;

        if (this.translateName)
        {
            name = LocalizationService.GetPhraseTranslation(name);
        }
        
        if (this.characterLevel < 0 || withLevel == false)
        {
            return name;
        }
        string levelPrefix = LocalizationService.GetPhraseTranslation("LEVEL_PREFIX");
        return name + " " + levelPrefix + this.characterLevel;
    }
}