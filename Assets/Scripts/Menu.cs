using System;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject CreditsObject;
    public GameObject SettingsObject;

    public Game Game;

    public void OnButtonPlay()
    {
        this.gameObject.SetActive(false);
        this.Game.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (this.CreditsObject.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                OnButtonCredits();
            }
        }
    }

    public void OnButtonCredits()
    {
        this.CreditsObject.SetActive(this.CreditsObject.activeSelf == false);
    }

    public void OnButtonSettings()
    {
        this.SettingsObject.SetActive(this.SettingsObject.activeSelf == false);
    }

    public void OnButtonQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}