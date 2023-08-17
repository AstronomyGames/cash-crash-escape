using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMechanic : Singleton<EndMechanic>
{

    
    [SerializeField] private Animator carAnimator;
    [SerializeField] private Transform landPoint;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ActivateCar()
    {
        if (carAnimator.enabled)
            return;
        carAnimator.enabled = true;

    }

    public Transform GetJumpTarget()
    {
        return landPoint;
    }
}
