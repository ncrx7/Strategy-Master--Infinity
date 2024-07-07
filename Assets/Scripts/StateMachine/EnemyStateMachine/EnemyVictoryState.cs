using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVictoryState : IEnemyState
{
    public void EnterState(EnemyManager enemyManager)
    {
        enemyManager.characterAnimationManager.HandlePlayAnimation("Zombie_Victory");
    }

    public void ExitState(EnemyManager enemyManager)
    {
        
    }

    public void UpdateState(EnemyManager enemyManager)
    {
        //PLAY VICTORY ANIM
        //Debug.Log("ENEMIES, VICTORY!!!");
    }
}
