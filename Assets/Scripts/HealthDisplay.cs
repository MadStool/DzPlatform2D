using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Health))]
public class HealthDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider smoothHealthBar;

    [Header("Smooth Bar Settings")]
    [SerializeField] private float smoothSpeed = 1f;

    private void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();

        healthBar.maxValue = health.MaxHealth;
        smoothHealthBar.maxValue = health.MaxHealth;
    }

    private void Start()
    {
        UpdateHealthDisplay(health.CurrentHealth, health.MaxHealth);
        smoothHealthBar.value = health.CurrentHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHealthDisplay;
    }

    private void Update()
    {
        if (smoothHealthBar.value != healthBar.value)
        {
            float maxHealthDelta = health.MaxHealth * smoothSpeed * Time.deltaTime;
            smoothHealthBar.value = Mathf.MoveTowards(
                smoothHealthBar.value,
                healthBar.value,
                maxHealthDelta
            );
        }
    }

    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (healthText != null)
            healthText.text = $"{currentHealth}/{maxHealth}";

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }
}