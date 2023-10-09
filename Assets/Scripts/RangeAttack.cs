using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] RectTransform imageRectTransform;
    public void SetRadiusRangeAttack(float overlapSphereRadius) => imageRectTransform.sizeDelta = new Vector2(overlapSphereRadius * 2, overlapSphereRadius * 2);

}
