using UnityEngine;

public class Knife : Weapon
{
    private void OnEnable()
    {
        rb.isKinematic = true;
    }
    public override void Moving(Vector3 start, Vector3 end)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        Vector3 lookDirection = end - start;
        //Vector3 directionKnife = transform.rotation * new(1, 1, -1);
        transform.rotation = Quaternion.Euler(-90, 0, Mathf.Atan2(lookDirection.z, lookDirection.x) * Mathf.Rad2Deg);




        rb.AddForce(lookDirection.normalized * speed, ForceMode.Impulse);
        //transform.DOLocalRotate(new Vector3(90, 360, 0), 1, RotateMode.FastBeyond360)
        //    .SetLoops(-1, LoopType.Restart)
        //    .SetEase(Ease.Linear);
        Invoke(nameof(OnDespawn), 3f);

    }
    public void OnDespawn()
    {
        SimplePool.Despawn(gameObject);

    }


}
