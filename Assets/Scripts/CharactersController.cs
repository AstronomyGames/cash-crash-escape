using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CharactersController : Singleton<CharactersController>
{
    private List<Character> nonPlayersEnemy = new List<Character>();
    private List<Character> nonPlayersFriends = new List<Character>();
    private Character player;
    [SerializeField] private CharacterMaterial[] characterMaterials;
    [SerializeField] private Material deathMaterial;
    private Queue<NonPlayerFriend> soldiersPool = new Queue<NonPlayerFriend>();
    private Queue<NonPlayerEnemy> enemiesPool = new Queue<NonPlayerEnemy>();

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private int targetActiveEnemies;
    [SerializeField] private int activeEnemies;
    public Character Player { get => player; }
    protected override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
        SetPool();
    }
    private void SetPool()
    {
        GameObject npc;

        for (int i = 0; i < 25; i++)
        {
            npc = Instantiate(enemyPrefab);
            npc.SetActive(false);
            enemiesPool.Enqueue(npc.GetComponent<NonPlayerEnemy>());

            npc = Instantiate(soldierPrefab);
            npc.SetActive(false);
            soldiersPool.Enqueue(npc.GetComponent<NonPlayerFriend>());
        }
    }

    public Material GetDeathMaterial()
    {
        return deathMaterial;
    }

    public ITarget GetTarget(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.EnemyNonPlayer:
                return GetFriendCharacter();
            case CharacterType.FriendNonPlayer:
                if (nonPlayersEnemy.Count == 0)
                    return null;
                return nonPlayersEnemy[Random.Range(0, nonPlayersEnemy.Count)];
            default:
                return null;
        }
    }

    private Character GetFriendCharacter()
    {
        if (nonPlayersFriends.Count > 0)
        {
            int rand = Random.Range(0, nonPlayersFriends.Count);
            if (!nonPlayersFriends[rand].IsDead)
            {
                return nonPlayersFriends[rand];
            }
            else
            {
                nonPlayersFriends.RemoveAt(rand);
                if (nonPlayersFriends.Count == 0)
                    return player;
                rand = Random.Range(0, nonPlayersFriends.Count);
                return nonPlayersFriends[rand];
            }
        }
        else
        {
            return player;
        }
    }

    public Material GetMaterial(CharacterType type)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].CharacterType == type)
            {
                return characterMaterials[i].material;
            }
        }

        Debug.Log("Type Not Found");
        return null;
    }

    public void AddCharacter(Character character)
    {
        switch (character.CharacterType)
        {
            case CharacterType.Player:

                player = character;
                break;
            case CharacterType.EnemyNonPlayer:
                if (!nonPlayersEnemy.Contains(character))
                    nonPlayersEnemy.Add(character);
                break;
            case CharacterType.FriendNonPlayer:
                if (!nonPlayersFriends.Contains(character))
                    nonPlayersFriends.Add(character);
                UpdateEnemiesTarget();
                break;
            default:
                break;
        }
    }

    private void UpdateEnemiesTarget()
    {
        for (int i = 0; i < nonPlayersEnemy.Count; i++)
        {
            nonPlayersEnemy[i].OnTargetGenerated();
        }
    }

    public Transform GetEnemyTransform()
    {
        if (nonPlayersEnemy.Count == 0)
            return null;
        return nonPlayersEnemy[Random.Range(0, nonPlayersEnemy.Count)].transform;
    }

    internal void RemoveCharacter(Character character)
    {

        // Debug.Log("B " + enemiesPool.Count);

        switch (character.CharacterType)
        {
            case CharacterType.EnemyNonPlayer:
                if (nonPlayersEnemy.Contains(character))
                {
                    activeEnemies--;
                    nonPlayersEnemy.Remove(character);
                }
                else
                {
                    return;
                }
                enemiesPool.Enqueue((NonPlayerEnemy)character);
                if (activeEnemies < targetActiveEnemies)
                {
                    Debug.Log("Curr " + activeEnemies);
                    int enemiesToActivate = targetActiveEnemies - activeEnemies;
                    Debug.Log("Needed " + enemiesToActivate);
                    GenerateEnemies(enemiesToActivate, character.transform.position);
                }
                break;
            case CharacterType.FriendNonPlayer:
                if (nonPlayersFriends.Contains(character))
                    nonPlayersFriends.Remove(character);
                soldiersPool.Enqueue((NonPlayerFriend)character);
                break;
            default:
                break;
        }
        //Debug.Log("A " + enemiesPool.Count);
        character.Build();
    }


    public void GenerateEnemies(int count, Vector3 generatedPosition)
    {
        if (count > enemiesPool.Count)
            count = enemiesPool.Count;
        activeEnemies += count;
        Debug.Log("Af " + activeEnemies);
        for (int i = 0; i < count; i++)
        {
            enemiesPool.Dequeue().Spawn(generatedPosition - Vector3.forward * 6f);
        }
    }

    public void GenerateSoldiers(int soldiersCount, Vector3 generatedPosition)
    {
        if (soldiersCount > soldiersPool.Count)
            soldiersCount = soldiersPool.Count;

        for (int i = 0; i < soldiersCount; i++)
        {
            soldiersPool.Dequeue().Spawn(generatedPosition);
        }
    }
}
[System.Serializable]
public struct CharacterMaterial
{
    public CharacterType CharacterType;
    public Material material;
}