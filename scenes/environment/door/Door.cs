using Godot;
using System;

public partial class Door : Node3D
{
    [Export]
    private AnimationPlayer _animationPlayer;

	public void OpenDoor() 
	{
        _animationPlayer.Play("open_door");
    }

	public void CloseDoor()
	{
        _animationPlayer.Play("close_door");
    }
}
