using Godot;
using System;

public partial class Pin : Area2D
{
	[Export]
	public float PinSpeed { get; set; }

    [Export]
    private Marker2D _leftLimit, _rightLimit;

    private Vector2 _moveVector = Vector2.Right;
    private bool _moveLeft = true, _active = false;

    [Signal]
    public delegate void PinStoppedEventHandler();

    public override void _Ready()
	{
        GlobalPosition = _leftLimit.GlobalPosition;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept") && _active)
        {
            _active = false;
            EmitSignal(nameof(PinStopped));
        }
    }

    public void EnablePin()
    {
        _active = true;
    }

    public void ResetPin()
    {
        _active = false;
        
        Vector2 newPos = new()
        {
            X = _leftLimit.GlobalPosition.X,
            Y = GlobalPosition.Y
        };

        GlobalPosition = newPos;
    }

	public override void _PhysicsProcess(double delta)
	{
		if (_active) MovePin((float)delta);
    }

	private void MovePin(float delta)
	{
		if (GlobalPosition.X < _leftLimit.GlobalPosition.X) 
		{
            _moveLeft = false;
            _moveVector = Vector2.Right;
        } else if (GlobalPosition.X > _rightLimit.GlobalPosition.X) 
		{
            _moveLeft = true;
            _moveVector = Vector2.Left;
        }

        Translate(_moveVector * PinSpeed * (float)delta);
	}
}
