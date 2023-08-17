using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NonPlayerFriend : Character
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ITarget target;

    protected override void Initialize()
    {
        base.Initialize();
        agent = agent ? agent : GetComponent<NavMeshAgent>();
        agent.enabled = false;
        meshRenderer.sharedMaterial = CharactersController.instance.GetMaterial(CharacterType);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (target == null)
            return;

        if (target.IsDead)
        {
            UpdateTarget();
            return;
        }
        Shoot(target.TargetTransform.position);
    }

    protected override void Move()
    {
        if (!StartShooting || target == null)
            return;

        Vector3 lookDir = target.TargetTransform.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public override void Activate()
    {
        base.Activate();
        isActive = true;
        Rb.isKinematic = true;
        StartShooting = true;
        AnimationController.PlayAnimation(Animation.AssaultIdle);
        UpdateTarget();
        meshRenderer.sharedMaterial = CharactersController.instance.GetMaterial(CharacterType);
    }

    public override void GetHit(float damageAmount)
    {
        if (IsDead) return;
        if (!isActive) return;
        isDead = true;

        Die();
    }

    protected override void Die()
    {
        meshRenderer.sharedMaterial = CharactersController.instance.GetDeathMaterial();
        CharactersController.instance.RemoveCharacter(this);
        agent.enabled = false;
        isDead = true;
        base.Die();
    }

    public void Spread()
    {
        CharactersController.instance.AddCharacter(this);
        Vector3 force = Vector3.one;
        Rb.isKinematic = false;
        Rb.useGravity = true;
        force.y = 0f;
        force.z = UnityEngine.Random.Range(0.7f, 0.9f);
        force.x = UnityEngine.Random.Range(-0.5f, 0.9f);
        Rb.AddForce(force * 300f);
        Rb.DORotate(Rb.velocity.normalized, 1f).OnUpdate(() =>
        {
            //transform.rotation = Quaternion.LookRotation(Rb.velocity.normalized);
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -3f, 3f);
            transform.position = pos;

        }).OnComplete(() =>
        {
            Rb.useGravity = false;
        });

        AnimationController.PlayAnimation(Animation.Slide);
        // CharacterType = source;
        Invoke("Activate", 1f);
    }

    protected override void Shoot(Vector3 targetPos)
    {
        if (target == null)
            return;

        if (target.IsDead)
        {
            UpdateTarget();
            return;
        }
        base.Shoot(target.TargetTransform.position);
    }

    private void UpdateTarget()
    {
        target = CharactersController.instance.GetTarget(CharacterType);
        if (target == null)
        {
            AnimationController.PlayAnimation(Animation.Idle);
            StartShooting = false;
            Debug.LogError("There is not targets");
            return;
        }
        AnimationController.PlayAnimation(Animation.AssaultIdle);
        if (!StartShooting)
            StartShooting = true;

    }

    public async override Task Build()
    {
        await base.Build();

        agent.enabled = false;
        target = null;
    }

    protected override void GameStarted(bool win)
    {
        base.GameStarted(win);
    }

    protected override void GameEnded(bool win)
    {
        base.GameEnded(win);
        AnimationController.PlayAnimation(Animation.Idle);
    }

    internal void Spawn(Vector3 generatedPosition)
    {
        int randX = UnityEngine.Random.Range(-3, 3);
        int randZ = UnityEngine.Random.Range((int)generatedPosition.z - 2, (int)generatedPosition.z + 2);
        generatedPosition.x = randX;
        generatedPosition.z = randZ;
        transform.position = generatedPosition - Vector3.up * 5f;
        gameObject.SetActive(true);
        StartShooting = false;
        AnimationController.PlayAnimation(Animation.Idle);
        transform.DOMoveY(generatedPosition.y, 0.5f).OnComplete(() =>
        {
            Activate();
        });
    }
}
