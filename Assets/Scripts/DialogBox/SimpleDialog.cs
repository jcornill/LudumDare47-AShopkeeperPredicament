using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SimpleDialog : MonoBehaviour
{
    public float CharPerSecond = 10f;
    public bool IsDisplayingText { get; private set; }

    private TextMeshProUGUI textMesh;
    private string textToShow;
    public float soundPitch;

    private bool stopCoroutine;

    private bool htmlTag;
    private bool backSlash;

    private void Awake()
    {
        this.textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        this.textMesh.text = string.Empty;
        this.stopCoroutine = false;
        this.IsDisplayingText = false;
    }

    public void ShowText(string translatedText)
    {
        this.textToShow = translatedText.TrimStart().TrimEnd();
        StartCoroutine(DisplayText());
    }

    public void WriteEverything()
    {
        if (this.IsDisplayingText)
        {
            this.textMesh.text = this.textToShow.Replace("\\n", Environment.NewLine);
            this.stopCoroutine = true;
        }
    }

    private IEnumerator DisplayText()
    {
        this.textMesh.text = string.Empty;
        this.IsDisplayingText = true;
        foreach (char c in this.textToShow)
        {
            if (this.stopCoroutine)
            {
                this.stopCoroutine = false;
                break;
            }
            if (c == '\\')
            {
                this.backSlash = true;
                continue;
            }

            if (this.backSlash)
            {
                this.backSlash = false;
                if (c == 'n')
                {
                    this.textMesh.text += Environment.NewLine;
                    continue;
                }
            }
            
            this.textMesh.text += c;
            
            if (c == '<')
            {
                this.htmlTag = true;
            }

            if (c == '>')
            {
                this.htmlTag = false;
            }
            
            
            if (this.htmlTag)
            {
                continue;
            }
            SoundManager.PlayInterfaceSound("DialogWrite", soundPitch);
            yield return new WaitForSeconds(1f/this.CharPerSecond);
        }

        this.IsDisplayingText = false;
    }
}
