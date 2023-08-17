using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ring : MonoBehaviour, IActivable
{
    [SerializeField] private NonPlayerFriend[] nonPlayers;
    [SerializeField] private SkinnedMeshRenderer[] rubbers;
    [SerializeField] private int shotCountForeachRubber;
    [SerializeField] private Transform countCanvas;
    [SerializeField] private TMP_Text text_HitCount;
    [SerializeField] private int hitCount = 0;
    private int currentRubber;

    private void Awake()
    {
        text_HitCount.text = hitCount.ToString();
    }

    public void GetHit()
    {
        hitCount--;
        text_HitCount.text = hitCount.ToString();
        if (hitCount % shotCountForeachRubber == 0)
        {
            rubbers[currentRubber].SetBlendShapeWeight(0, 100);
            currentRubber++;

        }
        if (hitCount == 0)
        {
            countCanvas.DOScale(0f, 0.5f);
            countCanvas.DORotate(Vector3.forward * 360f, 0.5f);
            ReleaseStickmans();
            tag = "Untagged";
        }
    }

    private void ReleaseStickmans()
    {
        for (int i = 0; i < nonPlayers.Length; i++)
        {
            nonPlayers[i].Spread();
        }
    }
}
