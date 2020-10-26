using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SoundMenu : EditorWindow
{
    [MenuItem("LD47/Generate folder Sound Files")]
    static void MenuGenerateSoundFiles()
    {
        doCreateAllSoundData(Selection.activeObject);
    }

    [MenuItem("CONTEXT/AudioClip/Create Sound Data", false, 0)]
    static void ContextCreateSoundData(MenuCommand command)
    {
        doCreateSoundData(AssetDatabase.LoadAssetAtPath<AudioClip>(AssetDatabase.GetAssetPath(Selection.activeObject)));
    }

    [MenuItem("CONTEXT/AudioClip/Create All Sound Data", false, 0)]
    static void ContextCreateAllSoundData(MenuCommand command)
    {
        doCreateAllSoundData(command.context);
    }

    static void doCreateSoundData(AudioClip clip)
    {
        SoundData sound = new SoundData();
        sound.soundName = clip.name;
        sound.clip = clip;
        AssetDatabase.CreateAsset(sound, AssetDatabase.GetAssetPath(clip).Split('.')[0] + ".asset");
        AssetDatabase.SaveAssets();
        Debug.Log("Sound data created for " + clip.name);
    }

    // This doesn't work and I shouldn't have wasted time doing it
    static void doCreateAllSoundData(UnityEngine.Object selectedObject)
    {
        string aPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject));
        aPath = aPath.Split(new string[] { "Resources\\" }, StringSplitOptions.None)[1];
        Debug.Log(aPath);
        //throw new NotImplementedException("Sorry.");
        int count = 0;
        foreach (AudioClip clip in Resources.LoadAll(aPath))
        {
            string newPath = AssetDatabase.GetAssetPath(clip).Split('.')[0] + ".asset";
            if (AssetDatabase.LoadAssetAtPath<SoundData>(newPath) != null)
                return;

            doCreateSoundData(clip);
            count++;
        }
        Debug.Log("Created " + count + " Sound files.");
    }
}
