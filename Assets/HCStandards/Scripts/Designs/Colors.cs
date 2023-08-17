using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colors ", menuName = "HCStandards/UI/Colors")]
public class Colors : ScriptableObject
{
    public UIDesign design;
    [SerializeField] private Color designOutlineColor;
    [SerializeField] private Color loadScreenLoadColor;
    [SerializeField] private Color startGameButtonColor;
    [SerializeField] private Color successButtonColor;
    [SerializeField] private Color failButtonColor;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    public Color GetButtonColor(PanelType type)
    {
        switch (type)
        {
            case PanelType.Loading:
                return loadScreenLoadColor;
            case PanelType.StartGame:
                return startGameButtonColor;
            case PanelType.Fail:
                return failButtonColor;
            case PanelType.Success:
                return successButtonColor;
            default:
                return Color.black;
        }
    }

    public Color GetOutLineColor()
    {
        return designOutlineColor;
    }
}
