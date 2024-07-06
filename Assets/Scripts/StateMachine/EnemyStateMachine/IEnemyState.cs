using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
   void EnterState(EnemyManager enemyManager); //CharacterManager characterManager
   void UpdateState(EnemyManager enemyManager);
   void ExitState(EnemyManager enemyManager);
}
