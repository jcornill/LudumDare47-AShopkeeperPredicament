using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CustomEditor(typeof(DialogData))]
public class DialogDataEditor : Editor
{
    private DialogData dialogData;
    private Condition[] conditionsList;
    private string[] conditionsNameList;
    
    public void OnEnable()
    {
        this.dialogData = (DialogData) this.target;
        
        this.conditionsList = Assembly.GetAssembly(typeof(Condition)).GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Condition))).Select(x => (Condition) Activator.CreateInstance(x)).ToArray();
        this.conditionsNameList = this.conditionsList.Select(x => x.GetType().Name).ToArray();
    }
    
    public override void OnInspectorGUI()
    {
        if (this.dialogData.dialogLines == null)
        {
            this.dialogData.dialogLines = new List<DialogLine>();
        }

        this.dialogData.ID = this.target.name;
        
        EditorGUILayout.LabelField("Condition for the dialog to be seen:");
        this.dialogData.ConditionID = EditorGUILayout.Popup(this.dialogData.ConditionID, this.conditionsNameList);
        this.dialogData.Condition = this.conditionsList[this.dialogData.ConditionID];
        
        this.dialogData.dialogRarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", this.dialogData.dialogRarity);
        this.dialogData.numberOfCharacters = EditorGUILayout.IntField("Number of fake characters", this.dialogData.numberOfCharacters);
        
        int i = 0;
        foreach (DialogLine dialogLine in this.dialogData.dialogLines.ToArray())
        {
            dialogLine.ID = this.target.name + "-" + i++;
            dialogLine.foldOut = EditorGUILayout.Foldout(dialogLine.foldOut, dialogLine.ID);
            if (dialogLine.foldOut)
            {
                dialogLine.IDCharacterSpeaking = EditorGUILayout.IntField("Speaker Character ID", dialogLine.IDCharacterSpeaking);
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Language");
                EditorGUILayout.LabelField("English:");
                dialogLine.EnglishText = EditorGUILayout.TextArea(dialogLine.EnglishText);
                EditorGUILayout.LabelField("French:");
                dialogLine.FrenchText = EditorGUILayout.TextArea(dialogLine.FrenchText);
                EditorGUILayout.LabelField("Portuguese:");
                dialogLine.PortugueseText = EditorGUILayout.TextArea(dialogLine.PortugueseText);
                EditorGUILayout.LabelField("");
                if (GUILayout.Button("Remove " + this.target.name + "-" + (i - 1)))
                {
                    this.dialogData.dialogLines.Remove(dialogLine);
                }
            }
        }
        if (GUILayout.Button("Add dialog line"))
        {
            this.dialogData.dialogLines.Add(new DialogLine());
        }
        
        this.dialogData.answer = EditorGUILayout.ObjectField("Result answer", this.dialogData.answer, typeof(AnswerData), false) as AnswerData;
        this.dialogData.scenarioMusic = EditorGUILayout.ObjectField("Scenario Music", this.dialogData.scenarioMusic, typeof(SoundData), false) as SoundData;

        if (GUILayout.Button("Force Save"))
        {
            foreach (string path in AssetDatabase.FindAssets("t:DialogData"))
            {
                EditorUtility.SetDirty((AssetDatabase.LoadAssetAtPath<DialogData>(AssetDatabase.GUIDToAssetPath(path))));
            }
            //EditorUtility.SetDirty(this.dialogData);
            AssetDatabase.SaveAssets();
        }
    }
}
