public class MoveState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.Control();
    }

    public void OnExecute(Enemy enemy)
    {
        if (enemy.CheckChangeIdleState())
            enemy.ChangeState(new IdleState());
    }

    public void OnExit(Enemy enemy)
    {

    }
}
