using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Bullets;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Enemies;

public class Enemy : Mob
{
    
    public Enemy(GameScene gameScene, string name = "") : base(gameScene)
    {

        if (name != "")
        {
            EnemiesManager.SetEnemy(name, gameScene, this);
            CalculateHitBox();
        }
        else
        {
            if (Misc.Random.Next(0, 15) == 0)
            {
                Ia = new ShootingBasicIa(gameScene, this);
            }
            else
            {
                Ia = new BasicIa(gameScene);
            }
        }
    }

    public override bool Collide(Mob other, GameTime gameTime)
    {
        if (other is PlayerBullet bullet)
        {
            Health -= Math.Min(bullet.Attack, 0.01f);
        }

        return base.Collide(other, gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        Ia.Update(gameTime);

        Vector2 movementVector = Ia.MovementVector(HitBoxMoved.Center);
        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Vector2 oldPosition = Position;
            Position += movementVector * Speed * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            FaceDirection = Misc.Angle(Position, oldPosition);
        }
        
        base.Update(gameTime);
    }
    
}