using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    private int totalRewards;
    private int score;
    [SerializeField] private int lifeCount;
    private bool endGame;
    private bool missionFail;

    //Enemy variables
    private int enemyCount;
    private List<EnemyControl> enemyList;
    
    private bool isHitStopActive;
    private float hitStopDuration;
    private float hitStopTimer;

    //Freezing variables
    private float freezeDuration; 
    private float freezeTimer;
    private bool isFreezingActive;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject rewards;
    [SerializeField] private GameObject bigRewards;
    //UI
    [SerializeField] private GameUI gameUI;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalRewards = 206;
        score = 0;
        lifeCount = 3;
        endGame = false;
        enemyCount = 4;
        missionFail = false;
        enemyList = new List<EnemyControl>();
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemyList.Add(enemy.GetComponent<EnemyControl>());
        }

        freezeDuration = 10f;
        freezeTimer = 0f;
        isFreezingActive = false;
        isHitStopActive = false;
        hitStopDuration = 1f;
        hitStopTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (!endGame){
            if(totalRewards == 0 || lifeCount == 0)   
            {
                endGame = true;
                CharacterController cc = player.GetComponent<CharacterController>();
                cc.enabled = false;
                Debug.Log("Game End");
                if(lifeCount == 0)
                {
                    missionFail = true;
                    
                }
                foreach (EnemyControl enemy in enemyList)
                {
                    enemy.TemporyStopOn();
                }
            }

            if(isFreezingActive)
            {
                freezeTimer -= Time.deltaTime;
                if(freezeTimer <= 0){
                    freezeTimer = 0f;
                    isFreezingActive = false;
                    gameUI.HideFreezingMessage();
                    foreach (EnemyControl enemy in enemyList)
                    {
                        enemy.UnFreeze();
                    }
                }
                
            }

            if(isHitStopActive)
            {
                hitStopTimer -= Time.deltaTime;
                if(hitStopTimer <=0){
                    hitStopTimer = 0f;
                    isHitStopActive = false;
                    foreach(EnemyControl enemy in enemyList)
                    {
                        enemy.TemporyStopOff();
                    }
                }
            }
        }
        
    }
    public int GettingScore()
    {
        return score;
    }

    public bool GettingGameEnd()
    {
        return endGame;
    }

    public bool GettingMissionFail()
    {
        return missionFail;
    }

    public int GettingLifeCount()
    {
        return lifeCount;
    }

    public void HandleReward()
    {
        totalRewards--;
        score += 10;
        Debug.Log("score: "+score);
    }

    public void HandleBigReward()
    {
        totalRewards--;
        score += 100;
        isFreezingActive = true;
        freezeTimer = freezeDuration;
        gameUI.ShowFreezingMessage();
        foreach (EnemyControl enemy in enemyList)
        {
            enemy.Freeze();
        }
        Debug.Log("score: "+score);
    }

    public void OnPlayerDamaged()
    {
        lifeCount--;
        CharacterController cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        cc.transform.position = spawnPoint.transform.position;
        cc.enabled = true;
        isHitStopActive = true;
        hitStopTimer = hitStopDuration;
        foreach(EnemyControl enemy in enemyList)
        {
            enemy.TemporyStopOn();
        }
        gameUI.PopupCombatMessage(true);
        gameUI.LoseLife();
        Debug.Log("Enemies attack player, LifeTotal:"+lifeCount);
    }

    public void KillEnemy(GameObject enemy)
    {
        EnemyControl deleteEnemy = enemy.GetComponent<EnemyControl>();
        enemyList.Remove(deleteEnemy);
        Destroy(enemy);
        score += 200;
        enemyCount --;
        gameUI.PopupCombatMessage(false);
        Debug.Log("Player attack Enemy, score:"+score);
    }
}
