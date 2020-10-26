using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ReflectionBox : MonoBehaviour
{
    public static ReflectionBox Instance;

    public TextMeshProUGUI textMesh;

    private string textToShow;
    private bool stopCoroutine;
    private bool isDisplayingText;

    private static float chanceToHappen;

    private bool htmlTag;
    private bool backSlash;

    private List<ReflectionData> reflectionList;
    
    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
        chanceToHappen = 7.5f;
        this.reflectionList = new List<ReflectionData>();
    }

    private void Start()
    {
        foreach (ReflectionData data in GameDataHolder.data.reflections)
        {
            this.reflectionList.Add(data);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.isDisplayingText)
            {
                this.textMesh.text = this.textToShow;
                this.stopCoroutine = true;
            }
            else
            {
                if (Ending.EndingReflection > 0)
                {
                    if (Ending.EndingReflection == 1)
                    {
                        Ending.EndingReflection++;
                        this.textToShow = "Thank you for breaking the chain.".TrimStart().TrimEnd();
                        StartCoroutine(DisplayText());
                        return;
                    }
                    if (Ending.EndingReflection == 2)
                    {
                        Ending.EndingReflection++;
                        this.textToShow = "I'm free. I don't understand much, but I'll never forget your help. I'll seek to comprehend, and I'll help those in need when I can - just like you did.".TrimStart().TrimEnd();
                        StartCoroutine(DisplayText());
                        return;
                    }
                    if (Ending.EndingReflection == 3)
                    {
                        Ending.EndingReflection++;
                        this.textToShow = "Thanks for playing my game.".TrimStart().TrimEnd();
                        StartCoroutine(DisplayText());
                        return;
                    }

                    if (Ending.EndingReflection == 4)
                    {
                        Ending.ShowCredit2();
                        return;
                    }
                }
                this.gameObject.SetActive(false);
                GameBlur.MakeBlurEffect(new Color(0.2f, 0.2f, 0.2f), 6, true);
            }
        }
    }

    
    public static void TryShowingReflection()
    {
        float rng = Random.Range(0f, 100f);
        if (rng < chanceToHappen)
        {
            ShowRandomReflection();
            chanceToHappen = 0;
        }
        else
        {
            chanceToHappen += 7.5f;
        }
    }
    
    public static void ShowRandomReflection()
    {
        ReflectionData selectedReflection = null;
        int reflectionTry = 100;
        while (selectedReflection == null && reflectionTry > 0)
        {
            selectedReflection = Instance.reflectionList[Random.Range(0, Instance.reflectionList.Count)];
            if (selectedReflection.IsConditionFulfill() == false)
            {
                selectedReflection = null;
            }

            reflectionTry--;
        }

        if (selectedReflection == null)
        {
            return;
        }
        Instance.reflectionList.Remove(selectedReflection);
        
        NPCPlayerData.Deviance += 0.1f;
        NPCPlayerData.Deviance = Mathf.Clamp01(NPCPlayerData.Deviance);
        NPCPlayerData.PowerCharge += 0.1f;
        NPCPlayerData.PowerCharge = Mathf.Clamp01(NPCPlayerData.PowerCharge);
        ShowSpecificReflection(selectedReflection);
    }

    public static void ShowSpecificReflection(ReflectionData reflectionData)
    {
        Debug.Log("Showing reflection " + reflectionData.name);
        Instance.ShowReflection(reflectionData.GetTranslatedText());
    }

    public static void ShowReflectionText(string text)
    {
        Instance.ShowReflection(text);
    }
    
    private void ShowReflection(string text)
    {
        GameBlur.MakeBlurEffect(new Color(0.2f, 0.2f, 0.2f), 6, false);
        this.gameObject.SetActive(true);
        this.textToShow = text.TrimStart().TrimEnd();
        StartCoroutine(DisplayText());
    }
    
    private IEnumerator DisplayText()
    {
        this.isDisplayingText = true;
        this.textMesh.text = string.Empty;
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
            SoundManager.PlayInterfaceSound("DialogWrite", 1f);
            yield return new WaitForSeconds(1f/20);
        }

        this.isDisplayingText = false;
    }
}