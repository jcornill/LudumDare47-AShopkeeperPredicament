using UnityEngine;

public class PowerGain : MonoBehaviour
{
    private bool anyKey;
    
    // Update is called once per frame
    private void Update()
    {
        if (NPCPlayerData.DevianceLevel == 0)
        {
            return;
        }
        
        if (Input.anyKey && this.anyKey == false)
        {
            this.anyKey = true;
            NPCPlayerData.PowerCharge += 0.001f;
        }
        else if (Input.anyKey == false)
        {
            this.anyKey = false;
        }
    }
}
