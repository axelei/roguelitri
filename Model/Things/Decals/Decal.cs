using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{

    public Rectangle CollisionBox;

    public bool Solid;
    public float Depth = 0;

    public Decal() : base()
    {
        CollisionBox = new Rectangle(0, 0, Texture.Height, Texture.Width);
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }
}