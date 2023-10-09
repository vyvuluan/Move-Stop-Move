using DG.Tweening;
using UnityEngine;
public class Hammer : Weapon
{
    private void OnEnable()
    {
        rb.isKinematic = true;
    }
    public override void Moving(Vector3 start, Vector3 end)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        Vector3 lookDirection = end - start;
        rb.AddForce(lookDirection.normalized * speed, ForceMode.Impulse);
        transform.DOLocalRotate(new Vector3(90, 360, 0), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        Invoke(nameof(OnDespawn), 3f);

    }
    public void OnDespawn()
    {
        transform.DOKill();
        SimplePool.Despawn(gameObject);

    }
}
