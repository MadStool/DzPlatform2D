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
    [SerializeField] private Slider _smoothHealthBar;

    [Header("Smooth Bar Settings")]
    [SerializeField] private float smoothSpeed = 1f;

    private void Awake()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        _healthBar.maxValue = _health.MaxHealth;
        _smoothHealthBar.maxValue = _health.MaxHealth;
    }

    private void Start()
    {
        UpdateHealthDisplay(_health.CurrentHealth, _health.MaxHealth);
        _smoothHealthBar.value = _health.CurrentHealth;
    }

    private void OnEnable()
    {
        _health.OnHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        _health.OnHealthChanged -= UpdateHealthDisplay;
    }

    private void Update()
    {
        if (Mathf.Approximately(_smoothHealthBar.value, _healthBar.value) == false)
        {
            float maxHealthDelta = _health.MaxHealth * smoothSpeed * Time.deltaTime;
            _smoothHealthBar.value = Mathf.MoveTowards(
                _smoothHealthBar.value,
                _healthBar.value,
                maxHealthDelta
            );
        }
    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (_healthText != null)
            _healthText.text = $"{currentHealth}/{maxHealth}";

        if (_healthBar != null)
        {
            _healthBar.maxValue = maxHealth;
            _healthBar.value = currentHealth;
        }
    }
}