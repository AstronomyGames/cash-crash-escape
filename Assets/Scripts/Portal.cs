using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum PortalProcess
{
    Heal,
    IncreaseShootingRate,
    GenerateSoldiers
}
public class Portal : MonoBehaviour
{
    public PortalProcess process;
    [SerializeField] private Collider otherPortal;
    [SerializeField] private TMP_Text text;
    [SerializeField] private float healthAmount;
    [SerializeField] private int soldiersCount;
    [SerializeField] private float shootingRateAddedAmount;
    private Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;



    }

    public PortalProcess GetPortalValue()
    {
        otherPortal.enabled = false;
        transform.DOScale(localScale + Vector3.one * 0.3f, GlobalSettings.instance.portalPopUpSpeed).OnComplete(() =>
        {
            transform.DOScale(localScale, GlobalSettings.instance.portalPopUpSpeed * 1.5f);
        });
        return process;
    }

    public void Heal(ref float health)
    {
        health += healthAmount;
    }

    public void GenerateSoldiers(Vector3 pos)
    {
        CharactersController.instance.GenerateSoldiers(soldiersCount, pos);
    }

    public void IncreaseShootingRate(ref float weaponShootingRate)
    {
        weaponShootingRate -= shootingRateAddedAmount;
    }


}
