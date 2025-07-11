using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GlobalFontSetter : MonoBehaviour
{
    public TMP_FontAsset newFont;

    void Start()
    {
        TMP_Text[] allTexts = FindObjectsOfType<TMP_Text>(true); // true = include inactive
        foreach (var tmp in allTexts)
        {
            tmp.font = newFont;
        }
    }
}