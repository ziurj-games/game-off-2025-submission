using Godot;
using System.Threading.Tasks;

public partial class Player : Actionable
{
    [Export]
    private Control _playerInterface, _waveOptionsContainer, _targetSliderContainer;

    [Export]
    private ActionTargetSlider _targetSlider;

    [Signal]
    private delegate void GeneratedWaveResEventHandler();

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
        // GD.Print("ASYNC REACHEd");

        // DO THIS FKING BEFORE BECAUSE THE IF STATEMENT IS MAKING A LOCAL COPY? -- THIS WAS ANNOYING...
        await ToSignal(this, nameof(GeneratedWaveRes)); // Wait before alerting the game manager we have finished.

        // WaveRes is inherited.
        if (WaveRes is Wave waveResource)
        {
            // GD.PrintS($"[PRE-UPDATE EMIT] WAVE RESOURCE: {waveResource.WaveType} // {waveResource.TargetSliderFinalValue} ");
            waveResource.WaveType = selectedWave;
            EmitSignal(nameof(TurnSignal), [ waveResource ]);
            GD.PrintS($"[UPDATE EMIT] WAVE RESOURCE: {waveResource.WaveType} // {waveResource.TargetSliderFinalValue} ");
        }
    }

    private void GenerateWaveResource(int finalSliderValue)
    {
        Wave newWaveRes = new()
        {
            TargetSliderFinalValue = finalSliderValue
        };

        WaveRes = newWaveRes;
        Wave res = WaveRes as Wave;

        // GD.PrintS($"[GEN WAVE] RESOURCE: {res.WaveType} // {res.TargetSliderFinalValue} ");
        EmitSignal(nameof(GeneratedWaveRes));
    }
}
