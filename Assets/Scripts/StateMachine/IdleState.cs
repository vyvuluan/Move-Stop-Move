using UnityEngine;

public class IdleState : IState
{
    private float timer;
    private float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.ResetIdle();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }
    public void OnExecute(Enemy enemy)
    {
        Debug.Log("ilde");
        timer += Time.deltaTime;
        if (timer >= randomTime)
        {
            enemy.ChangeState(new MoveState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}

