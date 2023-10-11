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
        transform.position = new(start.x, transform.position.y, start.z);
        rb.AddForce(lookDirection.normalized * speed, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(-90, Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg, 0);
        Invoke(nameof(OnDespawn), 3f);
    }
    public void OnDespawn()
    {
        SimplePool.Despawn(gameObject);

    }


}
