using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs;

public class Mob : Decal
{
    public float Health = 100;
    public bool Important;
    public float Speed = 0.15f;
    public float Attack = 1f;

    protected IA Ia = new NoIa();

    public float CollisionFactor = 1f;

    public Mob()
    {
        Solid = true;
    }

    public virtual void Collide(Mob other, GameTime gameTime)
    {
        Vector2 collisionVector = Misc.AngleVector(Position, other.Position);
        Position += collisionVector * CollisionFactor * gameTime.ElapsedGameTime.Milliseconds;
    }
    
}