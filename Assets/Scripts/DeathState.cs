using UnityEngine;

public class DeathState : IEnemyAIState
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


    public IEnemyAIState DoState(EnemyAI enemy, bool isDeath)
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

        bool isFreezing = gameManager.IsFreezingActive();
        if (isDeath)
        {
            Killed(enemy); // Handle destruction
            return enemy.deathState;
        } 
        else{
            // Safety net: entered DeathState erroneously. Treat this branch as non-death
            if (isFreezing == true)
                return enemy.freezingState; 
            else
                return enemy.attackState; 
        }
        
    }

    private void Killed(EnemyAI enemy)
    {
        if (enemy.navAgent != null && enemy.navAgent.gameObject != null){
            UnityEngine.Object.Destroy(enemy.navAgent.gameObject);
        }
        else{
            Debug.LogWarning("Cannot kill enemy: NavMeshAgent or GameObject is null.");
        }
        
    }
}
