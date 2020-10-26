using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AnswerDialogBox : MonoBehaviour
{
    public GameObject AnswerButtonPrefab;
    public DialogBox DialogBox;

    private void OnEnable()
    {
        ClearBox();
    }

    public void AddAnswers(DialogData dialog, AnswerData answerData, GameCharacterData[] dialogCharacters)
    {
        foreach (AnswerLine answerLine in answerData.answers)
        {
            if (answerLine.IsConditionFulfill() == false && Powers.RequirementForDialogs == false)
            {
                continue;
            }
            GameObject answerObject = Instantiate(this.AnswerButtonPrefab, this.transform);
            AnswerButton answerButton = answerObject.GetComponent<AnswerButton>();
            string answerText = string.Empty;
            bool first = true;
            if (Powers.RequirementForDialogs)
            {
                answerText += "[Cost: ";

                if (answerLine.MoneyChange < 0)
                {
                    answerText += "Money " + answerLine.MoneyChange;
                    first = false;
                }
                
                if (answerLine.answerInteraction != AnswerInteractionType.ActingNormally && answerLine.PowerChange < 0)
                {
                    if (first == false)
                    {
                        answerText += ", ";
                    }
                    answerText += "Power " + Usefull.ToPercent(answerLine.PowerChange);
                    first = false;
                }

                answerText += "] ";
            }

            if (first == false)
            {
                answerText += answerLine.GetTranslatedText();
            }
            else
            {
                answerText = answerLine.GetTranslatedText();
            }

            answerButton.SetText(answerText);

            if (NPCPlayerData.Money + answerLine.MoneyChange < 0)
            {
                if (Powers.RequirementForDialogs)
                {
                    answerButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Destroy(answerButton.gameObject);
                    continue;
                }
            }
            
            if (answerLine.answerInteraction != AnswerInteractionType.ActingNormally && NPCPlayerData.PowerCharge + answerLine.PowerChange < 0)
            {
                if (Powers.RequirementForDialogs)
                {
                    answerButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Destroy(answerButton.gameObject);
                    continue;
                }
            }

            answerButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Powers.MoneyCollection)
                {
                    NPCPlayerData.Money += answerLine.MoneyChange;
                }
                NPCPlayerData.Deviance += answerLine.DevianceChange;
                NPCPlayerData.Deviance = Mathf.Clamp01(NPCPlayerData.Deviance);
                NPCPlayerData.PowerCharge += answerLine.PowerChange;
                NPCPlayerData.PowerCharge = Mathf.Clamp01(NPCPlayerData.PowerCharge);
                NPCPlayerData.ChangeWantedLevel(answerLine.WantedLevelChange);

                if (answerLine.PowerChange < 0)
                {
                    StaticData.PowerUsed -= answerLine.PowerChange;
                }
                
                DialogEvents.OnAnswerDialogSelected(dialog, answerData, answerLine);
                this.DialogBox.PlayDialog(answerLine.ResultDialogs[Random.Range(0, answerLine.ResultDialogs.Count)], dialogCharacters);
            });
        }
    }

    public void ClearBox()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "ClickToContinue")
            {
                continue;
            }
            Destroy(child.gameObject);
        }
    }
}