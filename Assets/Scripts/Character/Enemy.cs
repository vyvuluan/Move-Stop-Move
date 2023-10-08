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
        //Debug.Log(currentState.ToString());
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        //Debug.Log(currentAnimName);
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
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
