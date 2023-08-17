using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NonPlayerEnemy : Character
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ITarget target;
    [SerializeField] private float targetFollowSpeed;
    [SerializeField] private float distance;

    protected override void Initialize()
    {
        base.Initialize();

        agent = agent ? agent : GetComponent<NavMeshAgent>();
        agent.enabled = false;
        meshRenderer.sharedMaterial = CharactersController.instance.GetMaterial(CharacterType);
        targetFollowSpeed = agent.speed;
    }
    public Transform tar;
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
        tar = target.TargetTransform;
        SetFollowSpeed();
        Shoot(target.TargetTransform.position);
    }

    private void SetFollowSpeed()
    {
        distance = (target.TargetTransform.position - transform.position).sqrMagnitude;
        if (distance > 120f)
        {
            agent.speed += Time.deltaTime * 0.2f;
        }
        else
        {
            agent.speed = targetFollowSpeed;
        }
    }

    protected override void Move()
    {
        if (target == null || CharacterType == CharacterType.FriendNonPlayer)
            return;

        if (Time.frameCount % 10 == 0)
        {
            agent.destination = target.TargetTransform.position;
        }
    }

    public override void Activate()
    {

        base.Activate();
        CharactersController.instance.AddCharacter(this);
        isActive = true;
        enabled = true;
        GetComponent<Collider>().enabled = true;
        agent.enabled = true;
        Rb.isKinematic = true;
        StartShooting = true;
        AnimationController.Enable(true);
        Weapon.gameObject.SetActive(true);
        AnimationController.PlayAnimation(Animation.AssaultRunForward);
        UpdateTarget();
        meshRenderer.sharedMaterial = CharactersController.instance.GetMaterial(CharacterType);
    }

    public override void OnTargetGenerated()
    {
        UpdateTarget();
    }

    public override void GetHit(float damageAmount)
    {
        if (IsDead) return;
        if (!isActive) return;
        meshRenderer.sharedMaterial = CharactersController.instance.GetDeathMaterial();
        Die();
    }

    protected override void Die()
    {
        CharactersController.instance.RemoveCharacter(this);
        agent.enabled = false;
        isDead = true;
        base.Die();
    }

    public void Spread(CharacterType source)
    {
        Vector3 force = Vector3.one;
        Rb.isKinematic = false;
        force.z = UnityEngine.Random.Range(0.7f, 0.9f);
        force.y = 0f;
        force.x = UnityEngine.Random.Range(-0.5f, 0.9f);
        Rb.AddForce(force * 300f);
        // CharacterType = source;
        Invoke("Activate", 1f);
    }

    protected override void Shoot(Vector3 targetPos)
    {
        if (target == null)
        {

            return;
        }

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
            Debug.LogError("There is not targets");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Punch"))
        {
            if (isDead)
                return;
            Die();
            ThrowCharacter(other);
        }
    }

    private void ThrowCharacter(Collider other)
    {
        agent.enabled = false;
        Rb.isKinematic = false;
        Vector3 hitDirection = transform.position - other.transform.position;
        hitDirection.Normalize();
        for (int i = 0; i < RagdollRbs.Length; i++)
        {
            RagdollRbs[i].isKinematic = false;
            RagdollRbs[i].AddForce(hitDirection * 1350f + Vector3.up * 10);
        }
    }

    public async override Task Build()
    {
        await base.Build();

        agent.enabled = false;
        target = null;
        isDead = false;
    }

    internal void Spawn(Vector3 generatedPosition)
    {
        int randX = UnityEngine.Random.Range(-3, 3);
        int playerZ = (int)CharactersController.instance.Player.transform.position.z - 20;
        int randZ = UnityEngine.Random.Range(playerZ - 2, playerZ + 2);
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

    protected override void GameStarted(bool win)
    {
        base.GameStarted(win);


        Activate();
    }

    protected override void GameEnded(bool win)
    {
        base.GameEnded(win);
        agent.enabled = false;
        target = null;
        AnimationController.PlayAnimation(Animation.Idle);
    }
}
