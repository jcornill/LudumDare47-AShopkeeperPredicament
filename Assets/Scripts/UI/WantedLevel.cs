using UnityEngine;
using UnityEngine.UI;

public class WantedLevel : MonoBehaviour
{
    public Image bar;
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = NPCPlayerData.WantedLevel;
    }
}
