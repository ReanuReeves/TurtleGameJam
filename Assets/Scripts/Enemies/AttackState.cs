
using UnityEngine;

public class AttackState : State
{
    [SerializeField] float attackCooldown = 1f;
    float attackTimer = 0f;
    public override void Enter()
    {
        enemyStateManager.enemyMovement.StopMoving();
    }

    public override void Execute()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            Attack();
            attackTimer = 0f;
        }

        if (!enemyStateManager.PlayerInAttackRange())
        {
            attackTimer = 0f;
            enemyStateManager.ChangeEnemyState(GetComponent<ChaseState>());
        }
    }

    public override void Exit()
    {
        
    }

    void Attack()
    {
        enemyStateManager.playerHealth.TakeDamage(1);
    }
}   