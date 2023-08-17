using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxBulletTrail;
    protected Transform magazine;
    protected float bulletSpeed = 1f;
    protected float duration = 1f;
    protected float damageAmount = 1f;
    protected Rigidbody rb;
    protected CharacterType source;

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {

    }

    public virtual void Setup(Transform mag, CharacterType source, float bulletSpeed, float damageAmount, float duration)
    {
        rb = GetComponent<Rigidbody>();
        magazine = mag;
        this.source = source;
        this.bulletSpeed = bulletSpeed;
        this.damageAmount = damageAmount;
        this.duration = duration;
        transform.parent = LevelManager.instance.transform;
        if (source == CharacterType.Player || source == CharacterType.FriendNonPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        else if (source == CharacterType.EnemyNonPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }

    public virtual void Launch(Vector3 target)
    {
        CancelInvoke();
     
        rb.isKinematic = false;
        gameObject.SetActive(true);
        rb.position = magazine.position;
        Vector3 dir = target - magazine.position;
        transform.forward = dir.normalized;
        rb.velocity = dir.normalized * bulletSpeed;
        vfxBulletTrail.Play();
        Invoke("ResetBullet", duration);
    }

    protected virtual void ResetBullet()
    {
        rb.isKinematic = true;
        if (!gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(false);
        vfxBulletTrail.Stop();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        ResetBullet();
        if (collision.transform.CompareTag("Character"))
        {
            collision.gameObject.GetComponent<Character>().GetHit(damageAmount);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ResetBullet();
    }

    internal void UpdateSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }
}
