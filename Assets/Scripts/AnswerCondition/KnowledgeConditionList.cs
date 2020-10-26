public class SkillKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Skills;
    }
}

public class ExperienceKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Experience;
    }
}

public class GameKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Game;
    }
}

public class GoldKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Gold;
    }
}

public class LevelKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Level;
    }
}

public class LootKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Loot;
    }
}

public class ManaKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Mana;
    }
}

public class NightKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Night;
    }
}

public class RainKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Rain;
    }
}

public class WalkingKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Walking;
    }
}

//Ant. Knowledge
public class DroneKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Drones;
    }
}

public class GhostsKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.Ghosts;
    }
}

public class ControlSystemKnowledge : Condition
{
    public override bool AnswerCanBeSeen()
    {
        return Knowledge.MCS;
    }
}


