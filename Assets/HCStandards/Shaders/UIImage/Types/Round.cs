using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Procedural;

[ModifierID("Round")]
public class Round : BaseModifier
{
    public override Vector4 CalculateRadius(Rect imageRect)
    {
        float r = Mathf.Min(imageRect.width, imageRect.height) * 0.5f;
        return new Vector4(r, r, r, r);
    }
}
