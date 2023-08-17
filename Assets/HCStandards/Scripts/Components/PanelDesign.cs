using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelDesign : UIComponent
{
    [SerializeField] private UIDesign panelDesign;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private bool isActive = false;

    public UIDesign GetDesign()
    {
        return panelDesign;
    }

    public void ButtonCall()
    {
        if (isActive)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }





    public override void Enable()
    {
        KillActiveTweens();
        isActive = true;
        switch (panelDesign)
        {
            case UIDesign.Apple:
                gameObject.SetActive(true);
                rectTransform.DOAnchorPosY(-210f, HCStandards.Configs.panelsMoveSpeed, true).SetEase(Ease.InOutCirc);
                break;
            case UIDesign.Lemon:
                rectTransform.localScale = Vector3.zero;
                gameObject.SetActive(true);
                rectTransform.DOScale(1f, HCStandards.Configs.panelEnableSpeed);
                break;
            case UIDesign.Strawberrie:
                break;
            case UIDesign.Blackberrie:
                break;
            default:
                break;
        }
    }

    public override void Disable()
    {
        KillActiveTweens();
        isActive = false;
        switch (panelDesign)
        {
            case UIDesign.Apple:
                rectTransform.DOAnchorPosY(20f, HCStandards.Configs.panelsMoveSpeed, true).SetEase(Ease.OutCirc).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
                break;
            case UIDesign.Lemon:
                rectTransform.DOScale(0f, HCStandards.Configs.panelDisableSpeed).OnComplete(() =>
                gameObject.SetActive(false)
                );
                break;
            case UIDesign.Strawberrie:
                break;
            case UIDesign.Blackberrie:
                break;
            default:
                break;
        }
    }

    private void KillActiveTweens()
    {
        rectTransform.DOKill(false);
    }

    public override void SetUp()
    {
        rectTransform = GetComponent<RectTransform>();
    }


}
