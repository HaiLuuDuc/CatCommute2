using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoOutline : MonoBehaviour
{
    private void Awake()
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.outlineColor = new Color(0, 0, 0, 0);
    }
}
