using UnityEngine;

public class TargetRotate
{
    private readonly Transform _targetTransform;
    private readonly float _maxRotateSpeed;
    private readonly float _accelerationTime;
    private readonly float _decelerationTime;
    private readonly float _rotateTime;
    private readonly float _idleTime;

    private RotateState _state;
    private float _currentRotateSpeed;
    private float _currentTime;
    private bool _isActive;

    public TargetRotate(Transform targetTransform, float maxRotateSpeed, float accelerationTime, float decelerationTime, float rotateTime, float idleTime)
    {
        _targetTransform = targetTransform;
        _maxRotateSpeed = maxRotateSpeed;
        _accelerationTime = accelerationTime;
        _decelerationTime = decelerationTime;
        _rotateTime = rotateTime;
        _idleTime = idleTime;
    }

    public void Update()
    {
        if (_isActive == false) return;

        OnStateHandler();
    }

    public void SetActive(bool isActive) => _isActive = isActive;

    private void OnStateHandler()
    {
        switch (_state)
        {
            case RotateState.Idle: IdleStateUpdate(); break;
            case RotateState.Acceleration: AccelerationStateUpdate(); break;
            case RotateState.Deceleration: DecelerationStateUpdate(); break;
            case RotateState.Rotate: RotateStateUpdate(); break;
        }
    }

    private void AccelerationStateUpdate()
    {
        _currentRotateSpeed += Time.deltaTime * _maxRotateSpeed / _accelerationTime;

        if (_currentRotateSpeed >= _maxRotateSpeed)
        {
            _currentRotateSpeed = _maxRotateSpeed;
            _state = RotateState.Rotate;
            return;
        }

        _targetTransform.Rotate(0, 0, _currentRotateSpeed * Time.deltaTime);
    }

    private void DecelerationStateUpdate()
    {
        _currentRotateSpeed -= Time.deltaTime * _maxRotateSpeed / _decelerationTime;

        if (_currentRotateSpeed <= 0)
        {
            _currentRotateSpeed = 0;
            _state = RotateState.Idle;
            return;
        }

        _targetTransform.Rotate(0, 0, _currentRotateSpeed * Time.deltaTime);
    }

    private void RotateStateUpdate()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _rotateTime)
        {
            _currentTime = 0;
            _state = RotateState.Deceleration;
            return;
        }

        _targetTransform.Rotate(0, 0, _currentRotateSpeed * Time.deltaTime);
    }

    private void IdleStateUpdate()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _idleTime)
        {
            _currentTime = 0;
            _state = RotateState.Acceleration;
            return;
        }
    }
}

public enum RotateState
{
    Idle,
    Acceleration,
    Rotate,
    Deceleration
}