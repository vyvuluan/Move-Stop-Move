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
    public override void Moving(Vector3 direction)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        rb.AddForce(direction * speed, ForceMode.Impulse);
        transform.DOLocalRotate(new Vector3(90, 360, 0), 0.5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        Invoke(nameof(Back), 3f);

    }
    public void Back()
    {
        rb.isKinematic = true;
        Debug.Log(transformStart.position);
        transform.DOMove(playerTransform.position, 1.5f).OnComplete(() =>
        {
            transform.SetParent(parentRightHand, false);
            transform.SetLocalPositionAndRotation(transformStart.position, transformStart.rotation);
            transform.DOKill();
            onChangeStatusCanAttack?.Invoke(true);
        });


    }


}