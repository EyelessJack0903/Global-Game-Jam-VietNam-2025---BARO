using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider; // Tham chiếu tới Slider
    public TMP_Text percentageText; // Tham chiếu tới Text hiển thị giá trị
    public string nameSlider; // Tham chiếu tới Text hiển thị giá trị

    void Update()
    {
        float percentage = (slider.value / slider.maxValue) * 100f; // Tính phần trăm
        percentageText.text = $"Mức độ {nameSlider}: {percentage:F0}%";
    }
}
