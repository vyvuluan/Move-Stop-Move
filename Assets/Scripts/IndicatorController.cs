using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Indicator indicator;
    public void OnInit(Transform playerTransform, Indicator indicator)
    {
        this.playerTransform = playerTransform;
        this.indicator = indicator;
    }
    private void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1 || screenPos.z < 0)
        {
            indicator.gameObject.SetActive(true);
            Vector3 enemyScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerTransform.position);

            //limit x, y fit screen
            Vector3 enemyScreenPointLimit = LimitMinMax(enemyScreenPoint);
            Vector3 playerScreenPointLimit = LimitMinMax(playerScreenPoint);

            Vector3 directionToPlayer = enemyScreenPointLimit - playerScreenPointLimit;

            indicator.transform.position = enemyScreenPointLimit;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            angle -= 90f;
            indicator.ArrowImage.rotation = Quaternion.Euler(0, 0, angle);
            indicator.SetDefaultRotationPanelLevel();
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }

    }
    public Vector3 LimitMinMax(Vector3 enemyScreenPoint)
    {
        float x = Mathf.Clamp(enemyScreenPoint.x, 50, Screen.width - 50f);
        float y = Mathf.Clamp(enemyScreenPoint.y, 50, Screen.height - 50f);
        y = transform.position.z < -14f ? 50f : y;
        return new Vector3(x, y, 0);
    }
}
