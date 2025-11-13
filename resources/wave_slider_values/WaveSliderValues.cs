using Godot;
using System;

public partial class WaveSliderValues : Resource
{
	[Export]
    public float OuterWidthScale = 1.0f; // Did these manually and saved in a resource.

	[Export]
    public float OuterPosition = 0.0f; // Controls position of the sweetspot and outer position (sicne sweetspot is child of outer)

    [Export]
    public float OuterPositionVariance = 0.0f; // Offset variance

    [Export]
	public float PinSpeed = 100f;
}
