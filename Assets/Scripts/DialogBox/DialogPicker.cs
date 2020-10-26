using System.Collections.Generic;
using UnityEngine;

public class DialogPicker : MonoBehaviour
{
    private static DialogPicker Instance;
    
    public DialogData[] dialogData;

    public DialogData finalTestDialog;

    private Dictionary<Rarity, List<DialogData> > sortedDialogData;

    private void Awake()
    {
        Instance = this;
        FillUpDialogData();
    }

    private void FillUpDialogData()
    {
        this.sortedDialogData = new Dictionary<Rarity, List<DialogData>>();
        this.sortedDialogData.Add(Rarity.Common, new List<DialogData>());
        this.sortedDialogData.Add(Rarity.Uncommon, new List<DialogData>());
        this.sortedDialogData.Add(Rarity.Rare, new List<DialogData>());
        this.sortedDialogData.Add(Rarity.Epic, new List<DialogData>());
        this.sortedDialogData.Add(Rarity.Legendary, new List<DialogData>());
        foreach (DialogData data in this.dialogData)
        {
            this.sortedDialogData[data.dialogRarity].Add(data);
        }
    }

    public static void RemoveDialog(DialogData data)
    {
        Instance.sortedDialogData[data.dialogRarity].Remove(data);
    }

    public static DialogData GetADialog()
    {
        if (NPCPlayerData.DevianceLevel == 4 && Random.Range(0, 100) <= 50)
        {
            return Instance.finalTestDialog;
        }
        
        Rarity selectedRarity;
        int random = Random.Range(0, 100);
        if (random < 48)                             // 48%
        {
            selectedRarity = Rarity.Common;
        }
        else if (random < 48 + 32)                   // 32%
        {
            selectedRarity = Rarity.Uncommon;
        }
        else if (random < 48 + 32 + 16)              // 16%
        {
            selectedRarity = Rarity.Rare;
        }
        else if (random < 48 + 32 + 16 + 3)          // 3%
        {
            selectedRarity = Rarity.Rare;
        }
        else                                         // 1%
        {
            selectedRarity = Rarity.Legendary;
        }

        List<DialogData> selectedRarityDialogList = Instance.sortedDialogData[selectedRarity];

        DialogData selectedDialog = null;

        if (selectedRarityDialogList.Count > 0)
        {
            selectedDialog = selectedRarityDialogList[Random.Range(0, selectedRarityDialogList.Count)];
        }

        if (selectedDialog == null || selectedDialog.IsConditionFulfill() == false)
        {
            return null;
        }

        bool isItGood = false;
        if (selectedDialog.answer != null)
        {
            foreach (AnswerLine answer in selectedDialog.answer.answers)
            {
                if (answer.IsConditionFulfill() && NPCPlayerData.Money + answer.MoneyChange >= 0)
                {
                    if (answer.answerInteraction == AnswerInteractionType.ActingNormally || NPCPlayerData.PowerCharge + answer.PowerChange >= 0)
                    {
                        isItGood = true;
                        break;
                    }
                }
            }
        }
        else
        {
            isItGood = true;
        }

        if (isItGood == false)
        {
            Debug.Log("Cannot select " + selectedDialog.ID + " cause no answer are possibles.");
            return null;
        }

        return selectedDialog;
    }
}
