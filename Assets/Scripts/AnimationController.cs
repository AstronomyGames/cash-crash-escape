using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public enum Animation
{
    Idle, PistolRunForward, PistolRunBackward, PistolIdle, AssaultRunForward, AssaultRunBackward, AssaultIdle, Slide, Die
}
[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IEnumerator animNumerator;

    public void PlayAnimation(Animation animation, float delay = 0f)
    {
        if (animNumerator != null)
            StopCoroutine(animNumerator);
        animNumerator = AnimationPlayer(animation, delay);
        StartCoroutine(animNumerator);

    }

    internal void Enable(bool v)
    {
        animator.enabled = v;
    }

    private IEnumerator AnimationPlayer(Animation animation, float delay = 0f)
    {
        if (delay != 0f)
            yield return new WaitForSeconds(delay);

        switch (animation)
        {
            case Animation.Idle:
                animator.SetInteger("State", 0);
                break;
            case Animation.PistolRunForward:
                animator.SetInteger("State", 1);
                break;
            case Animation.PistolRunBackward:
                animator.SetInteger("State", 2);
                break;
            case Animation.PistolIdle:
                animator.SetInteger("State", 3);
                break;
            case Animation.AssaultRunForward:
                animator.SetInteger("State", 3);
                break;
            case Animation.AssaultRunBackward:
                animator.SetInteger("State", 5);
                break;
            case Animation.AssaultIdle:
                animator.SetInteger("State", 6);
                break;
            case Animation.Slide:
                animator.SetInteger("State", 7);
                break;
            case Animation.Die:
                animator.SetInteger("State", 8);
                break;
            default:
                break;
        }

    }
}
