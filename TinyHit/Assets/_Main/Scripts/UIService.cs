using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService
{
    private readonly TargetService _targetService;


    private readonly TMP_Text _stageNameText;
    private readonly Image _targetHealthFiller;
    private readonly TMP_Text _targetHealthText;
    private readonly TMP_Text _knifeCounterText;

    private Target _target;

    public UIService(TMP_Text stageNameText, Image targetHealthFiller, TMP_Text targetHealthText, TMP_Text knifeCounterText, TargetService targetService)
    {
        _stageNameText = stageNameText;
        _targetHealthFiller = targetHealthFiller;
        _targetHealthText = targetHealthText;
        _knifeCounterText = knifeCounterText;
        _targetService = targetService;
    }

    public void Initialize()
    {
        _targetService.OnTargetChanged += OnTargetChanged;
    }

    public void Deinitialize()
    {
        _targetService.OnTargetChanged -= OnTargetChanged;
    }

    public void OnTargetChanged(Target newTarget)
    {
        _target = newTarget;
    }

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