using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    NavMeshAgent agent;

    public Vector3 lastKnownPlayerPosition;

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void MoveTowardsPlayer()
    {
        agent.SetDestination(playerMovement.transform.position);
    }

    public void BufferPlayerPosition()
    {
        lastKnownPlayerPosition = playerMovement.transform.position;
    }

    public void MoveTowardsLastKnownPlayerPosition()
    {
        agent.SetDestination(lastKnownPlayerPosition);
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, playerMovement.transform.position) <= 1.5f;
    }
}