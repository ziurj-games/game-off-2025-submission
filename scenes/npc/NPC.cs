using System;
using Godot;

public partial class NPC : Actionable
{
    public async void ChooseWave()
    {
        // Little buffer before choosing an option.
        await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);

        Random random = new();

        string selectedWave = "";
        int waveChosen = random.Next(4);

        switch(waveChosen)
        {
            case 0:
                selectedWave = Wave.NormalWave;
                break;
            case 1:
                selectedWave = Wave.RoyalWave;
                break;
            case 2:
                selectedWave = Wave.EnthusiasticWave;
                break;
            case 3:
                selectedWave = Wave.FingerWiggleWave;
                break;
            default:
                GD.PrintErr("[NPC.c] ERROR: INVALID WAVE CHOSEN.");
                break;
        }

        if (WaveRes is Wave waveResource)
        {
            waveResource.WaveType = selectedWave;
            EmitSignal(nameof(TurnSignal), [ waveResource ]);
        }
    }
}
