using UnityEngine.AI;

public class ChaseState : State
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        // If the player is in vision, move towards them
        if (enemyStateManager.PlayerInVision())
        {
            enemyStateManager.enemyMovement.MoveTowardsPlayer();
            enemyStateManager.enemyMovement.BufferPlayerPosition();
        }

        if(!enemyStateManager.PlayerInVision() && GetComponent<NavMeshAgent>().remainingDistance > 0.1f)
        {
            enemyStateManager.enemyMovement.MoveTowardsLastKnownPlayerPosition();
        }
        else if(!enemyStateManager.PlayerInVision())
        {
            enemyStateManager.ChangeEnemyState(GetComponent<IdleState>());
        }

        if (enemyStateManager.PlayerInAttackRange())
        {
            enemyStateManager.ChangeEnemyState(GetComponent<AttackState>());
        }
    }

    public override void Exit()
    {
        enemyStateManager.enemyMovement.StopMoving();
    }
}
