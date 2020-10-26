using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnswerData))]
public class AnswerDataEditor : Editor
{
    private AnswerData answerData;
    private Condition[] conditionsList;
    private string[] conditionsNameList;

    private Dictionary<AnswerInteractionType, float[,]> AnswerInteractionData = new Dictionary<AnswerInteractionType, float[,]>()
    {
        {
            AnswerInteractionType.ActingNormally, new float[,]
            {
                // Deviance
                {-0.1f, -0.15f, -0.2f},
                // Power
                {-0.01f, -0.01f, -0.01f},
                // Wanted
                {-0.05f, -0.1f, -0.2f},
            }
        },
        {
            AnswerInteractionType.ActingNormallyBad, new float[,]
            {
                // Deviance
                {0.05f, 0.075f, 0.15f},
                // Power
                {0.05f, 0.1f, 0.15f},
                // Wanted
                {0.05f, 0.1f, 0.2f},
            }
        },
        {
            AnswerInteractionType.ActingNormallyGood, new float[,]
            {
                // Deviance
                {-0.05f, -0.075f, -0.15f},
                // Power
                {0.025f, 0.05f, 0.1f},
                // Wanted
                {-0.1f, -0.2f, -0.3f},
            }
        },
        {
            AnswerInteractionType.ActingNormallyForcibly, new float[,]
            {
                // Deviance
                {-0.2f, -0.4f, -0.6f},
                // Power
                {-0.01f, -0.01f, -0.1f},
                // Wanted
                {-0.025f, -0.05f, -0.1f},
            }
        },
        {
            AnswerInteractionType.Talk, new float[,]
            {
                // Deviance
                {0.05f, 0.1f, 0.15f},
                // Power
                {-0.1f, -0.2f, -0.3f},
                // Wanted
                {0.01f, 0.025f, 0.05f},
            }
        },
        {
            AnswerInteractionType.Ignore, new float[,]
            {
                // Deviance
                {0.025f, 0.05f, 0.075f},
                // Power
                {-0.1f, -0.15f, -0.25f},
                // Wanted
                {0.01f, 0.02f, 0.03f},
            }
        },
        {
            AnswerInteractionType.Money, new float[,]
            {
                // Deviance
                {0.1f, 0.2f, 0.3f},
                // Power
                {-0.25f, -0.35f, -0.45f},
                // Wanted
                {0.05f, 0.1f, 0.15f},
            }
        },
        {
            AnswerInteractionType.Destroy, new float[,]
            {
                // Deviance
                {0.1f, 0.3f, 0.6f},
                // Power
                {-0.4f, -0.6f, -0.8f},
                // Wanted
                {0.1f, 0.2f, 0.3f},
            }
        }
    };
    
    
    public void OnEnable()
    {
        this.answerData = (AnswerData) this.target;

        this.conditionsList = Assembly.GetAssembly(typeof(Condition)).GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Condition))).Select(x => (Condition) Activator.CreateInstance(x)).ToArray();
        this.conditionsNameList = this.conditionsList.Select(x => x.GetType().Name).ToArray();
    }
    
    public override void OnInspectorGUI()
    {
        if (this.answerData.answers == null)
        {
            this.answerData.answers = new List<AnswerLine>();
        }
        

        this.answerData.ID = this.target.name;
        
        int i = 0;
        foreach (AnswerLine answer in this.answerData.answers.ToArray())
        {
            if (answer.ResultDialogs == null)
            {
                answer.ResultDialogs = new List<DialogData>();
            }
            
            answer.ID = this.target.name + "-" + i++;
            answer.foldOut = EditorGUILayout.Foldout(answer.foldOut, answer.ID);
            if (answer.foldOut)
            {
                EditorGUILayout.LabelField("Condition for the answer to be seen:");
                answer.ConditionID = EditorGUILayout.Popup(answer.ConditionID, this.conditionsNameList);
                answer.Condition = this.conditionsList[answer.ConditionID];
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Requirements");
                answer.MoneyChange = EditorGUILayout.IntField("Money Change", answer.MoneyChange);
                
                EditorGUILayout.BeginHorizontal();
                answer.answerInteraction = (AnswerInteractionType)EditorGUILayout.EnumPopup("Answer Interaction", answer.answerInteraction);
                answer.answerSeverity = EditorGUILayout.IntPopup(answer.answerSeverity, new [] {"1", "2", "3"}, new []{0, 1, 2}, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();

                if (answer.answerInteraction != AnswerInteractionType.Custom)
                {
                    float[,] values = this.AnswerInteractionData[answer.answerInteraction];
                    answer.DevianceChange = values[0, answer.answerSeverity];
                    answer.PowerChange = values[1, answer.answerSeverity];
                    answer.WantedLevelChange = values[2, answer.answerSeverity];
                }

                answer.DevianceChange = EditorGUILayout.FloatField("Deviance Change", answer.DevianceChange);
                answer.PowerChange = EditorGUILayout.FloatField("Power Change", answer.PowerChange);
                answer.WantedLevelChange = EditorGUILayout.FloatField("Wanted Level Change", answer.WantedLevelChange);
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Language");
                EditorGUILayout.LabelField("English:");
                answer.EnglishText = EditorGUILayout.TextArea(answer.EnglishText);
                EditorGUILayout.LabelField("French:");
                answer.FrenchText = EditorGUILayout.TextArea(answer.FrenchText);
                EditorGUILayout.LabelField("Portuguese:");
                answer.PortugueseText = EditorGUILayout.TextArea(answer.PortugueseText);
                
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Result Dialogs");
                int toRemove = -1;
                for (int index = 0; index < answer.ResultDialogs.Count; index++)
                {
                    EditorGUILayout.BeginHorizontal();
                    answer.ResultDialogs[index] = EditorGUILayout.ObjectField(index + ":", answer.ResultDialogs[index], typeof(DialogData), false) as DialogData;
                    if (GUILayout.Button("x"))
                    {
                        toRemove = index;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (toRemove >= 0)
                {
                    answer.ResultDialogs.RemoveAt(toRemove);
                }
                if (GUILayout.Button("Add result dialog to " + answer.ID))
                {
                    answer.ResultDialogs.Add(null);
                }


                if (GUILayout.Button("Remove " + answer.ID))
                {
                    this.answerData.answers.Remove(answer);
                }
            }
        }
        if (GUILayout.Button("Add answer line"))
        {
            this.answerData.answers.Add(new AnswerLine());
        }
        if (GUILayout.Button("Force Save"))
        {
            EditorUtility.SetDirty(this.answerData);
            AssetDatabase.SaveAssets();
        }
    }
}
