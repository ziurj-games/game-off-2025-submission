using Godot;
using System;

public partial class MainMenu : Node
{
    [Export]
    private AnimationPlayer _cameraAnimationPlayer; // maybe only have one now? idk

    [Export]
    private Control _mainMenuInterface;

    private void _on_play_button_pressed()
	{
        _cameraAnimationPlayer.Play("move_to_chair");
        _mainMenuInterface.Hide();
    }

	private void _on_exit_button_pressed()
	{
        GetTree().Quit();
    }
}
