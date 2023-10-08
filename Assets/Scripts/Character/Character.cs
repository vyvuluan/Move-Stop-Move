using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Weapon weaponCurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject nameInfoPrefab;
    [SerializeField] private Transform parentNameInfo;
    protected LevelSystem levelSystem;
    protected string currentAnimName;
    protected bool canAttack = true;
    private Coroutine attackCoroutine;
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    public virtual void Awake()
    {
        Debug.Log(transform.name);
        OnInit();
        levelSystem = new LevelSystem();
    }
    private void Start()
    {
        NameInfo nameInfo = SimplePool.Spawn(nameInfoPrefab, nameInfoPrefab.transform.position, nameInfoPrefab.transform.rotation).GetComponent<NameInfo>();
        nameInfo.OnInit(transform, parentNameInfo);
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
        //ChangeAnim(Constants.AttackAnim);
        //Weapon weapon = weaponCurrent;
        //weapon.Moving(transform.forward);
        //canAttack = false;
        //Invoke(nameof(ResetAttack), 1.02f);
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    public void SetParentNameInfo(Transform tf) => this.parentNameInfo = tf;
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
    public IEnumerator AttackCoroutine()
    {
        ChangeAnim(Constants.AttackAnim);
        Weapon weapon = weaponCurrent;
        yield return new WaitForSeconds(0.3f);
        weapon.Moving(transform.forward);
        canAttack = false;


        yield return new WaitForSeconds(1.02f);
        //Reset attack
        currentAnimName = Constants.IdleAnim;
        if (weaponCurrent is Boomerang)
        {
            yield return null;
        }
        else
        {
            weapon = SimplePool.Spawn(weaponPrefab.gameObject, weaponPrefab.transform.position, weaponPrefab.transform.rotation).GetComponent<Weapon>();
            weapon.transform.SetParent(firePoint.transform, false);
            weaponCurrent = weapon;
            canAttack = true;
        }
        StopCoroutine(attackCoroutine);
    }
    public void AddLvl()
    {
        levelSystem.AddExperience(50);
    }
}
