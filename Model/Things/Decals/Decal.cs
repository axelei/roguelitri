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
        int collisionBoxHeight = (int) (Texture.Height / HitBoxFactor);
        int collisionBoxWidth = (int) (Texture.Width / HitBoxFactor);
        int collisionBoxStartXHeight = Texture.Height - collisionBoxHeight;
        int collisionBoxStartXWidth = Texture.Width - collisionBoxWidth;
        HitBox = new RectangleF(collisionBoxStartXWidth, collisionBoxStartXHeight, collisionBoxHeight, collisionBoxWidth);
    }
}