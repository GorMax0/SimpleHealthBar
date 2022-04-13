using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Slider _healthBar;
    private TMP_Text _textHealth;
    private Coroutine _changeValue;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<Slider>();
        _textHealth = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _player.MaximumHealthChange += SetMaximumValue;
        _player.HealthChange += DisplayHealth;
    }

    private void OnDisable()
    {
        _player.MaximumHealthChange -= SetMaximumValue;
        _player.HealthChange -= DisplayHealth;
    }

    private void SetMaximumValue(float maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = maxHealth;
    }

    private void DisplayHealth(float currentHealth, float maxHealth)
    {
        _textHealth.text = $"{currentHealth}/{maxHealth}";
        StartChangeValue(currentHealth);
    }

    private void StartChangeValue(float currentHealth)
    {
        if (_changeValue != null)
        {
            StopCoroutine(_changeValue);
            _changeValue = null;
        }

       _changeValue = StartCoroutine(ChangeValue(currentHealth));
    }

    private IEnumerator ChangeValue(float currentHealth)
    {
        float changeStep = 0.1f;
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        while (_healthBar.value != currentHealth)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentHealth, changeStep);

            yield return waitForEndOfFrame;
        }
    }   
}
