using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetHealth TargetHealth { get; private set; }
    public TargetRotate TargetRotate { get; private set; }

    [Header("Rotation Settings")]
    [SerializeField] private float _maxRotateSpeed;
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _decelerationTime;
    [SerializeField] private float _rotateTime;
    [SerializeField] private float _idleTime;

    [Header("Health Settings")]
    [SerializeField] private float _maxHealth;

    public void Initialize()
    {
        TargetHealth = new(_maxHealth);
        TargetRotate = new(transform, _maxRotateSpeed, _accelerationTime, _decelerationTime, _rotateTime, _idleTime);

        TargetHealth.Initialize();

        Invoke(nameof(Kill), 3);
    }

    private void Kill()
    {
        TargetHealth.TakeDamage(int.MaxValue);
    }

    public void Update()
    {
        TargetRotate?.Update();
    }
}
