using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "HCStandards/Setting")]
public class DesignSettings : ScriptableObject
{
    public UIDesign design = UIDesign.Apple;
    public float buttonClickScale = 0.825f;
    public float dragSensitivity = 1f;
    public float panelsMoveSpeed = 1f;
}
