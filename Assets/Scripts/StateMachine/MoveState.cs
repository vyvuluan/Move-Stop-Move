using UnityEngine;

public class MoveState : IState
{
    public void OnEnter(Enemy enemy)
    {
        Debug.Log("move entenr");
        enemy.Control();
    }

    public void OnExecute(Enemy enemy)
    {
        Debug.Log("move");
        if (enemy.CheckChangeIdleState())
            enemy.ChangeState(new IdleState());
    }

    public void OnExit(Enemy enemy)
    {

    }
}
