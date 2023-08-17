using DG.Tweening;
using MK.Toon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Procedural;
using static UnityEngine.GraphicsBuffer;

public class Player : Character
{
    [SerializeField] private Transform tr;
    [SerializeField] private Transform movementSmoother;
    [SerializeField] private ImageModifier fillImage;
    [SerializeField] private Transform healthCanvas;
    private float health = 1f;
    private float maxPlatformX, minPlatformX;

    protected override void Initialize()
    {
        base.Initialize();
        runSpeed = GlobalSettings.instance.playerRunSpeed;
        TurnSpeed = GlobalSettings.instance.playerTurnSpeed;
        movementSmoother.position = tr.position;
        CharactersController.instance.AddCharacter(this);
        maxPlatformX = GlobalSettings.instance.platformWidth * 0.5f;
        minPlatformX = GlobalSettings.instance.platformWidth * -0.5f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!StartShooting)
            return;
        Shoot(Vector3.zero);
    }

    protected override void Move()
    {
        Vector3 input;
        input.x = tr.position.x - InputManager.instance.deltaX;
        input.z = tr.position.z + Time.deltaTime * runSpeed;
        input.y = tr.position.y;
        input.x = Mathf.Clamp(input.x, minPlatformX, maxPlatformX);
        movementSmoother.position = Vector3.Lerp(movementSmoother.position, input, runSpeed * Time.deltaTime);
        tr.position = Vector3.Lerp(tr.position, movementSmoother.position, runSpeed * Time.deltaTime);

    }
    bool isScaling = false;
    public override void GetHit(float damageAmount)
    {
        if (isDead) return;
        float normalizedDamage = damageAmount * 0.01f;
        if (!healthCanvas.gameObject.activeInHierarchy)
        {
            healthCanvas.localScale = Vector3.zero;
            healthCanvas.gameObject.SetActive(true);
            healthCanvas.DOScale(Vector3.one, 0.5f);
            isScaling = true;
        }
        if ((health - normalizedDamage) <= 0)
        {
            isDead = true;
            health = 0f;
            Die();
            return;
        }

        health -= normalizedDamage;
        if (!isScaling)
        {
            healthCanvas.DORewind();
            healthCanvas.DOPunchScale(Vector3.one * 0.1f, 0.1f, 0, 0f);
        }

        float fillAmount = fillImage.fillAmount;
        DOTween.To(x => fillImage.fillAmount = x, fillAmount, health, 0.3f);
    }

    protected override void Die()
    {
        HCStandards.Game.EndGame(false, 2f);
        base.Die();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            other.tag = "Untagged";
            InteractWithPortal(other.GetComponent<Portal>());
        }

        if (other.CompareTag("Jump"))
        {
            other.enabled = false;
            JumpOnCar();
        }
    }

    private void JumpOnCar()
    {
        AdjustCamera();
        gameObject.layer = LayerMask.NameToLayer("IgnoreBullets");
        AnimationController.PlayAnimation(Animation.Idle);
        HCStandards.Game.EndGame(true, 2f);
        Rb.isKinematic = false;
        Rb.useGravity = true;
        InputManager.instance.Enable(false);
        Rb.velocity = CalculateLaunchData(EndMechanic.instance.GetJumpTarget().position).initialVelocity;

    }

    private static void AdjustCamera()
    {
        CameraController.instance.followSpeed = 1f;
        CameraController.instance.lookSpeed = 1f;
        CameraController.instance.offset += Vector3.forward * 15f-Vector3.up*3f;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.transform.CompareTag("CarBase"))
        {
            collision.transform.tag = "Untagged";
            Rb.isKinematic = true;
            transform.parent = EndMechanic.instance.GetJumpTarget();
            EndMechanic.instance.ActivateCar();
        }
    }

    public virtual LaunchData CalculateLaunchData(Vector3 target)
    {
        float height = 3.5f;
        float displacementY = target.y - Rb.position.y;
        Vector3 displacementXZ = new Vector3(target.x - Rb.position.x, 0, target.z - Rb.position.z);
        float time = Mathf.Sqrt(-2 * height / -9.81f) + Mathf.Sqrt(2 * (displacementY - height) / -9.81f);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * -9.81f * height);
        Vector3 velocityXZ = displacementXZ / time;
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(-9.81f), time);
    }

    private void InteractWithPortal(Portal p)
    {
        switch (p.process)
        {
            case PortalProcess.Heal:
                p.Heal(ref health);
                GetHit(0);
                break;
            case PortalProcess.IncreaseShootingRate:
                float weaponSRate = Weapon.ShootingRate;
                p.IncreaseShootingRate(ref weaponSRate);
                Weapon.UpdateShootingRate(weaponSRate);
                break;
            case PortalProcess.GenerateSoldiers:
                p.GenerateSoldiers(transform.position);
                break;
            default:
                break;
        }
    }

    protected override void GameStarted(bool win)
    {
        base.GameStarted(win);
        StartShooting = true;
        AnimationController.PlayAnimation(Animation.PistolRunBackward, 0f);

    }

    protected override void GameEnded(bool win)
    {
        base.GameEnded(win);
        enabled = false;
        StartShooting = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Weapon.UpdateBulletSpeed(GlobalSettings.instance.bulletSpeed);
    }
}
public struct LaunchData
{
    public readonly Vector3 initialVelocity;
    public readonly float timeToTarget;

    public LaunchData(Vector3 initialVelocity, float timeToTarget)
    {
        this.initialVelocity = initialVelocity;
        this.timeToTarget = timeToTarget;
    }

}