﻿using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoSound;
using MonoSound.Streaming;

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
        public static StreamPackage TheExplorer;
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
            public static Texture2D Cacodemon;
            public static Texture2D Kobold;
            public static Texture2D Zombie;
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
        Music.TheExplorer = StreamLoader.GetStreamedSound("Content/music/TheExplorer.ogg", looping: true);

        //// Gfx
        Gfx.Player = content.Load<Texture2D>("gfx/player");
        // Title
        Gfx.Title.Logo = content.Load<Texture2D>("gfx/title/title_logo");
        Gfx.Title.Background = LoadFromFile("gfx/raw/title_background.png");
        
        // Enemies
        Gfx.Enemies.Bat = content.Load<Texture2D>("gfx/enemies/bat01"); // https://pixelius-vita.itch.io/monster-fantasy-bat
        Gfx.Enemies.Cacodemon = content.Load<Texture2D>("gfx/enemies/cacodemon"); // https://elthen.itch.io/2d-pixel-art-cacodaemon-sprites
        Gfx.Enemies.Kobold = content.Load<Texture2D>("gfx/enemies/kobold"); // https://xzany.itch.io/kobold-warrior-2d-pixel-art
        Gfx.Enemies.Zombie = content.Load<Texture2D>("gfx/enemies/zombie"); // https://opengameart.org/content/lpc-zombie
        
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
        using var fileStream = new FileStream($"{Game1.ContentFolder}/{path}", FileMode.Open);
        return Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
    }
}