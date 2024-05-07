using Microsoft.Xna.Framework;
using MonoGame.Extended;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Service;

namespace roguelitri.Model.Things.Decals.Bullets;

public abstract class Bullet : Mob
{

    public int Hits = 1;
    public const float HitBoxFactor = 0.8f;
    
    public Bullet(GameScene gameScene) : base(gameScene)
    {
        Solid = false;
        Texture = ResourcesManager.Gfx.Sprites.Bullet;
        Attack = 1;
        Speed = 0.6f;
        CalculateHitBox();
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Ia.Update(gameTime);
        
        Vector2 movementVector = Ia.MovementVector(Position);
        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Position += movementVector * Speed * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
    
    private new void CalculateHitBox()
    {
        float width = Texture.Width * HitBoxFactor;
        float height = Texture.Height * HitBoxFactor;
        HitBox = new RectangleF((Texture.Width - width) / 2, (Texture.Height - height) / 2, width, height);
    }
    
}