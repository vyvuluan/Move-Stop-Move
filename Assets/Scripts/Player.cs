using UnityEngine;

public class Player : Character
{
    public override void Control()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
        Debug.Log(levelSystem.Level);

    }
}
