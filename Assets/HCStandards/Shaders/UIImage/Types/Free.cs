using UnityEngine;
using UnityEngine.UI.Procedural;

[ModifierID("Free")]
public class Free : BaseModifier
{
	[SerializeField] private Vector4 radius;

	public Vector4 Radius
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
		return radius;
	}



	protected void OnValidate()
	{
		radius.x = Mathf.Max(0, radius.x);
		radius.y = Mathf.Max(0, radius.y);
		radius.z = Mathf.Max(0, radius.z);
		radius.w = Mathf.Max(0, radius.w);
	}
}
