using System.Collections.Generic;
using UnityEngine;

public class KnifeService
{
    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private TargetService _targetService;
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _knifePoint;
    [SerializeField] private List<int> _knifeDamage;

    private ObjectPool<Knife> _knifePool;
    private Knife _currentKnife;
    private Target _currentTarget;
    private bool _isThrowActive;

    public void Initialize()
    {
        _knifePool = new(_knifePrefab);

        _knifePool.Initialize(_poolSize);
    }

    public void OnTargetChanged(Target newTarget)
    {
        _currentTarget = newTarget;
        _currentKnife.Initialize(_currentTarget, _knifeDamage[Random.Range(0, _knifeDamage.Count)]);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(3) && _isThrowActive)
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
            _knifePool.ReturnItem(_currentKnife);
            _currentKnife = null;
        }

        _currentKnife = _knifePool.GetItem();
        _currentKnife.MoveTo(_knifePoint.position);
        _currentKnife.Initialize(_currentTarget, _knifeDamage[Random.Range(0, _knifeDamage.Count)]);
    }
}
