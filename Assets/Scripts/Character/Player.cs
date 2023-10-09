using UnityEngine;

public class Player : Character
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private RangeAttack rangeAttack;
    [SerializeField] private FloatingJoystick joystick;
    private Vector3 moveVector;
    private RaycastHit hit;
    private RaycastHit hitWall;
    public override void Start()
    {
        base.Start();
        //init radius range start
        rangeAttack.SetRadiusRangeAttack(rangeAttackRadius);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canAttack)
            {
                //Attack();
            }
        }
        else
        {
            //Control();
        }
    }
    public override void Control()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = joystick.Vertical * moveSpeed * Time.deltaTime;
        Debug.Log(moveVector);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, moveSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim(Constants.RunAnim);
        }
        else if (joystick.Horizontal == 0 || joystick.Vertical == 0)
        {
            ChangeAnim(Constants.IdleAnim);
        }
        rb.MovePosition(rb.position + moveVector);
    }
    public void SetJoystick(FloatingJoystick floatingJoystick)
    {
        this.joystick = floatingJoystick;
    }

}
