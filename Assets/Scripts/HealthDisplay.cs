using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Health))]
public class HealthDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthBar;

    private void Awake()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        _healthBar.maxValue = _health.MaxHealth;
        _healthBar.value = _health.CurrentHealth;

        if (_healthText != null)
            _healthText.text = $"{_health.CurrentHealth}/{_health.MaxHealth}";
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (_healthText != null)
            _healthText.text = $"{currentHealth}/{maxHealth}";

        _healthBar.maxValue = maxHealth;
        _healthBar.value = currentHealth;
    }
}