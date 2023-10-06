using UnityEngine;

public class Knife : Weapon
{
    private void OnEnable()
    {
        rb.isKinematic = true;
    }
    public override void Moving(Vector3 direction)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        rb.AddForce(direction * speed, ForceMode.Impulse);
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
