using Godot;
using System;

public partial class OuterArea : Area2D
{
    [Export]
    Sprite2D _sprite;

    [Export]
    private CollisionShape2D _collisionShape;


    public override void _Ready()
    {
        UpdateCollisionShape();
    }

	/// <summary>
    /// Match the collision shape to the sprite shape.
    /// Allows us to modify the size (scaling via x axis) of the sprite and will then 
    /// update the collision shape to the size of the sprite.
    /// </summary>
	private void UpdateCollisionShape()
	{
        Vector2 newPosition = _sprite.GlobalPosition;
        Vector2 newScale = _sprite.Scale;

        _collisionShape.GlobalPosition = newPosition;
        _collisionShape.Scale = newScale;
    }
}
