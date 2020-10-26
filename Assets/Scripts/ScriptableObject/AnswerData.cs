using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NoSuchStudio.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnswerData", order = 3)]
[Serializable]
public class AnswerData : ScriptableObject
{
    public string ID;
    
    public List<AnswerLine> answers;
}

[Serializable]
public class AnswerLine
{
    public string ID;
    
    public bool foldOut;

    public AnswerInteractionType answerInteraction;
    public int answerSeverity;
    public float PowerChange;
    public float DevianceChange;
    public float WantedLevelChange;
    public int MoneyChange;
    
    public string EnglishText;
    public string FrenchText;
    public string PortugueseText;
    
    public List<DialogData> ResultDialogs;
    public int ConditionID;
    public Condition Condition;

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
public abstract class Condition
{
    public abstract bool AnswerCanBeSeen();
}

public enum AnswerInteractionType
{
    Custom,
    ActingNormally,
    ActingNormallyBad,
    ActingNormallyGood,
    ActingNormallyForcibly,
    Talk,
    Ignore,
    Money,
    Destroy
}