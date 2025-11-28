using Godot;

public partial class Actionable : CharacterBody3D
{
    [Export]
    public Resource WaveRes;

	[Signal]
    public delegate void TurnSignalEventHandler();

    public HealthBar HealthBarComponent;

    public override void _Ready()
    {
        HealthBarComponent = GetNode<HealthBar>("HealthBar");
    }
}
