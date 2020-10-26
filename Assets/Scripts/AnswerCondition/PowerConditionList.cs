public class TalkWeirdThingsPower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.TalkWeirdThings;
    }
}

public class IgnorePeoplePower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.IgnorePeople;
    }
}
public class MoneyCollectionPower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.MoneyCollection;
    }
}
public class RequirementForDialogsPower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.RequirementForDialogs;
    }
}
public class DestroyPeoplePower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.DestroyPeople;
    }
}
public class LeavingTheGamePower : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Powers.LeavingTheGame;
    }
}