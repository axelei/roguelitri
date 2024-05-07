using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Model.Things.Decals.Mobs.Player;

namespace roguelitri.Model.Things.Decals.Bullets;

public class EnemyBullet : Bullet
{
    
    public EnemyBullet(GameScene gameScene, Vector2 initialPosition) : base(gameScene)
    {
        Position = initialPosition;
        Color = Color.Red;
        Ia = new BulletIa(gameScene, true, initialPosition);
        Speed = 0.4f;
    }
    
    public override bool Collide(Mob other, GameTime gameTime)
    {
        if (other is Player player)
        {
            if (player.Collide(this, gameTime))
            {
                Hits--;
                if (Hits <= 0)
                {
                    Dead = true;
                }
                return true;
            }

        }

        return false;
    }
    
}