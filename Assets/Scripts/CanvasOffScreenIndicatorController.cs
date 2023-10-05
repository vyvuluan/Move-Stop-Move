using UnityEngine;
using UnityEngine.UI;

public class CanvasOffScreenIndicatorController : MonoBehaviour
{
    public Transform target; // Mục tiêu cần chỉ hướng
    public Camera playerCamera; // Camera của người chơi
    public Image indicatorImage; // Tham chiếu đến Image trong Canvas
    private Transform player;
    [SerializeField] private Transform parent;
    private void Start()
    {
        transform.SetParent(parent);
    }
    private void Update()
    {
        // Kiểm tra xem mục tiêu có nằm trong tầm nhìn của camera hay không
        Vector3 screenPos = playerCamera.WorldToViewportPoint(target.position);

        if (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1 || screenPos.z < 0)
        {
            // Mục tiêu nằm bên ngoài tầm nhìn, hiển thị mũi tên chỉ hướng
            //ShowIndicator(screenPos);
            // Mục tiêu nằm bên ngoài tầm nhìn, hiển thị mũi tên chỉ hướng

            // Lấy kích thước của Canvas
            RectTransform canvasRect = indicatorImage.canvas.GetComponent<RectTransform>();
            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;

            // Lấy kích thước của mũi tên chỉ hướng
            RectTransform arrowRect = indicatorImage.rectTransform;
            float arrowWidth = arrowRect.rect.width;
            float arrowHeight = arrowRect.rect.height;

            // Tính toán vị trí của mũi tên chỉ hướng trên Canvas
            Vector3 playerPosition = playerCamera.WorldToScreenPoint(player.position);
            Vector3 targetDirection = target.position - player.position;
            Vector3 normalizedDirection = targetDirection.normalized;

            float xPosition = 0f;
            float yPosition = 0f;

            if (screenPos.x < 0) // Mục tiêu ở bên trái
            {
                xPosition = 0;
            }
            else if (screenPos.x > 1) // Mục tiêu ở bên phải
            {
                xPosition = canvasWidth - arrowWidth;
            }
            else if (screenPos.y < 0) // Mục tiêu ở phía dưới
            {
                yPosition = 0;
            }
            else if (screenPos.y > 1) // Mục tiêu ở phía trên
            {
                yPosition = canvasHeight - arrowHeight;
            }

            // Tính toán vị trí cắt giữa camera và đường thẳng từ player đến mục tiêu
            Vector3 intersectionPoint = playerPosition + normalizedDirection * 100f; // Điểm cắt gần xa ngoài màn hình
            Vector3 screenIntersectionPoint = new Vector3(
                Mathf.Clamp01(playerCamera.WorldToViewportPoint(intersectionPoint).x) * canvasWidth,
                Mathf.Clamp01(playerCamera.WorldToViewportPoint(intersectionPoint).y) * canvasHeight,
                0
            );

            // Đặt vị trí của mũi tên chỉ hướng
            arrowRect.anchoredPosition = screenIntersectionPoint;
            indicatorImage.enabled = true;
            Debug.Log("out");
        }
        else
        {
            // Mục tiêu trong tầm nhìn của camera, ẩn mũi tên chỉ hướng
            HideIndicator();
            Debug.Log("in");
        }
    }
    public void OnInit(Transform target, Camera playerCamera, Transform parent, Transform player)
    {
        this.target = target;
        this.playerCamera = playerCamera;
        this.parent = parent;
        this.player = player;
    }
    private void ShowIndicator(Vector3 screenPos)
    {
        RectTransform canvasRect = indicatorImage.canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // Lấy kích thước của mũi tên chỉ hướng
        RectTransform arrowRect = indicatorImage.rectTransform;
        float arrowWidth = arrowRect.rect.width;
        float arrowHeight = arrowRect.rect.height;

        // Tính toán vị trí của mũi tên chỉ hướng ở rìa màn hình
        float xPosition = 0f;
        float yPosition = 0f;

        // Xác định hướng tương ứng
        if (screenPos.x < 0) // Mục tiêu ở bên trái
        {
            xPosition = -canvasWidth / 2 + arrowWidth;
        }
        else if (screenPos.x > 1) // Mục tiêu ở bên phải
        {
            xPosition = canvasWidth / 2 - arrowWidth;
        }
        else if (screenPos.y < 0) // Mục tiêu ở phía dưới
        {
            yPosition = -canvasHeight / 2 + arrowHeight;
        }
        else if (screenPos.y > 1) // Mục tiêu ở phía trên
        {
            yPosition = canvasHeight / 2 - arrowHeight;
        }

        // Đặt vị trí của mũi tên chỉ hướng
        Debug.Log(xPosition + " x " + yPosition);
        arrowRect.anchoredPosition = new Vector2(xPosition, yPosition);
        indicatorImage.enabled = true;
    }

    private void HideIndicator()
    {
        // Ẩn mũi tên chỉ hướng khi mục tiêu nằm trong tầm nhìn
        indicatorImage.enabled = false;
    }
}
