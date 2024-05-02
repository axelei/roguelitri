using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{

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
        int collisionBoxHeight = (int) (Texture.Height / 1.10f);
        int collisionBoxWidth = (int) (Texture.Width / 1.10f);
        int collisionBoxStartXHeight = Texture.Height - collisionBoxHeight;
        int collisionBoxStartXWidth = Texture.Width - collisionBoxWidth;
        HitBox = new Rectangle(collisionBoxStartXWidth, collisionBoxStartXHeight, collisionBoxHeight, collisionBoxWidth);
    }
}