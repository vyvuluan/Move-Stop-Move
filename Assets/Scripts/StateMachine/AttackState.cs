public class AttackState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.StopAgent();
    }

    public void OnExecute(Enemy enemy)
    {
        if (enemy.CompetitorInRange() == null)
        {
            enemy.ChangeState(new MoveState());
        }
        else
        {
            enemy.Attack(enemy.CompetitorInRange().transform);
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
