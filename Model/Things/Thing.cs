using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri.Model.Things;

public class Thing
{
    public Texture2D Texture = ResourceManager.Gfx.Textures.Default;
    public Vector2 Scale = Vector2.One;
    public Vector2 Position;

    public Thing()
    {
        
    }
}