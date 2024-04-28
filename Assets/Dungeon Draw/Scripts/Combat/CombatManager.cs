using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<Entity> enemies = new List<Entity>();
    
    /*
    Use StartFight() to start the fight
    It waits for the player to play first if PlayerPlayFirst is true
    Use the end turn button to end the player turn (EndTurn())
    Play the enemy turn with the coroutine EnemyTurn() (TODO: Replace with enemy turn logic)
    And wait for the player to play again
     */

    public bool PlayerPlayFirst { get; set; } = true;

    [HideInInspector]
    public static CombatManager Instance { get; private set; }

    private bool _isPlayerTurn;
    public bool IsPlayerTurn {
        get => _isPlayerTurn;
        private set { 
            _isPlayerTurn = value; 
            Debug.Log(_isPlayerTurn ? "Player turn" : "Enemy turn"); 
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start() //TODO: Remove (for testing purposes only)
    {
        StartFight();
    }

    public void StartFight()
    {
        Debug.Log("Fight started");
        IsPlayerTurn = PlayerPlayFirst;
        
        //TODO: Remove (for testing purposes only)
        CardStats[] cards =
        {
            new CardStats("Attack", CardType.Attack, CardRarity.Common, 1, new List<Effect> {new DealDamage(1, 5)}),
            
        };
        enemies.Add(new Entity(10));
        enemies[0].ApplyCard(cards[0]);
    }

    public void EndTurn()
    {
        foreach (Entity enemy in enemies)
        {
            enemy.UpdateEffects();
            Debug.Log("Enemy [" + enemy + "] health: " + enemy.Health);
        }
        IsPlayerTurn = !IsPlayerTurn;
        if (!IsPlayerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f); //TODO: Replace with enemy turn logic
        EndTurn();
    }

}