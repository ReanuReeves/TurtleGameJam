using System;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] float rotateSpeed = 360f;
    [SerializeField] float hearingRange = 10f;
    public override void Enter()
    {

    }

    public override void Execute()
    {
        // cast rays in an angle forward to detect player

        if (enemyStateManager.PlayerInVision()) enemyStateManager.ChangeEnemyState(GetComponent<ChaseState>());
        if(PlayerInHearingRange()) RotateTowardsPlayer();
    }

    private bool PlayerInHearingRange()
    {
        return Vector3.Distance(transform.position, enemyStateManager.playerHealth.transform.position) <= hearingRange;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 dirToPlayer = enemyStateManager.playerHealth.transform.position - transform.position;
        dirToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public override void Exit()
    {
        
    }
}
