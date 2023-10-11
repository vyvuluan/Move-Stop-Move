using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private Weapon weaponCurrent;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject nameInfoPrefab;
    [SerializeField] private Transform parentNameInfo;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask layerMaskCharacter;
    private Coroutine attackCoroutine;
    protected LevelSystem levelSystem;
    protected string currentAnimName;
    protected bool canAttack = true;
    protected float rangeAttackRadius = 5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeAttackRadius);
    }
    public virtual void Awake()
    {
        OnInit();
        levelSystem = new LevelSystem();
    }
    public virtual void Start()
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
    public void Attack(Transform competitor)
    {
        if (!canAttack) return;
        attackCoroutine = StartCoroutine(AttackCoroutine(competitor));
    }
    public IEnumerator AttackCoroutine(Transform competitor)
    {
        canAttack = false;
        ChangeAnim(Constants.AttackAnim);
        Weapon weapon = weaponCurrent;
        yield return new WaitForSeconds(0.3f);
        Vector3 lookDirection = competitor.position - transform.position;
        //Debug.Log("competitor " + competitor.position);
        //Debug.Log("transform " + transform.position);
        transform.LookAt(lookDirection);
        weapon.Moving(transform.position, competitor.position);
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
            yield return new WaitForSeconds(4f);
            canAttack = true;
        }
        //StopCoroutine(attackCoroutine);
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

    public void AddLvl()
    {
        levelSystem.AddExperience(50);
    }
    public Transform CompetitorInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeAttackRadius, layerMaskCharacter);
        if (colliders.Length < 2) return null;
        colliders = colliders.OrderBy(n => Vector3.Distance(transform.position, n.transform.position)).ToArray();
        return colliders[1].transform;
    }

}
