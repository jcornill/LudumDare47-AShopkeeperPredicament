using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DevianceBar : MonoBehaviour
{
    public Image bar;

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = NPCPlayerData.Deviance;
    }
}
