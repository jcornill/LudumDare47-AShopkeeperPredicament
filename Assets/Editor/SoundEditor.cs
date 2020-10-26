using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundData))]
[CanEditMultipleObjects]
public class SoundEditor : Editor
{
    private SoundData sound;

    private void OnEnable()
    {
        this.sound = (SoundData)this.target;
        EditorApplication.playModeStateChanged += PlayModeStateChange;
    }

    private void PlayModeStateChange(PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingEditMode)
        {
            GameObject go = GameObject.Find("AUDIO_TEMP");
            if (go != null)
            {
                DestroyImmediate(go);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Play sound"))
        {
            GameObject go = GameObject.Find("AUDIO_TEMP");
            if (go == null)
            {
                go = new GameObject("AUDIO_TEMP");
            }

            go.transform.position = Camera.main.transform.position;
            AudioSource source = go.AddComponent<AudioSource>();
            SoundData.SetupAudioSource(source, this.sound);
            source.playOnAwake = false;
            source.Play();
        }

        if (GUILayout.Button("Stop playing"))
        {
            GameObject go = GameObject.Find("AUDIO_TEMP");
            if("AUDIO_TEMP" != null)
                go.GetComponent<AudioSource>().Stop();
        }

        if (GUILayout.Button("Force Save"))
        {
            EditorUtility.SetDirty(this.sound);
            AssetDatabase.SaveAssets();
        }
    }
}
