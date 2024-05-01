using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals.Mobs.Ia;

public class NoIa : IA
{
    public Vector2 MovementVector(Vector2 currentPosition)
    {
        return Vector2.Zero;
    }

    public void Update()
    {
    }
}