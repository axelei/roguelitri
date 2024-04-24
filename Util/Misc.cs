using System;
using Microsoft.Xna.Framework;

namespace roguelitri.Util;

public static class Misc
{
    public static readonly Version AppVersion = typeof(Program).Assembly.GetName().Version;
    public static readonly string AppName = typeof(Program).Assembly.GetName().Name;
    
    public static Vector2 CenterImage(Vector2 imageSize, Vector2 containerSize)
    {
        return new Vector2((containerSize.X - imageSize.X) / 2, (containerSize.Y - imageSize.Y) / 2);
    }

    public static Vector2 CenterImage(Vector2 imageSize)
    {
        return CenterImage(imageSize, new Vector2(Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight));
    }
}