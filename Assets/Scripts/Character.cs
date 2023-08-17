using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


public enum CharacterType
{
    Player, EnemyNonPlayer, FriendNonPlayer
}

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour, ITarget
{

    [SerializeField] protected CharacterType type;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform bodyTarget;
    [SerializeField] private Transform hipsTr;
    [SerializeField] private Rigidbody[] ragdollRbs;
    [SerializeField] private Collider[] ragdollColliders;

    protected float runSpeed { get; set; }
    protected Rigidbody Rb { get; set; }
    protected Rigidbody[] RagdollRbs { get => ragdollRbs; }
    protected Collider[] RagdollColliders { get => ragdollColliders; }
    protected Weapon Weapon { get => weapon; }
    protected float TurnSpeed { get; set; }
    protected AnimationController AnimationController { get => animationController; }
    protected bool StartShooting { get; set; }
    public CharacterType CharacterType { get => type; set => type = value; }
    public Transform TargetTransform { get => bodyTarget; }
    public Transform HipsTr { get => hipsTr; }
    public bool IsDead { get => isDead; }
    private float weaponShootingRate;
    protected bool isDead = false;
    protected bool isActive = false;
    protected Vector3 hipsLocalPosition;

    protected virtual void Awake()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        Rb = GetComponent<Rigidbody>();
        weapon.Setup(CharacterType);
        weaponShootingRate = weapon.ShootingRate;
        StartShooting = false;
        if (CharacterType == CharacterType.Player || CharacterType == CharacterType.FriendNonPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        else if (CharacterType == CharacterType.EnemyNonPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        hipsLocalPosition = hipsTr.localPosition;

    }

    protected virtual void FixedUpdate()
    {
        if (!HCStandards.Game.IsGameStarted)
            return;


        if (!StartShooting)
            return;


        Move();
    }

    protected virtual void Move()
    {

    }

    protected virtual void Shoot(Vector3 targetPos)
    {
        if (weaponShootingRate <= 0f)
        {
            weapon.Shot(targetPos);
            weaponShootingRate = weapon.ShootingRate;
        }
        weaponShootingRate -= Time.deltaTime;
    }

    public virtual void GetHit(float damageAmount)
    {

    }

    protected virtual void Die()
    {
        enabled = false;
        isDead = true;
        
        // animationController.PlayAnimation(Animation.Die);
        weapon.gameObject.SetActive(false);
        StartCoroutine(ActivateRagdoll(0f));

    }

    public virtual void Activate()
    {
        hipsTr.localPosition = hipsLocalPosition;
        Rb.isKinematic = true;
        GetComponent<Collider>().enabled = true;
        for (int i = 0; i < ragdollRbs.Length; i++)
        {
            ragdollRbs[i].isKinematic = true;
            ragdollColliders[i].enabled = false;
        }
    }

    private IEnumerator ActivateRagdoll(float delay)
    {
        yield return new WaitForSeconds(delay);
        animationController.Enable(false);
        GetComponent<Collider>().enabled = false;
        Rb.isKinematic = false;
        for (int i = 0; i < ragdollColliders.Length; i++)
        {
            ragdollColliders[i].enabled = true;
            ragdollRbs[i].isKinematic = false;
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            if (!isDead)
                Die();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void OnTargetGenerated()
    {

    }

    protected virtual void GameStarted(bool win)
    {

    }

    protected virtual void GameEnded(bool win)
    {
        enabled = false;
    }

    public async virtual Task Build()
    {
        await Task.Delay(GlobalSettings.instance.disableCharacterAfterMilliseconds);

        StartShooting = false;
    }

    protected virtual void OnEnable()
    {

        HCStandards.Game.onGameStarted += GameStarted;
        HCStandards.Game.onGameEnded += GameEnded;
    }

    protected virtual void OnDisable()
    {
        HCStandards.Game.onGameStarted -= GameStarted;
        HCStandards.Game.onGameEnded -= GameEnded;
    }
}

public interface ITarget
{
    public Transform TargetTransform { get; }
    public bool IsDead { get; }
}