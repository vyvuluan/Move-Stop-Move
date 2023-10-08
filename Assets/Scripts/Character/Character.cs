using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Weapon weaponCurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    protected LevelSystem levelSystem;
    protected string currentAnimName;
    protected bool canAttack = true;
    public virtual void Awake()
    {
        OnInit();
        levelSystem = new LevelSystem();
    }
    public abstract void Control();
    public void OnInit()
    {
        Weapon weapon = SimplePool.Spawn(weaponPrefab.gameObject, weaponPrefab.transform.position, weaponPrefab.transform.rotation).GetComponent<Weapon>();
        if (weapon is Boomerang)
        {
            weapon.GetComponent<Boomerang>().OnInit(firePoint, weaponPrefab.transform, transform, ChangeStatusCanAttack);
        }
        weapon.transform.SetParent(firePoint.transform, false);
        weaponCurrent = weapon;
    }
    public void Attack()
    {
        ChangeAnim(Constants.AttackAnim);
        Weapon weapon = weaponCurrent;
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
    public void ChangeStatusCanAttack(bool canAttack) => this.canAttack = canAttack;
    public void ResetAttack()
    {
        if (weaponCurrent is Boomerang)
        {
            return;
        }
        Weapon weapon = SimplePool.Spawn(weaponPrefab.gameObject, weaponPrefab.transform.position, weaponPrefab.transform.rotation).GetComponent<Weapon>();
        weapon.transform.SetParent(firePoint.transform, false);
        weaponCurrent = weapon;
        canAttack = true;
        currentAnimName = Constants.IdleAnim;
    }
    public void AddLvl()
    {
        levelSystem.AddExperience(50);
    }
}
