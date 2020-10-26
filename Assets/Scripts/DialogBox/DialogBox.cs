using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum CharacterSpeaking
{
    Left,
    Right
}

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    private SimpleDialog simpleDialog;
    [SerializeField]
    private AnswerDialogBox answerDialogBox;
    [SerializeField]
    private Image leftChar;
    [SerializeField]
    private Image rightChar;
    [SerializeField]
    private TextMeshProUGUI leftCharName;
    [SerializeField]
    private TextMeshProUGUI rightCharName;
    [SerializeField]
    private Image dialogBackground;
    private DialogData currentDialog;
    
    private GameCharacterData[] currentCharacters;
    
    public bool DialogInProgress { get; private set; }
    
    public delegate void DialogEndEvent();
    public DialogEndEvent dialogEnd;

    public static GameCharacterData CurrentlySpeakingChar;
    public static SoundData CurrentlyDialogPlayingMusic;

    private void OnEnable()
    {
        this.DialogInProgress = false;
        HideDialogBox();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.DialogInProgress)
        {
            if (this.simpleDialog.IsDisplayingText)
            {
                this.simpleDialog.WriteEverything();
            }
            else
            {
                DialogEvents.OnDialogLineEnd(this.currentDialog, this.currentDialog.GetCurrentLine());
                ProgressDialog();
            }
        }
    }

    private void ProgressDialog()
    {
        DialogLine nextLine = this.currentDialog.GetNextLine();
        if (nextLine == null)
        {
            this.DialogInProgress = false;
            HideDialogBox();
            ProcessAnswer();
        }
        else
        {
            CharacterSpeaking speaker = CharacterSpeaking.Right;
            if (nextLine.IDCharacterSpeaking == 0)
            {
                speaker = CharacterSpeaking.Left;
            }

            CurrentlySpeakingChar = this.currentCharacters[nextLine.IDCharacterSpeaking];
            DialogEvents.OnDialogLineStart(this.currentDialog, nextLine);
            ShowSimpleDialog(speaker, this.currentCharacters[nextLine.IDCharacterSpeaking], nextLine.GetTranslatedText());
        }
    }

    public void PlayDialog(DialogData dialog, params GameCharacterData[] characters)
    {
        if (dialog == null)
        {
            HideDialogBox();
            this.DialogInProgress = false;
            DialogEnd();
            return;
        }
        if (this.DialogInProgress)
        {
            Debug.LogError("A dialog is already in progress");
            return;
        }
        
        this.currentCharacters = characters;
        this.currentDialog = dialog;
        this.currentDialog.ResetDialogProgress();
        if (this.currentDialog.scenarioMusic != null)
        {
            CurrentlyDialogPlayingMusic = this.currentDialog.scenarioMusic;
            MusicPlayer.PlayMusic(this.currentDialog.scenarioMusic);
        }

        this.DialogInProgress = true;
        DialogEvents.OnDialogStart(this.currentDialog);
        ProgressDialog();
    }
    
    private void ShowSimpleDialog(CharacterSpeaking characterSpeaking, GameCharacterData character, string translatedText)
    {
        HideDialogBox();
        this.simpleDialog.gameObject.SetActive(true);
        this.dialogBackground.enabled = true;
        float voicePitch = 1f;

        if (characterSpeaking == CharacterSpeaking.Left)
        {
            this.leftChar.gameObject.SetActive(true);
            this.leftChar.sprite = character.characterFace;
            this.leftCharName.text = character.GetFullName(true);
            voicePitch = this.currentCharacters[0].voicePitch;
        }
        else
        {
            this.rightChar.gameObject.SetActive(true);
            this.rightChar.sprite = character.characterFace;
            this.rightCharName.text = character.GetFullName(true);
            voicePitch = this.currentCharacters[1].voicePitch;
        }
        this.simpleDialog.soundPitch = voicePitch;
        this.simpleDialog.ShowText(translatedText);
    }

    public void HideDialogBox()
    {
        this.simpleDialog.gameObject.SetActive(false);
        this.leftChar.gameObject.SetActive(false);
        this.rightChar.gameObject.SetActive(false);
        this.dialogBackground.enabled = false;
        this.answerDialogBox.gameObject.SetActive(false);
    }

    private void ProcessAnswer()
    {
        if (this.currentDialog.answer != null)
        {
            this.leftChar.gameObject.SetActive(true);
            this.leftChar.sprite = this.currentCharacters[0].characterFace;
            this.leftCharName.text = this.currentCharacters[0].GetFullName(true);
            this.dialogBackground.enabled = true;
            this.answerDialogBox.gameObject.SetActive(true);
            this.answerDialogBox.AddAnswers(this.currentDialog, this.currentDialog.answer, this.currentCharacters);
            DialogEvents.OnAnswerDialogStart(this.currentDialog, this.currentDialog.answer);
        }
        else
        {
            DialogEnd();
        }
    }

    private void DialogEnd()
    {
        CurrentlySpeakingChar = null;
        if (CurrentlyDialogPlayingMusic != null)
        {
            CurrentlyDialogPlayingMusic = null;
            if (this.currentDialog.ID.Contains("TheMissingShopkeeper") == false)
            {
                MusicPlayer.TransitionToMainMusic();
            }
        }
        DialogEvents.OnDialogEnd(this.currentDialog);
        this.dialogEnd?.Invoke();
    }
}
