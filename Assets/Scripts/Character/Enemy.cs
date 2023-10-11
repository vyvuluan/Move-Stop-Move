using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject targetGameObject;
    private IState currentState;
    public override void Awake()
    {
        base.Awake();
        ChangeState(new IdleState());
    }
    private void Update()
    {
        transform.position = new(transform.position.x, 0, transform.position.z);
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
        randomPosition.y = 0f;
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
    public void TargetSetVisible(bool status)
    {
        targetGameObject.SetActive(status);
    }
    public void ChangeState(IState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    //Vector3 direction = (target.position - transform.position).normalized;
    //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
}
