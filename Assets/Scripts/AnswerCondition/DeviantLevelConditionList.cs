public class DeviantLevel1 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 1;
    }
}

public class DeviantLevel2 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 2;
    }
}

public class DeviantLevel3 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 3;
    }
}

public class DeviantLevel4 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 4;
    }
}

public class DeviantLevel5 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 5;
    }
}

public class DeviantLevel6 : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return NPCPlayerData.DevianceLevel >= 6;
    }
}
