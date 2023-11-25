public class ChaseState : State
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        enemyStateManager.enemyMovement.MoveTowardsPlayer();

        if(!enemyStateManager.PlayerInVision())
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
