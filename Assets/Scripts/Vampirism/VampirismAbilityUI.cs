using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(VampirismCore))]
public class VampirismAbilityUI : MonoBehaviour
{
    [Header("Timing Settings")]
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldown = 4f;

    [Header("UI References")]
    [SerializeField] private Image _abilityIndicator;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Canvas _abilityCanvas;

    private VampirismCore _vampirismCore;
    private bool _isAbilityActive;
    private bool _isOnCooldown;
    private float _abilityTimer;
    private float _cooldownTimer;

    private void Awake()
    {
        _vampirismCore = GetComponent<VampirismCore>();
        _abilityIndicator.enabled = false;
        _progressSlider.gameObject.SetActive(false);
        _progressSlider.maxValue = 1f;
    }

    private void Update()
    {
        HandleInput();
        UpdateAbility();
        UpdateCooldown();
        UpdateUI();
        FixUIRotation();
    }

    private void FixUIRotation()
    {
        if (_abilityCanvas != null)
        {
            _abilityCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isAbilityActive == false && _isOnCooldown == false)
        {
            ActivateAbility();
        }
    }

    private void ActivateAbility()
    {
        _isAbilityActive = true;
        _abilityTimer = _abilityDuration;
        _abilityIndicator.enabled = true;
        _progressSlider.gameObject.SetActive(true);
        _progressSlider.value = 1f;
    }

    private void UpdateAbility()
    {
        if (_isAbilityActive == false) 
            return;

        _abilityTimer -= Time.deltaTime;
        _vampirismCore.FindNearestEnemy();
        _vampirismCore.StealHealth();

        if (_abilityTimer <= 0)
        {
            DeactivateAbility();
        }
    }

    private void DeactivateAbility()
    {
        _isAbilityActive = false;
        _isOnCooldown = true;
        _cooldownTimer = _cooldown;
        _abilityIndicator.enabled = false;
        _vampirismCore.ResetTarget();
    }

    private void UpdateCooldown()
    {
        if (_isOnCooldown == false) 
            return;

        _cooldownTimer -= Time.deltaTime;

        if (_cooldownTimer <= 0)
        {
            _isOnCooldown = false;
        }
    }

    private void UpdateUI()
    {
        if (_isAbilityActive)
        {
            float progress = _abilityTimer / _abilityDuration;
            _progressSlider.value = progress;
            _timerText.text = Mathf.Ceil(_abilityTimer).ToString("0");
        }
        else if (_isOnCooldown)
        {
            float progress = 1f - (_cooldownTimer / _cooldown);
            _progressSlider.value = progress;
            _timerText.text = Mathf.Ceil(_cooldownTimer).ToString("0");
        }
        else
        {
            _progressSlider.value = 1f;
            _timerText.text = "Ready!";
        }
    }

    private void OnDrawGizmos()
    {
        if (_isAbilityActive)
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, _vampirismCore.AbilityRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _vampirismCore.AbilityRadius);
        }
    }
}