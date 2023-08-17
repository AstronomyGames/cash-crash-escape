using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;


    private void SetLevelText()
    {
        levelText.text= "Level "+HCStandards.DataManager.GetData().Level.ToString();
    }

    private void OnEnable()
    {
        SetLevelText();
    }
}
