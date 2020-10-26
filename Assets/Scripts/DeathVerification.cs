using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DeathVerification : MonoBehaviour
{
    private static DeathVerification Instance;
    
    public UpdatePanel upd;
    
    private float time;
    private bool isCheckingTime;

    private void Awake()
    {
        Instance = this;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            NPCPlayerData.WantedLevel = 1;
            Debug.Log("Start checking time");
            this.time = Time.time;
            this.isCheckingTime = true;
        }
    }
#endif
    
    public static bool CheckDeath()
    {
        return Instance.Check();
    }
    
    private bool Check()
    {
        if (this.isCheckingTime)
        {
            Debug.Log(Time.time - this.time);
            if (Time.time - this.time >= 20f)
            {
                this.upd.ShowUpdatePanel();
                this.time = Time.time;
                this.isCheckingTime = false;
                return true;
            }
        }
        
        if (Math.Abs(NPCPlayerData.WantedLevel - 1) < 0.01f)
        {
            if (this.isCheckingTime == false)
            {
                Debug.Log("Start checking time");
                this.time = Time.time;
                this.isCheckingTime = true;
            }
        }
        else if (NPCPlayerData.WantedLevel <= 0.25f)
        {
            Debug.Log("Stop checking time");
            this.isCheckingTime = false;
        }

        return false;
    }
}
