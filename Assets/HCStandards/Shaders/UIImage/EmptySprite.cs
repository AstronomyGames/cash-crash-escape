﻿using UnityEngine;
using System.Collections;

public static class EmptySprite
{
    static Sprite instance;
    public static Sprite Get()
    {
        if (instance == null)
        {           
            instance = Resources.Load<Sprite>("procedural_ui_image_default_sprite");
        }
        return instance;
    }

    public static bool IsEmptySprite(Sprite s)
    {
        if (Get() == s)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
