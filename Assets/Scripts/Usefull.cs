using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usefull : MonoBehaviour
{
    private static Usefull Instance;

    private Action methodToInvoke;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    private void DelayedMethod(float time, Action method)
    {
        this.methodToInvoke = method;
        Invoke(nameof(Execute), time);
    }

    private void Execute()
    {
        this.methodToInvoke?.Invoke();
        this.methodToInvoke = null;
    }

    public static void DelayedCall(float t, Action a)
    {
        Instance.DelayedMethod(t, a);
    }
    
    public static string ToPercent(float f)
    {
        f *= 100;
        return (int)f + "%";
    }

}
