using UnityEngine;

public class HitState : IEnemyAIState
{
    // Cache references for efficiency
    private GameManager gameManager;
    private Rigidbody enemyRigidbody;


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

    }
   
    public IEnemyAIState DoState(EnemyAI enemy, bool isDeath)
    {
        if (enemy.navAgent == null){
            enemy.navAgent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (enemy.navAgent == null) {
                Debug.LogError("NavMeshAgent component not found on enemy!");
                return this; // Fallback to another state
            }
        }
        // Initialize GM if not done
        InitializeRferences(enemy);
        if (gameManager == null || enemyRigidbody == null) {
            return this; // Can't proceed without GM, enemy rigidbody
        }
        EnemyHitPlayer(enemy);
        // State transitions
        bool isHitStopActive = gameManager.IsHitStopActive();
        if (isHitStopActive) {
            return this;
        }
        else{
            return enemy.attackState;
        }
    }

    private void EnemyHitPlayer(EnemyAI enemy)
    {
        // stop NavMeshAgent movement
        if (enemy.navAgent.isStopped == false){
            enemy.navAgent.isStopped = true;
            enemy.navAgent.ResetPath();
            enemy.navAgent.velocity = Vector3.zero;
            enemy.navAgent.speed = 0;
            // Stop Rigidbody movement
            if (enemyRigidbody != null){
                enemyRigidbody.linearVelocity = Vector3.zero;
                enemyRigidbody.angularVelocity = Vector3.zero;
            }
        }

        
    }
}
