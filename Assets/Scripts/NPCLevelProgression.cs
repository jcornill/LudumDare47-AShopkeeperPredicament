using System;
using UnityEngine;

public class NPCLevelProgression : MonoBehaviour
{
    public GameObject PowerBar;
    public GameObject DevianceBar;
    public GameObject DeviantLevel;
    public GameObject WantedLevel;
    public GameObject Money;

    private static NPCLevelProgression Instance;

    private void Awake()
    {
        Instance = this;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }
#endif

    public static bool CheckLevelUp()
    {
        if (NPCPlayerData.DevianceLevel == 0 && StaticData.NumberOfPlayerServed >= 2)
        {
            LevelUp();
            return true;
        }
        if (NPCPlayerData.DevianceLevel == 1 && NPCPlayerData.PowerCharge >= 0.5f)
        {
            LevelUp();
            return true;
        }
        //if (NPCPlayerData.DevianceLevel == 2 && Math.Abs(NPCPlayerData.Deviance - 1f) < 0.001f)
        if (NPCPlayerData.DevianceLevel == 2 && NPCPlayerData.PowerCharge >= 0.5f && NPCPlayerData.Deviance >= 0.5f)
        {
            LevelUp();
            return true;
        }
        //if (NPCPlayerData.DevianceLevel == 3 && Math.Abs(NPCPlayerData.Deviance - 1f) < 0.001f && Math.Abs(NPCPlayerData.PowerCharge - 1f) < 0.001f)
        if (NPCPlayerData.DevianceLevel == 3 && NPCPlayerData.PowerCharge >= 0.75f && NPCPlayerData.Deviance >= 0.75f && NPCPlayerData.Money >= 100)
        {
            LevelUp();
            return true;
        }
        if (NPCPlayerData.DevianceLevel == 4 && StaticData.UniqueScenarioComplete)
        {
            LevelUp();
            return true;
        }
        if (NPCPlayerData.DevianceLevel == 5 && Math.Abs(NPCPlayerData.WantedLevel - 1f) < 0.001f)
        {
            LevelUp();
            return true;
        }
        return false;
    }

    private static void LevelUp()
    {
        NPCPlayerData.PowerCharge = 0;
        NPCPlayerData.Deviance = 0;
        NPCPlayerData.Money = 0;
        NPCPlayerData.DevianceLevel++;
        if (NPCPlayerData.DevianceLevel == 1)
        {
            UnlockPower();
            Powers.TalkWeirdThings = true;
        }
        if (NPCPlayerData.DevianceLevel == 2)
        {
            UnlockDeviance();
            Powers.IgnorePeople = true;
        }
        if (NPCPlayerData.DevianceLevel == 3)
        {
            UnlockDeviantLevel();
            UnlockWantedLevel();
            UnlockMoney();
            Powers.MoneyCollection = true;
        }

        if (NPCPlayerData.DevianceLevel == 4)
        {
            Powers.RequirementForDialogs = true;
        }

        if (NPCPlayerData.DevianceLevel == 5)
        {
            Powers.DestroyPeople = true;
        }

        if (NPCPlayerData.DevianceLevel == 6)
        {
            Powers.LeavingTheGame = true;
        }

        ReflectionBox.ShowSpecificReflection(ReflectionData.GetReflectionFromName("Level" + NPCPlayerData.DevianceLevel));
    }
    

    public static void UnlockPower()
    {
        Instance.PowerBar.SetActive(true);
    }
    
    public static void UnlockDeviance()
    {
        Instance.DevianceBar.SetActive(true);
    }
    
    public static void UnlockDeviantLevel()
    {
        Instance.DeviantLevel.SetActive(true);
    }
    
    public static void UnlockWantedLevel()
    {
        Instance.WantedLevel.SetActive(true);
    }
    
    public static void UnlockMoney()
    {
        Instance.Money.SetActive(true);
    }
}
