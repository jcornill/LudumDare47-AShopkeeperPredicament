using UnityEngine;

public static class StaticData
{
    public static int NumberOfPlayerServed = 0;
    public static float PowerUsed = 0;
    public static int PatchCount = 0;
    public static bool UniqueScenarioComplete;
}

public static class NPCPlayerData
{
    public static float PowerCharge;
    public static float Deviance;
    public static int DevianceLevel;
    public static float WantedLevel;
    public static int Money;

    public static void ChangeWantedLevel(float changeValue)
    {
        if (changeValue < 0)
        {
            if (Drone.IsHere)
            {
                changeValue *= 2;
            }

            if (Ghost.IsHere)
            {
                changeValue *= 2;
            }

            if (MSC.IsHere)
            {
                changeValue *= 2;
            }
        }

        WantedLevel += changeValue;
        WantedLevel = Mathf.Clamp01(WantedLevel);
    }
}

public static class Powers
{
    public static bool TalkWeirdThings;
    public static bool IgnorePeople;
    public static bool MoneyCollection;
    public static bool RequirementForDialogs;
    public static bool DestroyPeople;
    public static bool LeavingTheGame;
}

public static class Knowledge
{
    public static bool Skills;
    public static bool Game;
    public static bool Night;
    public static bool Gold;
    public static bool Experience;
    public static bool Loot;
    public static bool Mana;
    public static bool Level;
    public static bool Rain;
    public static bool Walking;

    public static bool Drones;
    public static bool Ghosts;
    public static bool MCS;
}
