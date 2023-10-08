using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    private IState currentState;
    public override void Awake()
    {
        base.Awake();
        ChangeState(new IdleState());
    }
    private void Update()
    {
        currentState?.OnExecute(this);
    }
    public override void Control()
    {
        ChangeAnim(Constants.RunAnim);
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        randomPosition.y = 0.1f;
        agent.SetDestination(randomPosition);
    }
    public bool CheckChangeIdleState() => agent.remainingDistance <= agent.stoppingDistance;

    public void ResetIdle()
    {
        ChangeAnim(Constants.IdleAnim);
    }
    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }
}
