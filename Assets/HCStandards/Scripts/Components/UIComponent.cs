using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UIComponent : MonoBehaviour
{
    public abstract void SetUp();
    public abstract void Enable();
    public abstract void Disable();


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

}
