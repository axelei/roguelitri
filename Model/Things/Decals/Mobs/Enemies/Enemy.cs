using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Ia;

namespace roguelitri.Model.Things.Decals.Mobs.Enemies;

public class Enemy : Mob
{
    
    private GameScene _gameScene;

    public Enemy(GameScene gameScene)
    {
        Ia = new BasicIa(gameScene);
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Ia.Update();

        Vector2 movementVector = Ia.MovementVector(Position);
        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Position += movementVector * Speed * gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}