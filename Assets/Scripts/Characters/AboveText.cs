using TMPro;
using UnityEngine;

public class AboveText : MonoBehaviour
{
    private static int ID;

    private TextMeshProUGUI text;
    
    private void Start()
    {
        this.text = GameObject.Find("WorldCanvas").GetComponent<WorldCanvas>().AddText(ID.ToString(), ID.ToString());
        this.text.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.25f));
        ID++;
    }

    // Update is called once per frame
    private void Update()
    {
        if (NPCPlayerData.DevianceLevel == 0)
        {
            this.text.text = "";
        }
        else
        {
            this.text.text = GetComponent<CharacterMovement>().gameCharacter.GetFullName(NPCPlayerData.DevianceLevel >= 2);
        }
        this.text.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.25f));
    }

    private void OnDestroy()
    {
        if (this.text.gameObject != null)
        {
            Destroy(this.text.gameObject);
        }
    }
}
