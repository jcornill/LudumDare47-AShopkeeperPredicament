using UnityEngine;

public class None : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return true;
    }
}

public class FalseCondition : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return false;
    }
}


