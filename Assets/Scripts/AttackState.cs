using UnityEngine;

public class AttackState : IEnemyAIState
{

    // Cache references for efficiency
    private GameManager gameManager;
    private Rigidbody enemyRigidbody;
    private Renderer enemyRenderer;

    private GameObject playerObj;

    private void InitializeRferences(EnemyAI enemy){
        if(gameManager == null)
        {
            GameObject gmObj = GameObject.FindWithTag("GM");
            if(gmObj != null)
            {
                gameManager = gmObj.GetComponent<GameManager>();
            }
            else{
                Debug.LogError("No GameObject with tag 'GM' found!");
            }
        }

        if (enemyRigidbody == null && enemy.navAgent != null) {
            enemyRigidbody = enemy.navAgent.GetComponent<Rigidbody>();
        }
        
        if (enemyRenderer == null && enemy.navAgent != null) {
            enemyRenderer = enemy.navAgent.GetComponent<Renderer>();
        }
        
        if (playerObj == null) {
            playerObj = GameObject.FindWithTag("Player");
        }

    }
    public IEnemyAIState DoState(EnemyAI enemy, bool isDeath)
    {
        if (enemy.navAgent == null){
            enemy.navAgent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (enemy.navAgent == null){
                Debug.LogError("NavMeshAgent component not found on enemy!");
                return this; // Fallback to another state
            }
        }
        // Initialize cached references
        InitializeRferences(enemy);
        if (gameManager == null || enemyRigidbody == null || enemyRenderer == null || playerObj == null){
            return this; // Can't proceed without GM, enemy rigidbody, enemy Material, player position
        }

        Attacking(enemy);
        bool isFreezing = gameManager.IsFreezingActive();
        bool endGame = gameManager.IsGameEnd();
        bool isHitStopActive = gameManager.IsHitStopActive();
        // State transitions
        if (endGame)
            return enemy.gameOverState;
        else if (isFreezing)
            return enemy.freezingState;
        else if (isHitStopActive)
            return enemy.hitState;
        else
            return enemy.attackState; //stay attacking
    }

    private void Attacking(EnemyAI enemy)
    {
        // Set NavMeshAgent movement
        if (enemy.navAgent.speed <= 0f){
            enemy.navAgent.isStopped = false;
            enemy.navAgent.speed = 3.5f;
        }
        
        Vector3 playerPos = playerObj.transform.position;
        enemy.navAgent.SetDestination(playerPos);
        
        // Change material
        if (enemyRenderer != null && gameManager.UnFrezMat != null){
            enemyRenderer.material = gameManager.UnFrezMat;
        }
    }

}
