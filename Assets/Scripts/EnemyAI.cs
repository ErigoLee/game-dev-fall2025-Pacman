using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    //Enemy Unique ID
    private static int counter = 0;  // Counter shared by all Enemies 
    [SerializeField] private int myOrder; //  Unique ID for each Enemy
    [SerializeField] private float orderDelayMultiplier = 5f; // Configurable delay per enemy (e.g., 5 seconds between starts)
    
    // Cached reference for efficiency
    [SerializeField] private GameManager GameManager;
    private float orderDelay;
    public bool hasStarted;
    public NavMeshAgent navAgent;

    public IEnemyAIState currentState;

    public WaitState waitState = new WaitState();
    public FreezingState freezingState = new FreezingState();
    public DeathState deathState= new DeathState();
    public AttackState attackState= new AttackState();
    public HitState hitState= new HitState();
    public GameOverState gameOverState = new GameOverState();
    private bool isDeath = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Awake()
    {
        counter++;
        myOrder = counter;
        orderDelay = orderDelayMultiplier * (myOrder - 1); 
        hasStarted = false;
        //Debug.Log(gameObject.name+": "+myOrder); 
    }
    private void OnEnable()
    {
        currentState =  waitState;  
    }
    

    void Update()
    {
        if(orderDelay > 0){
            orderDelay -= Time.deltaTime;
        }
        else{
            hasStarted = true;
        }
        currentState = currentState.DoState(this, isDeath);
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            //Debug.Log("Collided with Player!");
            GameManager gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();
            bool isFreezing = gameManager.IsFreezingActive();
            if(isFreezing){
                gameManager.KillEnemy(this.gameObject);
                isDeath = true;
                
            }
            else{
                gameManager.OnPlayerDamaged();
                
            }
        }
    }

}
