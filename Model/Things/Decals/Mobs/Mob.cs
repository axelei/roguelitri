using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs;

public class Mob : Decal
{
    public float Health = 100;
    public float Speed = 0.2f;
    public bool Important;
    protected IA Ia = new NoIa();

    public float CollisionFactor = 1f;

    public Mob()
    {
        Solid = true;
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }

    public void Collide(Mob other, GameTime gameTime)
    {
        Vector2 collisionVector = Misc.AngleVector(Position, other.Position);
        Position += collisionVector * CollisionFactor * gameTime.ElapsedGameTime.Milliseconds;
    }
    
}