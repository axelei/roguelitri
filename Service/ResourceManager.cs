using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace roguelitri.Service;

public static class ResourceManager
{
    public static class Fonts
    {
        public static string IbmVgaFont;
        public static string Arcade;
    }

    public static class Music
    {
        public static Song TestSong;
    }

    public static class Gfx
    {
        public static class Title
        {
            public static Texture2D Logo;
            public static Texture2D Background;
        }
    }

    public static void LoadContent(ContentManager content)
    {
        
        //// Fonts
        Fonts.Arcade = "fonts/arcadepix.fnt";
        Fonts.IbmVgaFont = "fonts/ibmVga.fnt";

        //// Music
        Music.TestSong = content.Load<Song>("music/test");
        
        //// Gfx
        // Title
        Gfx.Title.Logo = content.Load<Texture2D>("gfx/title/title_logo");
        Gfx.Title.Background = loadFromFile("Content/raw/title/title_background.png");
    }

    private static Texture2D loadFromFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);
        Texture2D texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
        fileStream.Dispose();
        return texture;
    }
}