using Godot;
using System;

public partial class Player : Actionable
{
    [Export]
    private Control _playerInterface;

    public override void _Ready()
    {
        base._Ready();
    }

    public void EnableInterface(bool enable = true)
    {
        _playerInterface.Visible = enable;
    }

    public void ButtonPressed(string waveName)
    {
        // GD.Print("PRESSING PLAYER BUTTON");
        string selectedWave = "";

        switch(waveName)
        {
            case Wave.NormalWave:
                selectedWave = Wave.NormalWave;
                break;
            case Wave.RoyalWave:
                selectedWave = Wave.RoyalWave;
                break;
            case Wave.EnthusiasticWave:
                selectedWave = Wave.EnthusiasticWave;
                break;
            case Wave.FingerWiggleWave:
                selectedWave = Wave.FingerWiggleWave;
                break;
            default:
                GD.PrintErr("[Player.cs] ERROR: NOT A PROPER WAVE");
                break;
        }

        if (WaveRes is Wave waveResource)
        {
            waveResource.WaveType = selectedWave;
            EmitSignal(nameof(TurnSignal), [ waveResource ]);
        }

        EnableInterface(false);
    }
}
