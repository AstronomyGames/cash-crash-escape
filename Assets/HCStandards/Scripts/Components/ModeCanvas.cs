using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeCanvas : MonoBehaviour
{
    [SerializeField] private PanelDesign[] winPanels;
    [SerializeField] private PanelDesign[] failPanels;


    private void EnablePanel(bool win)
    {
        if (win)
        {
            for (int i = 0; i < winPanels.Length; i++)
            {
                if (winPanels[i].GetDesign() == HCStandards.Configs.design)
                {
                    winPanels[i].Enable();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < failPanels.Length; i++)
            {
                if (failPanels[i].GetDesign() == HCStandards.Configs.design)
                {
                    failPanels[i].Enable();
                    break;
                }
            }
        }

    }

    public void Enable(bool win)
    {
        gameObject.SetActive(true);
        EnablePanel(win);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
