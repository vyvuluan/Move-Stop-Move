using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed = 10f;
    public abstract void Moving(Vector3 start, Vector3 end);

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            // Debug.Log(other.name);
        }
    }

}
