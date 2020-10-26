using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    public GameObject textPrefab;

    public TextMeshProUGUI AddText(string id, string text)
    {
        GameObject go = Instantiate(this.textPrefab, transform);
        go.name = id;
        TextMeshProUGUI result = go.GetComponent<TextMeshProUGUI>();
        result.text = text;
        return result;
    }
}
