using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NoSuchStudio.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ReflectionData", order = 3)]
[Serializable]
public class ReflectionData : ScriptableObject
{
    public string EnglishText;
    public string FrenchText;
    public string PortugueseText;
    
    [HideInInspector]
    public int ConditionID;
    public Condition Condition;

    public static ReflectionData GetReflectionFromName(string name)
    {
        foreach (ReflectionData data in GameDataHolder.data.reflections)
        {
            if (data.name == name)
            {
                return data;
            }
        }

        return null;
    }
    
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
