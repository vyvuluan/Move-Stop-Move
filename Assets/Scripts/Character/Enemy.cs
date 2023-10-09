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
        if (CompetitorInRange() != null)
        {
            //if (canAttack)
            ChangeState(new AttackState());
        }
        //else
        //{
        //    ChangeState(new MoveState());
        //}
        Debug.Log(currentState);
    }
    public override void Control()
    {
        rb.isKinematic = false;
        agent.isStopped = false;
        ChangeAnim(Constants.RunAnim);
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        randomPosition.y = 0.1f;
        agent.SetDestination(randomPosition);
    }
    public void StopAgent()
    {
        agent.isStopped = true;
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
