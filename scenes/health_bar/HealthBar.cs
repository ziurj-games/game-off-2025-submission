using Godot;
using System;

public partial class HealthBar : Node3D
{
	[Export]
	ProgressBar _healthBar;

	[Export]
	Sprite3D _hbSprite;

    [Export]
    private float _maxHealth = 100;

    [Export]
    private float _currentHealth;

    [Signal]
    public delegate void EntityDiedEventHandler();

    private float _targetHealth; // Smoothing

    public override void _Ready()
    {
        _hbSprite.Texture = GetNodeOrNull<SubViewport>("SubViewportContainer/SubViewport").GetTexture();


        SetHealthBarMaxValue(_maxHealth);
        _currentHealth = _targetHealth = _maxHealth;
        // Hide();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Mathf.Abs(_healthBar.Value - _targetHealth) > 0.1f)
            _healthBar.Value = Mathf.Lerp(_healthBar.Value, _targetHealth, 0.085f);
    }

    public void SetHealthBarMaxValue(float maxValue)
    {
        _healthBar.MaxValue = maxValue;
    }

    public bool HasEntityDied()
    {
        if (_currentHealth <= 0)
        {
            EmitSignal(nameof(EntityDied));
            return true;
        }

        return false;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        UpdateHealthBar(_currentHealth);
    }

    public void Heal(float healAmount) 
    {
        _currentHealth += healAmount;
        UpdateHealthBar(_currentHealth);
    }

    public void UpdateHealthBar(float newVal)
    {
        _targetHealth = newVal;
    }

    public float GetHealthBarMaxValue() { return (float)_healthBar.MaxValue; }
}
