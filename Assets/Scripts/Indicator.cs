using TMPro;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private RectTransform arrowImage;
    [SerializeField] private RectTransform panelLevel;
    [SerializeField] private TextMeshProUGUI textLevel;

    public RectTransform ArrowImage { get => arrowImage; }
    public TextMeshProUGUI TextMeshProUGUI { get => textLevel; }
    public RectTransform PanelLevel { get => panelLevel; }
    public void SetDefaultRotationPanelLevel() => panelLevel.rotation = Quaternion.identity;
}
