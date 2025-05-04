using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Text sensitivityValueText;
    public PlayerController playerController;
    private void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 2f);
        UpdateSensitivityValueText(sensitivitySlider.value);
        
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivityValue);
    }
    private void UpdateSensitivityValue(float value)
    {
        UpdateSensitivityValueText(value);
        PlayerPrefs.SetFloat("Sensitivity", value);
        playerController.SetMouseSensitivity(value);
    }
    private void UpdateSensitivityValueText(float value)
    {
        sensitivityValueText.text = value.ToString("F2");
    }
}