using UnityEngine;

public class GameOverState : IEnemyAIState
{
    private GameManager gameManager;
    private void InitializeGM()
    {
        if (gameManager == null)
        {
            GameObject gmObj = GameObject.FindWithTag("GM");
            if(gmObj != null)
            {
                gameManager = gmObj.GetComponent<GameManager>();
            }else{
                Debug.LogError("No GameObject with tag 'GM' found!");
            }
        }
    }
    public  IEnemyAIState DoState(EnemyAI enemy, bool isDeath)
    {
       if (enemy.navAgent == null){
            enemy.navAgent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (enemy.navAgent == null){
                Debug.LogError("NavMeshAgent component not found on enemy!");
                return this; // Stay in current state or handle error
            }
        }

         // Initialize GM if not done
        InitializeGM();
        if (gameManager == null) {
            return this; // Can't proceed without GM
        }

        bool endGame = gameManager.IsGameEnd();
        
        if (endGame){
            GameOver(enemy);
            return enemy.gameOverState;
        }
        else{
            return enemy.currentState;
        }
    }

    private void GameOver(EnemyAI enemy)
    {
        enemy.navAgent.isStopped = false;
        enemy.navAgent.ResetPath();
        enemy.navAgent.velocity = Vector3.zero;
        enemy.navAgent.speed = 0;
        Rigidbody rb = enemy.navAgent.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero;
    }
}
