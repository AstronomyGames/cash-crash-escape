using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunchMachine : MonoBehaviour, IActivable
{
    [SerializeField] private Rigidbody[] chain;
    [SerializeField] private CharacterJoint[] joints;
    [SerializeField] private int startHitCount;
    [SerializeField] private Transform countCanvas;
    [SerializeField] private Transform[] punches;
    [SerializeField] private TMP_Text text_HitCount;


    public void GetHit()
    {

        startHitCount--;
        text_HitCount.text = startHitCount.ToString();
        for (int i = 0; i < chain.Length; i++)
        {
            if (!chain[i].isKinematic)
                chain[i].AddForce(Vector3.one * Random.Range(50, 200));

        }

        if (startHitCount == 0)
        {

            GetComponent<Collider>().enabled = false; ;
            countCanvas.DOScale(0f, 0.5f);
            countCanvas.DORotate(Vector3.forward * 360f, 0.5f);
            for (int i = 0; i < joints.Length; i++)
            {
                joints[i].connectedBody = null;
                joints[i].breakForce = 0f;
                chain[i].isKinematic = false;
            }

            for (int i = 0; i < punches.Length; i++)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(punches[i].DOLocalMoveZ(1.15f, 0.4f).SetEase(Ease.InOutQuint).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0.5f, 1.5f)));
                seq.AppendInterval(.4f);
            }
        }
    }
}
