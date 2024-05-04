using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri.Model.Things;

public class Thing : IEqualityComparer<Thing>
{
    public Texture2D Texture = ResourcesManager.Gfx.Textures.Default;
    public Vector2 Scale = Vector2.One;
    public Vector2 Position = Vector2.Zero;
    public Color Color = Color.White;
    public bool Dead;
    public Guid Id = Guid.NewGuid();
    public int Leaf;
    public float Rotation;

    public Thing()
    {
        
    }

    public bool Equals(Thing x, Thing y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;
        return x.Id.Equals(y.Id);
    }

    public int GetHashCode(Thing obj)
    {
        return Id.GetHashCode();
    }
}