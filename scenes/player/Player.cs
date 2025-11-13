using Godot;
using System.Threading.Tasks;

public partial class Player : Actionable
{
    [Export]
    private Control _playerInterface, _waveOptionsContainer, _targetSliderContainer;

    [Export]
    private ActionTargetSlider _targetSlider;

    public override void _Ready()
    {
        base._Ready();
        _targetSlider.SliderStopped += GenerateWaveResource;
    }

    public void EnableWaveOptionsInterface(bool enable = true)
    {
        _waveOptionsContainer.Visible = enable;
    }

    public void EnableSliderInterface(bool enable = true)
    {
        _targetSliderContainer.Visible = enable;
    }

    public async void ButtonPressed(string waveName)
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

        EnableWaveOptionsInterface(false);

        EnableSliderInterface();
        _targetSlider.Enable();

        // Wait for the slider process to finish (this pauses until SliderStopped is emitted)
        await UpdateAndEmitWaveResourceAsync(selectedWave);

        EnableSliderInterface(false);
        _targetSlider.Reset();

    }

    private async Task UpdateAndEmitWaveResourceAsync(string selectedWave)
    {
        GD.Print("ASYNC REACHEd");

        // WaveRes is inherited.
        if (WaveRes is Wave waveResource)
        {
            waveResource.WaveType = selectedWave;
            await ToSignal(_targetSlider, nameof(_targetSlider.SliderStopped)); // Wait before alerting the game manager we have finished.
            EmitSignal(nameof(TurnSignal), [ waveResource ]);
            GD.PrintS($"WAVE RESOURCE: {waveResource.WaveType} // {waveResource.TargetSliderFinalValue} ");
        }
    }

    private void GenerateWaveResource(int finalSliderValue)
    {
        Wave newWaveRes = new()
        {
            TargetSliderFinalValue = finalSliderValue
        };

        WaveRes = newWaveRes;
    }
}
