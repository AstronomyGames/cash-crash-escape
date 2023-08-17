using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HCStandards
{
    public static class Design
    {
        private static DesignSettings setting;

        public static UIDesign design = UIDesign.Apple;
        public static float buttonClickScale = 0.825f;
        public static float dragSensitivity = 1f;
        public static float panelsMoveSpeed = 1f;

        public static Font GetFont()
        {
            return null;
        }

        //public Color GetColor(PanelType type)
        //{
        //    for (int i = 0; i < colors.Count; i++)
        //    {
        //        if (colors[i].design == design)
        //        {
        //            return colors[i].GetButtonColor(type);
        //        }
        //    }
        //    Debug.LogWarning("Design Not Found");
        //    return Color.black;
        //}
    }
}

