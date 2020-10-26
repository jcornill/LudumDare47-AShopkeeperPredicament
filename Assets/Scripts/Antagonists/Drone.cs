using UnityEngine;
using Random = UnityEngine.Random;

public class Drone : MonoBehaviour
{
    public static bool IsHere;
    
    public SpriteRenderer spriteRenderer;

    private float fadeChangeTime = 2f;
    private float speed = 5f;
    
    private bool appearFade;
    private bool diappearFade;
    private float fadeTime;

    private Vector3 targetPos;
    private bool askUpdatePos;

    private void Start()
    {
        this.transform.position = GetRandomPosOnScreen();
        this.targetPos = GetRandomPosOnScreen();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.color = new Color(0, 0, 0, 0);
        Invoke(nameof(Appear), Random.Range(40f, 120f));
    }

    private void Update()
    {
        UpdateFade();
        UpdateMoving();
        UpdateDir();
    }

    public void UpdateDir()
    {
        if (this.transform.position.x - this.targetPos.x < 0f) // move left
        {
            this.spriteRenderer.flipX = true;
        }
        if (this.transform.position.x - this.targetPos.x > 0f) // move right
        {
            this.spriteRenderer.flipX = false;
        }
    }
    
    public void UpdateMoving()
    {
        if (Vector3.Distance(this.transform.position, this.targetPos) < 1f && this.askUpdatePos == false)
        {
            askUpdatePos = true;
            Invoke(nameof(UpdateTargetPos), Random.Range(0f, 5f));
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPos, Time.deltaTime * this.speed);
    }

    public void UpdateTargetPos()
    {
        askUpdatePos = false;
        targetPos = GetRandomPosOnScreen();
    }

    public Vector3 GetRandomPosOnScreen()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-3.5f, 3.5f), 0);
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
        Invoke(nameof(Disappear), Random.Range(12f, 24f));
    }
    
    public void Disappear()
    {
        IsHere = false;
        this.diappearFade = true;
        this.fadeTime = Time.time;
        Invoke(nameof(Appear), Random.Range(40f, 120f));
    }
}
