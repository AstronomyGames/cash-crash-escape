using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cage : MonoBehaviour, IActivable
{
    [SerializeField] private Animator animator;
    [SerializeField] private int hitCountToActivate;
    [SerializeField] private Rigidbody[] ballsRb;
    [SerializeField] private Transform countCanvas;
    [SerializeField] private TMP_Text text_HitCount;
    [SerializeField] private float clearBallsAfter;

    private void Awake()
    {
        text_HitCount.text=hitCountToActivate.ToString();
    }

    public void GetHit()
    {
        hitCountToActivate--;
        text_HitCount.text = hitCountToActivate.ToString();
        if (hitCountToActivate == 0)
        {
            GetComponent<Collider>().enabled = false;
            animator.enabled = true;
            countCanvas.DOScale(0f, 0.5f);
            countCanvas.DORotate(Vector3.forward * 360f, 0.5f);
            for (int i = 0; i < ballsRb.Length; i++)
            {
                ballsRb[i].isKinematic = false;
            }
            Invoke("ClearBalls", clearBallsAfter);
        }
    }


    private void ClearBalls()
    {
        for (int i = 0; i < ballsRb.Length; i++)
        {
            ballsRb[i].gameObject.SetActive(false);
        }
    }
}


public interface IActivable
{
    public void GetHit();
}