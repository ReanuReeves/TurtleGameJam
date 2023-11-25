using System;
using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        // cast rays in an angle forward to detect player

        if (enemyStateManager.PlayerInVision()) enemyStateManager.ChangeEnemyState(GetComponent<ChaseState>()); ;
    }



    public override void Exit()
    {
        
    }
}
