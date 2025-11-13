using Godot;
using System;

public partial class ActionTargetSlider : Node2D
{
    enum PinPosition
    {
        IN_SWEET_SPOT,
        IN_OUTER_AREA,
        IN_GRAY_AREA
    };

    private PinPosition currentPinPosition = PinPosition.IN_GRAY_AREA;

    [Export]
    private Pin _pin;

    [Signal]
    public delegate void SliderStoppedEventHandler(int value);

    public override void _Ready()
    {
        base._Ready();
        _pin.PinStopped += HandlePinStopped;
    }

    private void HandlePinStopped()
    {
        GD.PrintS($"PIN STOPPED {(int)currentPinPosition}");
        EmitSignal(nameof(SliderStopped), (int)currentPinPosition);
    }

    public void Enable()
    {
        _pin.EnablePin();
    }

    public void Reset()
    {
        _pin.ResetPin();
    }

	private void AreaEnteredSweetSpot(Area2D area) 
	{
        // GD.PrintS($"ENTERED THE SWEET SPOT");
        currentPinPosition = PinPosition.IN_SWEET_SPOT;
        // GD.PrintS($"{currentPinPosition}");
	}

    private void AreaExitedSweetSpot(Area2D area)
    {
        // GD.Print("EXITED THE SWEET SPOT");
        currentPinPosition = PinPosition.IN_OUTER_AREA;
        // GD.PrintS($"{currentPinPosition}");
    }

    private void OuterAreaPinEntered(Area2D area)
    {
        // GD.Print("ENTERED OUTER AREA");
        currentPinPosition = PinPosition.IN_OUTER_AREA;
        // GD.PrintS($"{currentPinPosition}");
    }

    private void OuterAreaPinExited(Area2D area)
    {
        // GD.Print("EXITED OUTER AREA");
        currentPinPosition = PinPosition.IN_GRAY_AREA;
        // GD.PrintS($"{currentPinPosition}\n");
    }

}
