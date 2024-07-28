using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Slider slider;

    public Button Button => button;

    public Slider Slider => slider;
}
