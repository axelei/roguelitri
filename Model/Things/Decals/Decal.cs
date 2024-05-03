using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{
    
    private const float HitBoxFactor = 1.10f;

    public RectangleF HitBox;
    public int Leaf;
    
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
        float collisionBoxHeight = Texture.Height / HitBoxFactor;
        float collisionBoxWidth = Texture.Width / HitBoxFactor;
        float collisionBoxStartXHeight = (Texture.Height - collisionBoxHeight) / 2;
        float collisionBoxStartXWidth = (Texture.Width - collisionBoxWidth) / 2;
        HitBox = new RectangleF(collisionBoxStartXWidth, collisionBoxStartXHeight, collisionBoxHeight, collisionBoxWidth);
    }
}