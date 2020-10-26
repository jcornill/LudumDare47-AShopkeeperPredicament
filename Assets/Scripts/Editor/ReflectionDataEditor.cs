using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CustomEditor(typeof(ReflectionData))]
public class ReflectionDataEditor : Editor
{
    private ReflectionData reflectionData;
    private Condition[] conditionsList;
    private string[] conditionsNameList;
    
    
    public void OnEnable()
    {
        this.reflectionData = (ReflectionData) this.target;

        this.conditionsList = Assembly.GetAssembly(typeof(Condition)).GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Condition))).Select(x => (Condition) Activator.CreateInstance(x)).ToArray();
        this.conditionsNameList = this.conditionsList.Select(x => x.GetType().Name).ToArray();
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        this.reflectionData.ConditionID = EditorGUILayout.Popup(this.reflectionData.ConditionID, this.conditionsNameList);
        this.reflectionData.Condition = this.conditionsList[this.reflectionData.ConditionID];
        
        if (GUILayout.Button("Force Save"))
        {
            EditorUtility.SetDirty(this.reflectionData);
            AssetDatabase.SaveAssets();
        }
    }
}
