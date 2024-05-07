using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs;

public class Mob : Decal
{
    public float Health = 100;
    public float Speed = 0.1f;
    public float Attack = 1f;
    
    protected IA Ia = new NoIa();
    protected GameScene GameScene;

    public float CollisionFactor = 1f;

    public Mob(GameScene gameScene) : base()
    {
        Solid = true;
        GameScene = gameScene;
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Ia.Update(gameTime);
        if (Health <= 0)
        {
            Dead = true;
        }
    }

    public virtual bool Collide(Mob other, GameTime gameTime)
    {
        if (other.Solid)
        {
            Vector2 collisionVector = Misc.AngleVector(Position, other.Position);
            Position += collisionVector * CollisionFactor * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            return false;
        }

        return true;
    }
}
