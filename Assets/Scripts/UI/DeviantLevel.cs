using UnityEngine;
using UnityEngine.UI;

public class DeviantLevel : MonoBehaviour
{
    public Sprite[] sprites;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().sprite = this.sprites[NPCPlayerData.DevianceLevel - 3];
    }
}
