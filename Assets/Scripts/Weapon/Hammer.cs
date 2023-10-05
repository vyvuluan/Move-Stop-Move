using DG.Tweening;
using UnityEngine;
public class Hammer : Weapon
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    public override void Moving(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
        transform.DORotate(new Vector3(90, 360, 0), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    // Start is called before the first frame update
    void Start()
    {
        Moving(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
