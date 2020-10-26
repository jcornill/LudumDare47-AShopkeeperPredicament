using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NoSuchStudio.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogData", order = 2)]
[Serializable]
public class DialogData : ScriptableObject
{
    public string ID;
    
    private int currentDialogProgress;
    public List<DialogLine> dialogLines;
    
    public Condition Condition;
    public int ConditionID;

    public Rarity dialogRarity;
    public int numberOfCharacters;
    public SoundData scenarioMusic;
    
    public AnswerData answer;
    
    public void ResetDialogProgress()
    {
        this.currentDialogProgress = 0;
    }

    public DialogLine GetCurrentLine()
    {
        if (this.currentDialogProgress != 0)
        {
            return this.dialogLines[this.currentDialogProgress - 1];
        }

        return this.dialogLines.FirstOrDefault();
    }
    
    public DialogLine GetNextLine()
    {
        if (this.currentDialogProgress >= this.dialogLines.Count)
        {
            return null;
        }

        return this.dialogLines[this.currentDialogProgress++];
    }
    
    public bool IsConditionFulfill()
    {
        Condition condition = GetCondition();
        return condition.AnswerCanBeSeen();
    }

    private Condition GetCondition()
    {
        if (this.Condition != null)
        {
            return this.Condition;
        }

        IEnumerable<Type> list = Assembly.GetAssembly(typeof(Condition)).GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Condition)));
        return list.Skip(this.ConditionID).Select(x => (Condition) Activator.CreateInstance(x)).First();
    }
}

[Serializable]
public class DialogLine
{
    public string ID;
    
    public bool foldOut;
    
    public string EnglishText;
    public string FrenchText;
    public string PortugueseText;

    public int IDCharacterSpeaking;

    public string GetTranslatedText()
    {
        switch (LocalizationService.CurrentLocale)
        {
            case "fr":
                return this.FrenchText;
            case "pt":
                return this.PortugueseText;
            default:
                return this.EnglishText;
        }
    }
}