using System.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResourceManager = roguelitri.Service.ResourceManager;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{

    public Vector4 CollisionBox;

    public bool Solid;
    public float Depth = 0;

    public Decal() : base()
    {
        CollisionBox = new Vector4(0, 0, Texture.Height, Texture.Width);
    }
}