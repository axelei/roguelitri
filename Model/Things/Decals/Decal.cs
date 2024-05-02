using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{
    
    private const float HitBoxFactor = 1.10f;

    public Rectangle HitBox;

    public bool Solid;
    public float Depth = 0;
    public bool Important;

    public Decal() : base()
    {
        CalculateHitBox();
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }

    protected void CalculateHitBox()
    {
        int collisionBoxHeight = (int) (Texture.Height / HitBoxFactor);
        int collisionBoxWidth = (int) (Texture.Width / HitBoxFactor);
        int collisionBoxStartXHeight = Texture.Height - collisionBoxHeight;
        int collisionBoxStartXWidth = Texture.Width - collisionBoxWidth;
        HitBox = new Rectangle(collisionBoxStartXWidth, collisionBoxStartXHeight, collisionBoxHeight, collisionBoxWidth);
    }
}