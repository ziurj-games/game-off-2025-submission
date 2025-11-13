using Godot;
using System;

public partial class Wave : Resource
{
    public const string NormalWave = "Normal Wave";
    public const string RoyalWave = "Royal Wave";
    public const string EnthusiasticWave = "Enthusiastic Wave";
    public const string FingerWiggleWave = "Finger Wiggle Wave";

    [Export(PropertyHint.Enum, "Normal Wave,Royal Wave,Enthusiastic Wave,Finger Wiggle Wave")]
	public string WaveType { get; set; }

    // 2 = gray area
    // 1 = outer area
    // 0 = sweet spot
    [Export]
    public int TargetSliderFinalValue = 0; 
}
