using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    PlayerMovement playerMovement;
    NavMeshAgent agent;

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

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
}