using UnityEngine;

public class NameInfo : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private Transform target;
    [SerializeField] private Transform parent;
    private void Start()
    {
        transform.SetParent(parent);
    }
    private void Update()
    {
        transform.position = target.position + offset;
    }
    public void OnInit(Transform target, Transform parent)
    {
        this.target = target;
        this.parent = parent;
    }
}
