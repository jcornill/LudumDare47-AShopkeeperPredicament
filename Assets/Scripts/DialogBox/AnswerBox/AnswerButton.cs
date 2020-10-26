using TMPro;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private void Awake()
    {
        this.textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        this.textMesh.text = text.TrimStart().TrimEnd();
    }
}
