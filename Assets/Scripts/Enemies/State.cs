using UnityEngine;

public class State : MonoBehaviour
{
    protected EnemyStateManager enemyStateManager { get; set; }

    protected void Awake()
    {
        enemyStateManager = GetComponent<EnemyStateManager>();
    }
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}
