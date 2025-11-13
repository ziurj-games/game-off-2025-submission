using Godot;
using System;

public partial class HealthBar : Node3D
{
	[Export]
	ProgressBar _healthBar;

	[Export]
	Sprite3D _hbSprite;


    public override void _Ready()
    {
        _hbSprite.Texture = GetNodeOrNull<SubViewport>("SubViewportContainer/SubViewport").GetTexture();
        // Hide();
    }

    public void SetHealthBarMaxValue(float maxValue)
    {
        _healthBar.MaxValue = maxValue;
    }

    public void UpdateHealthBar(float newVal) 
    { 
        _healthBar.Value = newVal;

        if (_healthBar.Value == _healthBar.MaxValue)
            Hide();
        else if (_healthBar.Value < _healthBar.MaxValue && !Visible)
            Show();
    }

    public float GetHealthBarMaxValue() { return (float)_healthBar.MaxValue; }
}
