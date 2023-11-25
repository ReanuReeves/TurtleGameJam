using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyStateManager : MonoBehaviour
{
    public Health playerHealth;

    public EnemyMovement enemyMovement;

    State currentState;

    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform eyePosition;

    int rayCount = 20;
    int visionAngle = 60;


    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = GetComponent<IdleState>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();
    }

    public void ChangeEnemyState(State newState)
    {
        print("Change state to " + newState);
        if(newState == currentState)
        {
            return;
        }

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        print(currentState);
    }

    public bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, playerHealth.transform.position) <= GetComponent<AttackState>().attackRange;
    }
    public bool PlayerInVision()
    {
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(((i - rayCount / 2) * visionAngle) / rayCount, Vector3.up) * transform.forward;

            if (Physics.Raycast(eyePosition.position, direction, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
