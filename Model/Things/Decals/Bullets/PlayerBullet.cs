using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Model.Things.Decals.Mobs.Ia;

namespace roguelitri.Model.Things.Decals.Bullets;

public class PlayerBullet : Bullet
{
    public PlayerBullet(GameScene gameScene, Vector2 initialPosition) : base(gameScene)
    {
        Position = initialPosition;
        Color = Color.Blue;
        Ia = new BulletIa(gameScene, false, initialPosition);
    }

    public override void Collide(Mob other, GameTime gameTime)
    {
        if (other is Enemy enemy)
        {
            enemy.Health -= Attack;
            Hits--;
            if (Hits <= 0)
            {
                Dead = true;
            }
        }
    }
    
}