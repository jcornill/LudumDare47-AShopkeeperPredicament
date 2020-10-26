using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    public static bool IsHere;
    
    public SpriteRenderer spriteRenderer;

    private float fadeChangeTime = 2f;

    private bool appearFade;
    private bool diappearFade;
    private float fadeTime;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.color = new Color(0, 0, 0, 0);
        Invoke(nameof(Appear), Random.Range(60f, 150f));
    }

    private void Update()
    {
        UpdateFade();
    }


    public Vector3 GetRandomPosOnScreen()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-3.5f, 0f), 0);
    }
    
    public void UpdateFade()
    {
        if (this.appearFade)
        {
            this.spriteRenderer.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), (Time.time - this.fadeTime) / fadeChangeTime);
            if (Time.time - this.fadeTime >= fadeChangeTime)
            {
                this.appearFade = false;
            }
        }
        
        if (this.diappearFade)
        {
            this.spriteRenderer.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(0, 0, 0, 0), (Time.time - this.fadeTime) / fadeChangeTime);
            if (Time.time - this.fadeTime >= fadeChangeTime)
            {
                this.diappearFade = false;
            }
        }
    }

    public void Appear()
    {
        IsHere = true;
        this.appearFade = true;
        this.fadeTime = Time.time;
        this.transform.position = GetRandomPosOnScreen();
        this.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-30f, 30f), 0));
        Invoke(nameof(Disappear), Random.Range(8f, 16f));
    }
    
    public void Disappear()
    {
        IsHere = false;
        this.diappearFade = true;
        this.fadeTime = Time.time;
        Invoke(nameof(Appear), Random.Range(60f, 150f));
    }
}
