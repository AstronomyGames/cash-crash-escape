using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : Singleton<GlobalSettings>
{
    public float playerRunSpeed;
    public float playerTurnSpeed;
    public float portalPopUpSpeed;
    public int disableCharacterAfterMilliseconds;
    public float platformWidth;
    public float bulletSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

}
