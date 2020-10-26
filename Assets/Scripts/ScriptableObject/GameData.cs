using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 0)]
public class GameData : ScriptableObject
{
   public SoundData[] sounds;
   public ReflectionData[] reflections;

   #if UNITY_EDITOR
   private void OnEnable()
   {
      OnValidate();
   }

   private void OnValidate()
   {
      UpdateSound();
      UpdateReflections();
   }

   private void UpdateReflections()
   {
      List<ReflectionData> list = new List<ReflectionData>();
      foreach (string soundPath in AssetDatabase.FindAssets("t:ReflectionData"))
      {
         string path = AssetDatabase.GUIDToAssetPath(soundPath);
         list.Add(AssetDatabase.LoadAssetAtPath<ReflectionData>(path));
      }

      this.reflections = list.ToArray();
   }
   
   private void UpdateSound()
   {
      List<SoundData> list = new List<SoundData>();
      foreach (string soundPath in AssetDatabase.FindAssets("t:SoundData"))
      {
         string path = AssetDatabase.GUIDToAssetPath(soundPath);
         list.Add(AssetDatabase.LoadAssetAtPath<SoundData>(path));
      }

      this.sounds = list.ToArray();
   }
   #endif
}
