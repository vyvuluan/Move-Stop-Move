using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Weapon weaponCurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    private string currentAnimName;
    protected bool canAttack = true;
    private void Awake()
    {
        OnInit();
    }
    public abstract void Control();
    public void OnInit()
    {
        Weapon weapon = SimplePool.Spawn(weaponPrefab.gameObject, weaponPrefab.transform.position, weaponPrefab.transform.rotation).GetComponent<Weapon>();
        weapon.transform.SetParent(firePoint.transform, false);
        weaponCurrent = weapon;
    }
    public void Attack()
    {
        ChangeAnim(Constants.AttackAnim);
        Weapon weapon = weaponCurrent.GetComponent<Weapon>();
        weapon.Moving(transform.forward);
        canAttack = false;
        Invoke(nameof(ResetAttack), 1.02f);
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
    public void ResetAttack()
    {
        Weapon weapon = SimplePool.Spawn(weaponPrefab.gameObject, weaponPrefab.transform.position, weaponPrefab.transform.rotation).GetComponent<Weapon>();
        weapon.transform.SetParent(firePoint.transform, false);
        weaponCurrent = weapon;
        currentAnimName = Constants.IdleAnim;
        canAttack = true;
    }
}
