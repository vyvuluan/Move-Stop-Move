using UnityEngine;

public class Player : Character
{
    public override void Control()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

    }
}
