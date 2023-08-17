using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextComponent : UIComponent
{
    //private void SetText()
    //{
    //
    //}

    //private void OnEnable()
    //{
    //    HCStandards.HCStandards.Initialize += SetText;
    //}

    //private void OnDisable()
    //{
    //    HCStandards.HCStandards.Initialize -= SetText;
    //}

    public override void SetUp()
    {
        GetComponent<Text>().font = HCStandards.Design.GetFont();
    }

    public override void Enable()
    {

    }

    public override void Disable()
    {

    }
}
