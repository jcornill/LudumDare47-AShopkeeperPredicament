using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = NPCPlayerData.Money.ToString();
    }
}
