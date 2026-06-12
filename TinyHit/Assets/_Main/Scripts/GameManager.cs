using PrimeTween;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Settings")]
    [Header("Stage UI Settings")]
    [SerializeField] private TMP_Text _stageNameText;

    [Header("Target Health UI Settings")]
    [SerializeField] private Image _targetHealthFiller;
    [SerializeField] private TMP_Text _targetHealthText;

    [Header("Knife UI Settings")]
    [SerializeField] private TMP_Text _knifeCounterText;

    [SerializeField] private Target _targetPrefab;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _targetSpawnPoint;
    [SerializeField] private Transform _targetDeathPoint;
    [SerializeField] private Ease _animationType;
    [SerializeField] private float _durationAnimation;

    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private TargetService _targetService;
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _knifePoint;
    [SerializeField] private List<int> _knifeDamage;

    private UIService _uiService;

    private void Awake()
    {
        _uiService = new(_stageNameText, _targetHealthFiller, _targetHealthText, _knifeCounterText);
    }
}
