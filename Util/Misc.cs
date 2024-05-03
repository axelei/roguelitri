using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using roguelitri.Service;

namespace roguelitri.Util;

public static class Misc
{
    public static readonly Version AppVersion = typeof(Program).Assembly.GetName().Version;
    public static readonly string AppName = typeof(Program).Assembly.GetName().Name;
    
    public const int TileSize = 32;
    public const int NativeWidth = 1280;
    public const int NativeHeight = 720;
    
    public static Vector2 CenterImage(Vector2 imageSize, Vector2 containerSize)
    {
        return new Vector2((containerSize.X - imageSize.X) / 2, (containerSize.Y - imageSize.Y) / 2);
    }

    public static Vector2 CenterImage(Vector2 imageSize)
    {
        return CenterImage(imageSize, new Vector2(Game1.Graphics.PreferredBackBufferWidth, Game1.Graphics.PreferredBackBufferHeight));
    }

    public static TextRuntime AddText(String text, Vector2 position, String font = ResourcesManager.Fonts.IbmVgaFont)
    {
        var textRuntime = new TextRuntime
        {
            UseCustomFont = true,
            CustomFontFile = font,
            Text = text,
            X = position.X,
            Y = position.Y
        };
        textRuntime.AddToManagers(SystemManagers.Default, null);
        return textRuntime;
    }
    
    public static int NearestMultiple(int number, int multiple)
    {
        return (int) Math.Round((double) number / multiple) * multiple;
    }

    public static double Angle(Vector2 vector1, Vector2 vector2)
    {
        return Math.Atan2(vector1.Y - vector2.Y, vector1.X - vector2.X);
    }

    public static Vector2 AngleVector(Vector2 vector1, Vector2 vector2)
    {
        double angle = Angle(vector1, vector2);
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }
    
    public static RectangleF CreateRect(Vector2 v1, Vector2 v2) {
        float left = MathF.Min(v1.X, v2.X);
        float right = MathF.Max(v1.X, v2.X);
        float top = MathF.Min(v1.Y, v2.Y);
        float bottom = MathF.Max(v1.Y, v2.Y);

        return new RectangleF(left, top, right - left, bottom - top);
    }

    public static RectangleF MoveRect(RectangleF rectangleF, Vector2 vector)
    {
        return new RectangleF(rectangleF.X + vector.X, rectangleF.Y + vector.Y, rectangleF.Width, rectangleF.Height);
    }
}