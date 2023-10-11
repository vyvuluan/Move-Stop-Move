using DG.Tweening;
using System;
using UnityEngine;

public class Boomerang : Weapon
{
    [SerializeField] private Transform parentRightHand;
    private Action<bool> onChangeStatusCanAttack;
    private Transform transformStart;
    private Transform playerTransform;
    public void OnInit(Transform parentRightHand, Transform transformStart, Transform playerTransform, Action<bool> onChangeStatusCanAttack)
    {
        this.parentRightHand = parentRightHand;
        this.transformStart = transformStart;
        this.playerTransform = playerTransform;
        this.onChangeStatusCanAttack = onChangeStatusCanAttack;
    }
    public override void Moving(Vector3 start, Vector3 end)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        Vector3 lookDirection = end - start;
        transform.SetPositionAndRotation(new(start.x, transform.position.y, start.z), Quaternion.Euler(90, 0, 0));
        rb.AddForce(lookDirection.normalized * speed, ForceMode.Impulse);
        transform.DOLocalRotate(new Vector3(90, 360, 0), 0.5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        Invoke(nameof(Back), 3f);

    }
    public void Back()
    {
        rb.isKinematic = true;
        transform.DOMove(playerTransform.position, 0.5f).OnComplete(() =>
        {
            transform.SetParent(parentRightHand, false);
            transform.SetLocalPositionAndRotation(transformStart.position, transformStart.rotation);
            transform.DOKill();
            onChangeStatusCanAttack?.Invoke(true);
        });


    }


}
