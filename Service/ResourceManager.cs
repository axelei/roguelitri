using System.IO;
using FmodForFoxes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace roguelitri.Service;

public static class ResourceManager
{

    public static Texture2D Pixel;
    
    public static class Fonts
    {
        public const string IbmVgaFont = "fonts/arcadepix.fnt";
        public const string Arcade = "fonts/ibmVga.fnt";
    }

    public static class Music
    {
        public static Sound IntoTheNight;
    }

    public static class Gfx
    {
        public static Texture2D Player;
        
        public static class Title
        {
            public static Texture2D Logo;
            public static Texture2D Background;
        }

        public static class Textures
        {
            public static Texture2D Default;
            public static Texture2D Dirt;
        }
    }

    public static void LoadContent(ContentManager content)
    {
        
        Pixel = new Texture2D(Game1.Graphics.GraphicsDevice, 1, 1);
        Pixel.SetData(new [] {Color.White});
        
        //// Music
        Music.IntoTheNight = CoreSystem.LoadStreamedSound("raw/music/The_Spin_Wires_-_Into_The_Night.mp3");

        //// Gfx
        Gfx.Player = content.Load<Texture2D>("gfx/player");
        // Title
        Gfx.Title.Logo = content.Load<Texture2D>("gfx/title/title_logo");
        Gfx.Title.Background = LoadFromFile("Content/raw/title/title_background.png");
        
        // Textures
        Gfx.Textures.Default = content.Load<Texture2D>("gfx/default");
        Gfx.Textures.Dirt = content.Load<Texture2D>("gfx/textures/dirt");
    }

    private static Texture2D LoadFromFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);
        Texture2D texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
        fileStream.Dispose();
        return texture;
    }
}