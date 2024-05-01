using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals.Mobs.Ia;

public interface IA
{
    Vector2 MovementVector(Vector2 currentPosition);
    void Update();
}