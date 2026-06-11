using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [Header("Stage UI Settings")]
    [SerializeField] private TMP_Text _stageNameText;

    [Header("Target Health UI Settings")]
    [SerializeField] private Image _targetHealthFiller;
    [SerializeField] private TMP_Text _targetHealthText;

    [Header("Knife UI Settings")]
    [SerializeField] private TMP_Text _knifeCounterText;

    public void UpdateTargetHealthUI(float currentHealth, float maxHealth)
    {
        if (_targetHealthFiller == null || _targetHealthText == null) return;

        _targetHealthText.SetText($"{currentHealth} / {maxHealth}");
        _targetHealthFiller.fillAmount = currentHealth / maxHealth;
    }

    public void SetStageName(int stage)
    {
        if (_stageNameText == null) return;

        _stageNameText.SetText($"STAGE {stage}");
    }

    public void SetStageName(string stageName)
    {
        if (_stageNameText == null) return;

        _stageNameText.SetText(stageName);
    }
}