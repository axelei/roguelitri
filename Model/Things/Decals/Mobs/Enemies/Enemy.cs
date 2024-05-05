using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Enemies;

public class Enemy : Mob
{
    
    private GameScene _gameScene;

    public Enemy(GameScene gameScene) : base(gameScene)
    {
        if (Misc.Random.Next(0, 3) == 0)
        {
            Ia = new ShootingBasicIa(gameScene, this);
        }
        else
        {
            Ia = new BasicIa(gameScene);
        }
        _gameScene = gameScene;
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Ia.Update(gameTime);

        Vector2 movementVector = Ia.MovementVector(Position);
        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Vector2 oldPosition = Position;
            Position += movementVector * Speed * gameTime.ElapsedGameTime.Milliseconds;
            FaceDirection = Misc.Angle(Position, oldPosition) - Math.PI / 2;
        }
    }
    
}