using PrimeTween;
using UnityEngine;

public class TargetService : MonoBehaviour
{
    public Target CurrentTarget => _currentTarget;

    [SerializeField] private Target _targetPrefab;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _targetSpawnPoint;
    [SerializeField] private Transform _targetDeathPoint;
    [SerializeField] private Ease _animationType;
    [SerializeField] private float _durationAnimation;

    private Target _currentTarget;
    private Tween _currentTween;

    public void Start()
    {
        SpawnTarget();
    }

    private void SpawnTarget()
    {
        if (_currentTarget != null)
        {
            DestroyTarget();
            return;
        }

        if (_currentTween.isAlive)
            _currentTween.Stop();

        _currentTarget = Instantiate(_targetPrefab, _targetSpawnPoint.position, Quaternion.identity);
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
                    Destroy(_currentTarget.gameObject);
                    _currentTarget = null;
                }

                SpawnTarget();
            });
    }
}