using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int totalRewards = 206;
    [SerializeField] private int initialLifeCount = 3;
    [SerializeField] private int initialEnemyCount = 4;
    [SerializeField] private float freezeDuration = 10f;
    [SerializeField] private float hitStopDuration = 1f;
    [SerializeField] private float attackDuration = 3.5f;
    [SerializeField] private float damagedDuration = 3.5f;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject rewards;
    [SerializeField] private GameObject bigRewards;

    //game state variables
    private int score;
    private int lifeCount;
    private bool endGame;
    private bool missionFail;

    private bool attackPanel;
    private bool damagedPanel;

    //Enemy variables
    private int enemyCount;
    private List<EnemyAI> enemyList;
    

    // Cached references for efficiency
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject player;
    private CharacterController playerController;
    [SerializeField] private GameUI gameUI;
    
    //Materials
    [SerializeField] public Material UnFrezMat;
    [SerializeField] public Material FrezMat;

    // Timers & Active Variables
    // HitTimer: starts when the player hits an enemy.
    // FreezingTimer: starts when the enemy is frozen.
    private bool isHitStopActive;
    private float hitStopTimer;
    private float freezeTimer;
    private bool isFreezingActive;
    private float attackTimer;
    private float damagedTimer;
    
    //observable - event function
    public static event Action<GameManager> GameEnd;
    public static event Action<GameManager> GameOver;
    public static event Action<GameManager> FreezingEventStart;
    public static event Action<GameManager> FreezingEventEnd;
    public static event Action<GameManager> AttackEventStart;
    public static event Action<GameManager> AttackEventEnd;
    public static event Action<GameManager, string> collectItems;
    public static event Action<GameManager> DamageEventStart;
    public static event Action<GameManager> DamageEventEnd;
    public static event Action<GameManager, int> GetScoreEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        lifeCount = initialLifeCount;
        endGame = false;
        missionFail = false;
        attackPanel = false;
        damagedPanel = false;
        enemyCount = initialEnemyCount;
        enemyList = new List<EnemyAI>();

        // Cache player controller
        if(player != null){
            playerController = player.GetComponent<CharacterController>();
        }
        else{
            player = GameObject.FindWithTag("Player");
            if (player == null){
                Debug.LogError("Player not found!");
            }
            playerController = player.GetComponent<CharacterController>();
        }

        //Populate enemy List
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if(ai != null){
                enemyList.Add(ai);
            }
            
        }

        // Initialize timers
        freezeTimer = 0f;
        isFreezingActive = false;
        isHitStopActive = false;
        hitStopDuration = 1f;
        hitStopTimer = 0f;
        attackDuration = 3.5f;
        attackTimer = 0f;
        damagedDuration = 3.5f;
        damagedTimer = 0f;

    }
    
    // Update is called once per frame
    void Update()
    {

        if (!endGame){
            // check for game end
            if(totalRewards <= 0 || lifeCount <= 0)   
            {
                endGame = true;
                if (playerController != null){
                    playerController.enabled = false;
                }
                Debug.Log("Game End");
                if(lifeCount <= 0)
                {
                    missionFail = true; 
                    GameOver?.Invoke(this); // Invoke event for fail
                    
                }
                else{
                    GameEnd?.Invoke(this); // Invoke event for win 
                }
            }

            // Handle hit Stop
            if(isFreezingActive)
            {
                freezeTimer -= Time.deltaTime;
                if(freezeTimer <= 0){
                    freezeTimer = 0f;
                    isFreezingActive = false;
                    //gameUI.HideFreezingMessage();
                    FreezingEventEnd?.Invoke(this); // Invoke event for freezingEnd
                }
                
            }

            // Handle hit stop 
            if(isHitStopActive)
            {
                hitStopTimer -= Time.deltaTime;
                if(hitStopTimer <=0){
                    hitStopTimer = 0f;
                    isHitStopActive = false;
                }
            }

            if (attackPanel)
            {
                attackTimer -= Time.deltaTime;
                if(attackTimer <= 0){
                    attackTimer = 0f;
                    AttackEventEnd?.Invoke(this);
                    attackPanel = false;
                }
                
            }

            if (damagedPanel)
            {
                damagedTimer -= Time.deltaTime;
                if(damagedTimer <= 0){
                    damagedTimer = 0f;
                    DamageEventEnd?.Invoke(this);
                    damagedPanel = false;
                }
                
            }
        }
        
    }

    // public getters
    public bool IsHitStopActive() => isHitStopActive;
    public bool IsGameEnd() => endGame;
    public bool IsFreezingActive () => isFreezingActive;
    public int GetSore() => score;
    public bool IsMissionFail() => missionFail;
    public int GetLifeCount() => lifeCount;


    public void HandleReward()
    {
        totalRewards--;
        score += 10;
        GetScoreEvent?.Invoke(this, score); // Invoke event for getting score
        Debug.Log("score: "+score);
    }

    public void HandleBigReward()
    {
        totalRewards--;
        score += 100;
        isFreezingActive = true;
        freezeTimer = freezeDuration;
        //gameUI.ShowFreezingMessage();
        FreezingEventStart?.Invoke(this); // Invoke event for getting score
        GetScoreEvent?.Invoke(this, score);
        Debug.Log("score: "+score);
    }

    public void HandleItemReward(string item_name)
    {
        totalRewards--;
        score += 150;
        GetScoreEvent?.Invoke(this, score);
        collectItems?.Invoke(this,item_name);
        Debug.Log("score: "+score);
    }

    public void OnPlayerDamaged()
    {
        lifeCount--;
        if (playerController != null && spawnPoint != null) {
            playerController.enabled = false;
            playerController.transform.position = spawnPoint.transform.position;
            playerController.enabled = true;
        }
        isHitStopActive = true;
        hitStopTimer = hitStopDuration;
        damagedTimer = damagedDuration;
        damagedPanel = true;
        damagedTimer = damagedDuration;
        DamageEventStart?.Invoke(this);
        Debug.Log("Enemies attack player, LifeTotal:"+lifeCount);
    }

    public void KillEnemy(GameObject enemy)
    {
        EnemyAI deleteEnemy = enemy.GetComponent<EnemyAI>();
        enemyList.Remove(deleteEnemy);
        score += 200;
        enemyCount --;
        attackTimer = attackDuration;
        attackPanel = true;
        attackTimer = attackDuration;
        GetScoreEvent?.Invoke(this, score);
        AttackEventStart?.Invoke(this);
        Debug.Log("Player attack Enemy, score:"+score);
    }
}
