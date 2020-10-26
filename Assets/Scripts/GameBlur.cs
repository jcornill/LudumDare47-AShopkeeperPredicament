using System;
using UnityEngine;
using UnityEngine.UI;

public class GameBlur : MonoBehaviour
{
    private float time = 5;
    private float time2 = 0;
    public Color color;
    private Color color2;

    private bool needToUpdate = true;
    private bool fadeIn = false;

    private static GameBlur Instance;

    public static void MakeBlurEffect(Color newColor, float t, bool fadeOut)
    {
        Instance.color2 = newColor;
        Instance.time = t;
        Instance.fadeIn = !fadeOut;
        Instance.needToUpdate = true;
        Instance.time2 = 0;
    }
    
    private void Awake()
    {
        Instance = this;
        this.color2 = this.color;
    }

    private void Update()
    {
        if (this.needToUpdate == false)
        {
            return;
        }

        if (this.fadeIn)
        {
            this.time2 += Time.unscaledDeltaTime * 5;
            if (this.time2 >= this.time)
            {
                this.time2 = this.time;
                this.needToUpdate = false;
            }
            GetComponent<Image>().material.SetFloat("_Size", this.time2);
            GetComponent<Image>().material.SetColor("_Color", Color.Lerp(Color.white, this.color2, this.time2 / this.time));
        }
        else
        {
            this.time -= Time.unscaledDeltaTime * 5;
            if (this.time <= 0)
            {
                this.time = 0;
                this.needToUpdate = false;
            }
            GetComponent<Image>().material.SetFloat("_Size", this.time);
            GetComponent<Image>().material.SetColor("_Color", Color.Lerp(this.color2, Color.white, (5f - this.time) / 5f));
        }
    }

    private void OnDestroy()
    {
        GetComponent<Image>().material.SetFloat("_Size", 5f);
        GetComponent<Image>().material.SetColor("_Color", this.color);
    }
}
