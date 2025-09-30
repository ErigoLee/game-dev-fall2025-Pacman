using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEnemyAIState 
{
    IEnemyAIState DoState(EnemyAI enemy, bool isDeath);
}
