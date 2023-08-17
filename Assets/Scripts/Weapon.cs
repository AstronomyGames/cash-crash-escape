using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private Transform magazine;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damageAmount;
    [SerializeField] private float shootingRate;
    [SerializeField] private float duration;
    public float ShootingRate { get => shootingRate; set => shootingRate = value; }
    public float DamageAmount { get => damageAmount; }
    public float BulletSpeed { get => bulletSpeed; }
    protected int currentBullet = 0;
    protected Transform Magazine { get => magazine; }
    protected Bullet[] Bullets { get => bullets; }

    public virtual void Setup(CharacterType source)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].Setup(magazine, source, bulletSpeed, damageAmount, duration);
        }
    }

    public virtual void Shot(Vector3 target)
    {
        Vector3 magPos = magazine.position;
        magPos += Vector3.back * 100;
        bullets[currentBullet].Launch(magPos);
        currentBullet++;
        if (currentBullet >= bullets.Length)
            currentBullet = 0;
    }

    public virtual void UpdateShootingRate(float shootingRate)
    {
        this.shootingRate = shootingRate;
    }

    public virtual void Aim(Vector3 target)
    {

    }

    internal void UpdateBulletSpeed(float bulletSpeed)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].UpdateSpeed(bulletSpeed);
        }
    }
}
