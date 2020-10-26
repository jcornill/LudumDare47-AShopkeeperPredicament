using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static List<CharacterMovement> Characters;
    public static bool CanSendCharacter;
    
    private static DialogManager instance;
    
    public DialogBox DialogBox;

    private DialogData currentDialog;
    public static int numberOfCharToSend;
    private int numberOfCharNeeded;
    private int currentNumberOfChar;

    public static bool CanSelectDialog;

    public static bool CanDoStuff => Game.Instance.gameObject.activeSelf && ReflectionBox.Instance.gameObject.activeSelf == false && UpdatePanel.IsUpdating == false && Ending.EndingStart == false;
    
    private void Start()
    {
        CanSelectDialog = true;
        Characters = new List<CharacterMovement>();
        CanSendCharacter = false;
        instance = this;
        this.DialogBox.dialogEnd += MakeCharLeave;
    }

    private void Update()
    {
        if (CanDoStuff && CanSelectDialog)
        {
            LookForTheNextDialog();
        }
    }

    private void LookForTheNextDialog()
    {
        if (this.currentDialog == null)
        {
            SelectNextDialog(DialogPicker.GetADialog());
        }
    }
    
    public static void LaunchDialog(DialogData dialogData)
    {
        instance.SelectNextDialog(dialogData);
    }
    
    public void SelectNextDialog(DialogData dialog)
    {
        if (dialog == null)
        {
            return;
        }

        Debug.Log("Launch dialog " + dialog.name);
        
        CanSelectDialog = false;
        CanSendCharacter = true;
        this.currentDialog = dialog;
        this.numberOfCharNeeded = this.currentDialog.numberOfCharacters;
        numberOfCharToSend = this.numberOfCharNeeded;
        this.currentNumberOfChar = 0;
        DialogEvents.OnDialogSelected(dialog);
    }
    
    private void LaunchDialog()
    {
        GameCharacterData[] characters = new GameCharacterData[this.currentDialog.numberOfCharacters + 1];
        characters[0] = CharacterManager.Instance.playerCharacters[0];
        for (int i = 1; i < this.currentDialog.numberOfCharacters + 1; i++)
        {
            characters[i] = Characters[i - 1].gameCharacter;
        }
        this.DialogBox.PlayDialog(this.currentDialog, characters);
    }

    public static void SendChar(CharacterMovement charMov)
    {
        Characters.Add(charMov);
        numberOfCharToSend--;
        if (numberOfCharToSend == 0)
        {
            CanSendCharacter = false;
        }
    }
    
    public static void CharIsHere()
    {
        instance.currentNumberOfChar++;
        if (instance.currentNumberOfChar >= instance.numberOfCharNeeded)
        {
            instance.LaunchDialog();
        }
    }

    public static void DisconnectCharacter(CharacterMovement character)
    {
        Characters.Remove(character);
        SoundManager.PlayInterfaceSound("Disconnect");
        Destroy(character.gameObject);
    }
    
    private static void MakeCharLeave()
    {
        foreach (CharacterMovement characterMovement in Characters)
        {
            characterMovement.CanLeave();
        }
        Characters.Clear();
        instance.currentDialog = null;
    }
}
