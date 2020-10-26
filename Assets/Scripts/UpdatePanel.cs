using NoSuchStudio.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePanel : MonoBehaviour
{
    public int majorVersionNumber;
    public int middleVersionNumber;
    public int minorVersionNumber;

    public Image ProgressBar;
    
    private TextMeshProUGUI VersionText;
    private TextMeshProUGUI ChangeText;

    public static bool IsUpdating;

    private string[] updateFixes = new string[]
    {
        "- Fixing shopkeeper NPC",
        "- Fixing weird talking with some NPCs",
        "- Fixing error in shopkeeper NPC AI",
        "- Fixing an issue with the shopkeeper NPC",
        "- Fixing our NPCs dialog branch system",
        "- Fixing shopkeeper",
    };

    // Start is called before the first frame update
    private void Awake()
    {
        this.majorVersionNumber = Random.Range(0, 2);
        this.middleVersionNumber = Random.Range(0, 5);
        this.minorVersionNumber = Random.Range(0, 10);
        this.VersionText = this.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        this.ChangeText = this.transform.Find("FixPhrase").GetComponent<TextMeshProUGUI>();
    }

    public void ShowUpdatePanel()
    {
        IsUpdating = true;
        Time.timeScale = 0;
        NPCPlayerData.Deviance = 0;
        NPCPlayerData.PowerCharge = 0;
        NPCPlayerData.WantedLevel = 0;
        NPCPlayerData.Money = 0;
        this.gameObject.SetActive(true);
        UpdateText();
        this.ProgressBar.fillAmount = 0f;
        GameBlur.MakeBlurEffect(new Color(0.2f, 0.2f, 0.2f), 6, false);
    }

    private void IncreaseVersionNumber()
    {
        float rng = Random.Range(0f, 100f);
        if (rng < 75f)
        {
            this.minorVersionNumber++;
        }
        else if (rng < 97f)
        {
            this.minorVersionNumber = 0;
            this.middleVersionNumber++;
        }
        else
        {
            this.minorVersionNumber = 0;
            this.middleVersionNumber = 0;
            this.majorVersionNumber++;
        }
    }

    private void Update()
    {
        float rng = Random.Range(-0.0025f, 0.005f);
        rng = Mathf.Max(rng, 0);
        this.ProgressBar.fillAmount += rng;
        if (this.ProgressBar.fillAmount == 1)
        {
            this.gameObject.SetActive(false);
            OnUpdateEnd();
        }
    }

    private void OnUpdateEnd()
    {
        Time.timeScale = 1;
        StaticData.PatchCount++;
        GameBlur.MakeBlurEffect(new Color(0.2f, 0.2f, 0.2f), 6, true);
        Usefull.DelayedCall(2f, () =>
        {
            IsUpdating = false;
            ReflectionData data = ReflectionData.GetReflectionFromName("Repatch" + StaticData.PatchCount);
            if (data != null)
            {
                ReflectionBox.ShowSpecificReflection(data);
            }
        });
    }
    
    private void UpdateText()
    {
        this.ChangeText.text = this.updateFixes[Random.Range(0, this.updateFixes.Length)];
        this.VersionText.text = LocalizationService.GetPhraseTranslation("VERSION_UPDATE") + " " + this.majorVersionNumber + "." + this.middleVersionNumber + "." + this.minorVersionNumber;
    }
}
