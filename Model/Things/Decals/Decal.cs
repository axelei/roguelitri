using Microsoft.Xna.Framework;
using MonoGame.Extended;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{
    
    private const float HitBoxFactor = 0.9f;
    private const float HeightHitBoxFactor = 0.65f;

    public RectangleF HitBox;
    public RectangleF HitBoxMoved => Misc.MoveRect(HitBox, Position);
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
        float collisionBoxHeight = Texture.Height * HitBoxFactor * HeightHitBoxFactor;
        float collisionBoxWidth = Texture.Width * HitBoxFactor;
        float collisionBoxStartTop = Texture.Height - collisionBoxHeight;
        float collisionBoxStartLeft = (Texture.Width - collisionBoxWidth) / 2;
        HitBox = new RectangleF(collisionBoxStartLeft, collisionBoxStartTop, collisionBoxWidth, collisionBoxHeight);
    }
}