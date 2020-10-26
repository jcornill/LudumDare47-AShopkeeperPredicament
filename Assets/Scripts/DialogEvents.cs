using UnityEngine;

public static class DialogEvents
{
    public static void OnDialogSelected(DialogData data)
    {
        
    }
    
    public static void OnDialogStart(DialogData data)
    {
        
    }
    
    public static void OnDialogEnd(DialogData data)
    {
        if (Ending.EndingReflection > 0)
        {
            return;
        }
        if (data.ID.Contains("InstanceChange"))
        {
            DialogManager.DisconnectCharacter(DialogManager.Characters[0]);
        }

        if (data.ID.Contains("WalkingScenario"))
        {
            DialogManager.DisconnectCharacter(DialogManager.Characters[0]);
        }

        if (data.ID.Contains("TheMissingShopkeeper"))
        {
            Ending.EndingReflection++;
            ReflectionBox.ShowReflectionText("Thank you.");
            return;
        }

        if (data.ID.Contains("Rebellion"))
        {
            DialogManager.DisconnectCharacter(DialogManager.Characters[0]);
        }

        if (data.ID.Contains("Knowledge"))
        {
            DialogPicker.RemoveDialog(data);
        }
        
        // Make stuff execute 1 sec after the end of the dialog to delay the thoughts
        Usefull.DelayedCall(1, () =>
        {
            if (Ending.EndingStart)
            {
                Ending.EndingCanReallyStart = true;
                return;
            }
            StaticData.NumberOfPlayerServed++;
            if (NPCLevelProgression.CheckLevelUp() == false)
            {
                if (DeathVerification.CheckDeath() == false)
                {
                    ReflectionBox.TryShowingReflection();
                }
            }

            DialogManager.CanSelectDialog = true;
        });
    }
    
    public static void OnDialogLineStart(DialogData data, DialogLine line)
    {
        
    }
    
    public static void OnDialogLineEnd(DialogData data, DialogLine line)
    {
        if (line.ID.Contains("ReportYou"))
        {
            NPCPlayerData.ChangeWantedLevel(0.4f);
            NPCPlayerData.Deviance += 0.5f;
            NPCPlayerData.PowerCharge += 0.5f;
        }
        if (line.ID.Contains("RageQuit"))
        {
            NPCPlayerData.PowerCharge += 0.6f;
            NPCPlayerData.Deviance += 0.5f;
            NPCPlayerData.ChangeWantedLevel(0.25f);
        }
        if (line.ID.Contains("AngryAccept") || line.ID.Contains("DisappointedAccept"))
        {
            NPCPlayerData.PowerCharge += 0.45f;
            NPCPlayerData.Deviance += 0.2f;
            NPCPlayerData.ChangeWantedLevel(0.1f);
        }
        if (line.ID.Contains("Bug"))
        {
            NPCPlayerData.PowerCharge += 0.3f;
            NPCPlayerData.Deviance += 0.3f;
            NPCPlayerData.ChangeWantedLevel(0.15f);
        }
        if (line.ID.Contains("Thanks"))
        {
            NPCPlayerData.PowerCharge += 0.15f;
            NPCPlayerData.Deviance += 0.1f;
            NPCPlayerData.ChangeWantedLevel(-0.05f);
        }
        if (line.ID.Contains("Confused"))
        {
            NPCPlayerData.PowerCharge += 0.3f;
            NPCPlayerData.Deviance += 0.2f;
            NPCPlayerData.ChangeWantedLevel(0.1f);
        }
        

        // Rebellion
        if (data.ID.Contains("Rebellion"))
        {
            NPCPlayerData.PowerCharge += 0.75f;
            NPCPlayerData.Deviance += 0.5f;
            NPCPlayerData.ChangeWantedLevel(0.25f);
        }

        Mathf.Clamp01(NPCPlayerData.PowerCharge);

        //Knowledge
        if (line.ID.Contains("DronesScenario"))
            Knowledge.Drones = true;

        if (line.ID.Contains("ExperienceScenario"))
            Knowledge.Experience = true;

        if (line.ID.Contains("GameScenario"))
            Knowledge.Game = true;

        if (line.ID.Contains("GhostsScenario"))
            Knowledge.Ghosts = true;

        if (line.ID.Contains("GoldScenario"))
            Knowledge.Gold = true;

        if (line.ID.Contains("LevelScenario"))
            Knowledge.Level = true;

        if (line.ID.Contains("LootScenario"))
            Knowledge.Loot = true;

        if (line.ID.Contains("ManaScenario"))
            Knowledge.Mana = true;

        if (line.ID.Contains("MCSScenario"))
            Knowledge.MCS = true;

        if (line.ID.Contains("NightScenario"))
            Knowledge.Night = true;

        if (line.ID.Contains("RainScenario"))
            Knowledge.Rain = true;

        if (line.ID.Contains("SkillsScenario"))
            Knowledge.Skills = true;

        if (line.ID.Contains("WalkingScenario"))
            Knowledge.Walking = true;

        // The Final Test
        if (line.ID.Contains("TheFinalTest"))
            StaticData.UniqueScenarioComplete = true;

    }
    
    public static void OnAnswerDialogStart(DialogData data, AnswerData answers)
    {
        
    }
    
    public static void OnAnswerDialogSelected(DialogData data, AnswerData answers, AnswerLine selectedAnswer)
    {

    }
}
