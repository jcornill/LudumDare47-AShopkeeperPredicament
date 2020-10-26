using UnityEngine;
using Random = UnityEngine.Random;

public class MSC : MonoBehaviour
{
    public static bool IsHere;
    
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke(nameof(Appear), Random.Range(60f, 150f));
        this.spriteRenderer.enabled = false;
    }
    

    public void Appear()
    {
        IsHere = true;
        this.spriteRenderer.enabled = true;
        Invoke(nameof(Disappear), Random.Range(8f, 16f));
    }
    
    public void Disappear()
    {
        IsHere = false;
        this.spriteRenderer.enabled = false;
        Invoke(nameof(Appear), Random.Range(60f, 150f));
    }
}
