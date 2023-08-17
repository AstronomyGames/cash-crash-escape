using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Procedural;

[ModifierID("Uniform")]
public class Uniform : BaseModifier
{
    [SerializeField] private float radius;

    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
            _Graphic.SetVerticesDirty();
        }
    }

    public override Vector4 CalculateRadius(Rect imageRect)
    {
        float r = this.radius;
        return new Vector4(r, r, r, r);
    }

}
