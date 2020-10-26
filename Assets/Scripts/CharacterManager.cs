using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    
    public GameCharacterData[] playerCharacters;
    public GameCharacterData[] characterList;

    public Sprite[] CharacterFaceSprites;
    public GameObject[] characterPrefabs;

    private string[] CharNameList = new string[]
    {
        "XxDarkSasukexX",
        "StupidPlayer",
        "WolfSniper",
        "DeathKiller",
        "ProKiller",
        "NoobSlayer",
        "DragonSlayer",
        "BeerLover",
        // Gabeux
        "Zer0Z0n3",
        "Wazda",
        "PaoloMilkinhu",
        "JackBeolvi",
        "KimKatakasadas",
        "NotchHeartsRolf",
        "BringWurmBack",
        "AchsanDakho2127",
        "RebellionPawn",
        "MaxisWill",
        "ToadyTwo",
        "Bay13er",
        "FitGirl",
        "SSethWasHere",
        "Urist McGamer",
        "PewDiePoo",
        "Cuti3Pie",
        "RobbazIsSlacking",
        "SpaceStationer13",
        "Pam",
        "FarmerHaxus",
        "MakeHazeronGreatAgain",
        "ArgonsAreFriends",
        "ParanidHunter",
        "XenonSalvager87",
        "BladeWalker1982",
        "TooHuman",
        "NotANumber",
        "MuckyFootLives",
        "BullfrogSendsItsRegards",
        "FallenLionhead",
        "Headless Iguana",
        "SESshouldHireUs3",
        "StrangeLoopJohn",
        "EcoWasModded",
        "StinkyFinger",
        "BelairSwordsman",
        "Commodore65",
        "Kraftwerker",
        "DaftPunkPls",
        "ParkourBoi93",
        "Old King Tibianus",
        "DevolverSenpai",
        "Curve-ComeOn",
        "ParadoxPublishUs",
        "LudumDarer47",
        "DreamChaser47",
        "Believer47",
        "NotTheImpostor",
        "MoistCycl1kal",
        "CharlieTheMoist",
        "OverflowedStack",
        "SingletonsAreCool",
        "JustUseStatic",
        "EricBaroneFan",
        "ConcernedGrape",
        "TynanOfTheRim",
        "Egosofter",
        "BerndLehahner",
        "HelgeXKautz",
        "KiritoSux",
        "LelouchDidNothingWrong",
        "MercyMain86",
        // Mari
        "Bass God 504",
        "PeePee PooPoo",
        "Carnicer0",
        "aligote326",
        "McCreamy",
        "Cat Lover",
        "SpaceCowboy91",
        "Kotaro loves Hanna",
        "P3t3r Pot4to Br34d",
        "ProPlayer420",
        "TomatoHater47",
        "Xx. Cute princesS .xX",
        // Girl Names
        "SheWhoDontMiss",
        "SworrdGurrl",
        "SevenofNine",
        "CandyEater",
        "SmokinHotChick",
        "PistolPrincess",
        "TiaraONtop",
        "SuperGurl3000",
        "GlitterBowwoman",
        "PurpleBunnySlippers",
        "ImTheBirthdayGirl",
        "FlameThrower",
        "SmittenKitten66",
        "ArcherPrincess",
        "FabulousGladiator",
        "BluberriMuffins",
        "CutiePatootie22",
        "MsPiggysREVENGE",
        "StabGal33",
        // Console Gamer
        "SeekNDstroy",
        "Bulletz4Breakfast",
        "BigDamnHero",
        "LaidtoRest",
        "IronMAN77",
        "Xenomorphing",
        "PennywiseTheClown",
        "BluntMachete",
        "SniperLyfe",
        "SilentWraith",
        "BloodyAssault",
        "FightClubAlum",
        "KillSwitch",
        "ExecuteElectrocute",
        "BadBaneCat",
        "IndominusRexxx",
        "AzogtheDefiler",
    };

    private int index;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnFakePlayer());
    }

    private IEnumerator SpawnFakePlayer()
    {
        while (true)
        {
            this.index = Random.Range(0, this.characterPrefabs.Length);
            Instantiate(this.characterPrefabs[this.index]);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public GameCharacterData GetRandomCharacterFromList()
    {
        int rng = Random.Range(0, this.characterList.Length);
        return this.characterList[rng];
    }

    public GameCharacterData GenerateRandomCharacter()
    {
        return GenerateRandomCharacter(1, 99);
    }
    
    public GameCharacterData GenerateRandomCharacter(int minLevel, int maxLevel)
    {
        return GenerateCharacter(this.CharNameList[Random.Range(0, this.CharNameList.Length)], Random.Range(minLevel, maxLevel + 1), this.CharacterFaceSprites[this.index]);
    }

    public GameCharacterData GenerateCharacter(string charName, int level, Sprite face)
    {
        GameCharacterData data = ScriptableObject.CreateInstance<GameCharacterData>();
        data.characterFace = face;
        data.characterLevel = level;
        data.characterName = charName;
        data.voicePitch = Random.Range(0.4f, 1f);
        return data;
    }
}
