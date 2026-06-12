using PrimeTween;
using System;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public event Action OnGameOver;

    public float Damage => _damage;
    public bool IsStatic => _isStatic;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _throwTime;

    private float _damage;
    private Target _target;
    private Vector2 _positionInTarget;
    private Tween _currentTween;

    private bool _isStatic;

    public void Initialize(Target target, float damage)
    {
        _target = target;
        _damage = damage;

        CalculatePositionInTarget();
    }

    public void SetStatic(bool isStatic) => _isStatic = isStatic;

    public void CalculatePositionInTarget() => _positionInTarget.y = _target.transform.position.y - _target.KnifeDepth;

    public void Throw() => _currentTween = Tween.Position(transform, _positionInTarget, _throwTime, Ease.Linear);

    public void StopAnimation()
    {
        if (_currentTween.isAlive)
            _currentTween.Stop();
    }

    public void PlaceInTarget()
    {
        transform.SetParent(_target.transform);
        MoveTo(_positionInTarget);
        SetStatic(true);
    }

    public void MoveTo(Vector3 newPosition) => transform.position = newPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStatic) return;


        if (collision.CompareTag("Knife"))
        {
            if (collision.GetComponent<Knife>().IsStatic == false) return;

            StopAnimation();
            OnGameOver?.Invoke();
        }

        if (collision.CompareTag("Target"))
        {
            _target.OnKnifeHit(this);
            StopAnimation();
            PlaceInTarget();
        }
    }
}
