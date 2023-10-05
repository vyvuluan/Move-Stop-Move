using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    private string currentAnimName;
    public abstract void Control();
    public void Attack()
    {
        ChangeAnim(Constants.AttackAnim);
        SimplePool.Spawn(weapon.gameObject, firePoint.transform.position, weapon.transform.rotation);
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }
}
