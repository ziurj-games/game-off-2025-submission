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
	public void UpdateCollisionShape()
	{
        Vector2 newPosition = _sprite.GlobalPosition;
        Vector2 newScale = _sprite.Scale;

        _collisionShape.GlobalPosition = newPosition;
        _collisionShape.Scale = newScale;
    }

    public void UpdateSpriteSize(float scale)
    {
        Vector2 newScale = _sprite.Scale;
        newScale.X = scale;

        _sprite.Scale = newScale;

        _collisionShape.Disabled = scale == 0;

        UpdateCollisionShape(); // Since we have a new shape, update the collision shape as well.
    }

    /// <summary>
    /// Update outer area origin position.
    /// Only updates the X value.
    /// </summary>
    /// <param name="xPos">New 'X' value global position</param>
    public void UpdateOuterAreaOrigin(float designatedOrigin, float OuterPositionVariance) 
    {
        // I just realized I might not use the designatedOrigin property.. I'll leave it for now.
        Random random = new();
        float posOffset = (float)(random.NextDouble() * (2 * OuterPositionVariance) - OuterPositionVariance);

        Vector2 newPos = Position;
        newPos.X = designatedOrigin + posOffset;

        Position = newPos;
    }

    public bool IsCollisionScaleZero()
    {
        return _collisionShape.Disabled;
    }
}
