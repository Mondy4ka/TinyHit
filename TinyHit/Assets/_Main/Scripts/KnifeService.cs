using System.Collections.Generic;
using UnityEngine;

public class KnifeService : MonoBehaviour
{
    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private TargetService _targetService;
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _knifePoint;
    [SerializeField] private List<int> _knifeDamage;

    private KnifePool _knifePool;
    private Knife _currentKnife;

    private void Awake()
    {
        _knifePool = new(_knifePrefab);

        _knifePool.Initialize(_poolSize);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(3))
        {
            _currentKnife.Throw();
            _currentKnife = null;
            SpawnKnife();
        }
    }

    [ContextMenu("SpawnKnife")]
    private void SpawnKnife()
    {
        if (_currentKnife != null)
        {
            _knifePool.ReturnKnife(_currentKnife);
            _currentKnife = null;
        }

        _currentKnife = _knifePool.GetKnife();
        _currentKnife.MoveTo(_knifePoint.position);
        _currentKnife.Initialize(_targetService.CurrentTarget, _knifeDamage[Random.Range(0, _knifeDamage.Count)]);
    }
}
