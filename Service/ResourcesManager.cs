using System.IO;
using FmodForFoxes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace roguelitri.Service;

public static class ResourcesManager
{

    public static Texture2D Pixel;
    public static Texture2D Rectangle;
    
    public static class Fonts
    {
        public const string IbmVgaFont = "fonts/ibmVga.fnt";
        public const string Arcade = "fonts/arcadepix.fnt";
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

        public static class Enemies
        {
            public static Texture2D Bat;
        }
        
        public static class Sprites
        {
            public static Texture2D Bullet;
        }

        public static class Textures
        {
            public static Texture2D Default;
            public static Texture2D Dirt;
        }
    }
    
    public static class Sfx
    {
        public static SoundEffect PlayerHit;
    }

    public static void LoadContent(ContentManager content)
    {
        
        Pixel = new Texture2D(Game1.Graphics.GraphicsDevice, 1, 1);
        Pixel.SetData(new [] { Color.White });
        
        Rectangle = new Texture2D(Game1.Graphics.GraphicsDevice, 1, 1);
        Rectangle.SetData(new[] { Color.White });
        
        //// Music
        Music.IntoTheNight = CoreSystem.LoadStreamedSound("music/The_Spin_Wires_-_Into_The_Night.mp3");

        //// Gfx
        Gfx.Player = content.Load<Texture2D>("gfx/player");
        // Title
        Gfx.Title.Logo = content.Load<Texture2D>("gfx/title/title_logo");
        Gfx.Title.Background = LoadFromFile(Game1.ContentFolder + "/gfx/raw/title_background.png");
        
        // Enemies
        Gfx.Enemies.Bat = content.Load<Texture2D>("gfx/enemies/bat01"); // https://pixelius-vita.itch.io/monster-fantasy-bat
        
        // Textures
        Gfx.Textures.Default = content.Load<Texture2D>("gfx/default");
        Gfx.Textures.Dirt = content.Load<Texture2D>("gfx/textures/dirt");
        
        // Sprites
        Gfx.Sprites.Bullet = content.Load<Texture2D>("gfx/bullets/simple_bullet");
        
        //// Sfx
        // Player
        Sfx.PlayerHit = content.Load<SoundEffect>("sfx/player/player_hit");
    }

    private static Texture2D LoadFromFile(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open);
        Texture2D texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
        fileStream.Dispose();
        return texture;
    }
}