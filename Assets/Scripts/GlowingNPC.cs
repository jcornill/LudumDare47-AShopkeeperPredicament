using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingNPC : MonoBehaviour
{
    public Color A;

    public Color B;

    private bool oneTime;
    
    // Update is called once per frame
    private void Update()
    {
        if (Ending.EndingStart && this.oneTime == false)
        {
            this.oneTime = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        
        
        if (NPCPlayerData.DevianceLevel != 6 || Ending.EndingStart)
        {
            return;
        }
        float time = Time.time % 2;
        if (time > 1)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(this.B, this.A, time - 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(this.A, this.B, time );
        }
    }
}
