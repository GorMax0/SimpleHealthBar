using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _minHealth = 0;
    private float _currentHealth;

    public event UnityAction<float> MaximumHealthChange;
    public event UnityAction<float, float> HealthChange;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        MaximumHealthChange?.Invoke(_maxHealth);
        HealthChange?.Invoke(_currentHealth, _maxHealth);
    }

    public void Heal(float health)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + health, _minHealth, _maxHealth);
        HealthChange?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);
        HealthChange?.Invoke(_currentHealth, _maxHealth);
    }
}
