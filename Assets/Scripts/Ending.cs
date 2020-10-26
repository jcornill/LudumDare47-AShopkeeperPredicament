using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public static bool EndingStart;
    public static bool EndingCanReallyStart;
    public static int EndingReflection;

    public GameObject NPC;
    public DialogData endingDialog;

    public GameObject Credit2;

    private static Ending Instance;

    private bool diappearFade;
    private float fadeChangeTime = 2f;
    private float fadeTime;

    private void Start()
    {
        Instance = this;
        EndingStart = false;
        EndingCanReallyStart = false;
        GetComponent<Button>().onClick.AddListener(TriggerEnding);
    }

    private void Update()
    {
        if (this.Credit2.activeSelf)
        {
            if (Input.anyKeyDown)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }

        if (this.diappearFade)
        {
            NPC.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 1), new Color(0, 0, 0, 0), (Time.time - this.fadeTime) / fadeChangeTime);
            if (Time.time - this.fadeTime >= fadeChangeTime)
            {
                this.diappearFade = false;
                LaunchingEndingScenario();
            }
        }

        TryLaunchEnding();
    }

    private static void TriggerEnding()
    {
        if (NPCPlayerData.DevianceLevel != 6)
        {
            return;
        }

        EndingStart = true;
    }

    private void TryLaunchEnding()
    {
        if (EndingCanReallyStart && this.diappearFade == false)
        {
            EndingCanReallyStart = false;
            Disappear();
        }
    }

    public void Disappear()
    {
        this.diappearFade = true;
        this.fadeTime = Time.time;
    }

    private void LaunchingEndingScenario()
    {
        DialogManager.LaunchDialog(this.endingDialog);
    }

    public static void ShowCredit2()
    {
        Instance.Credit2.gameObject.SetActive(true);
    }
}