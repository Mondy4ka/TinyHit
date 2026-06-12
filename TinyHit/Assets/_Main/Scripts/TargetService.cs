using PrimeTween;
using System;
using UnityEngine;

public class TargetService
{
    public event Action<Target> OnTargetChanged;

    [SerializeField] private Target _targetPrefab;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _targetSpawnPoint;
    [SerializeField] private Transform _targetDeathPoint;
    [SerializeField] private Ease _animationType;
    [SerializeField] private float _durationAnimation;

    private ObjectPool<Target> _targetPool;

    private Target _currentTarget;
    private Tween _currentTween;

    public void Initialize()
    {
        _targetPool = new(_targetPrefab);

    }

    public void StopAnimation()
    {
        if (_currentTween.isAlive)
            _currentTween.Stop();
    }

    private void SpawnTarget()
    {
        if (_currentTarget != null) return;

        StopAnimation();

        Target newTarget = 


        _currentTarget = UnityEngine.Object.Instantiate(_targetPrefab, _targetSpawnPoint.position, Quaternion.identity);
        _currentTarget.Initialize();
        _currentTarget.TargetHealth.OnDeath += DestroyTarget;
        _currentTween = Tween.Position(_currentTarget.transform, _targetPoint.position, _durationAnimation, _animationType)
            .OnComplete(() => _currentTarget.TargetRotate.SetActive(true));
    }

    private void DestroyTarget()
    {
        if (_currentTarget == null)
        {
            SpawnTarget();
            return;
        }

        if (_currentTween.isAlive)
            _currentTween.Stop();

        _currentTween = Tween.Position(_currentTarget.transform, _targetDeathPoint.position, _durationAnimation, _animationType)
            .OnComplete(() =>
            {
                if (_currentTarget != null)
                {
                    _currentTarget.TargetHealth.OnDeath -= DestroyTarget;
                    UnityEngine.Object.Destroy(_currentTarget.gameObject);
                    _currentTarget = null;
                }

                SpawnTarget();
            });
    }
}