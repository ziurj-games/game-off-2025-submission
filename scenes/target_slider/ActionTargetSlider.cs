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

    [Export]
    private OuterArea _outerArea;

    [Signal]
    public delegate void SliderStoppedEventHandler(int value);


    public override void _Ready()
    {
        base._Ready();
        _pin.PinStopped += HandlePinStopped;
    }

    private void HandlePinStopped()
    {
        int lockedPinPosition = (int)currentPinPosition;

        GD.PrintS($"PIN STOPPED {lockedPinPosition}");
        EmitSignal(nameof(SliderStopped), lockedPinPosition);
    }

    public void UpdateTargetSlider(WaveSliderValues sliderValues)
    {
        _outerArea.UpdateSpriteSize(sliderValues.OuterWidthScale);
        _pin.PinSpeed = sliderValues.PinSpeed;
        _outerArea.UpdateOuterAreaOrigin(sliderValues.OuterPosition, sliderValues.OuterPositionVariance);
    }

    public void Enable()
    {
        _pin.EnablePin();
    }

    public void Reset()
    {
        _pin.ResetPin();
        currentPinPosition = PinPosition.IN_GRAY_AREA;
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
        if (_outerArea.IsCollisionScaleZero())
        {
            currentPinPosition = PinPosition.IN_GRAY_AREA;
            GD.Print("GRAY");
        } else 
        {
            currentPinPosition = PinPosition.IN_OUTER_AREA;
            GD.Print("OUTER");
        }
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
