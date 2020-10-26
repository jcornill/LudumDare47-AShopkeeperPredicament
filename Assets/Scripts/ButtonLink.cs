using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
