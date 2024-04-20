using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;
using roguelitri.Service;

namespace roguelitri;

public class Game1 : Game
{
    public GraphicsDeviceManager Graphics { get; }
    private SpriteBatch _spriteBatch;

    private readonly GlobalKeys _globalKeys;

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _globalKeys = new GlobalKeys(this);
    }

    protected override void Initialize()
    {

        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.ApplyChanges();
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {

        Resources.LoadContent(Content);

        MediaPlayer.Play(Resources.Music.TestSong);
        // TODO: use this.Content to load your game content here

        base.LoadContent();

    }

    protected override void Update(GameTime gameTime)
    {
        
        _globalKeys.Update();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        GraphicsDevice.Clear(Color.LightGray);

        _spriteBatch.DrawString(Resources.Fonts.IbmVgaFont, "Roguelitri SNAPSHOT áéíóçñ - àç", new Vector2(10, 10), Color.White);
        _spriteBatch.DrawString(Resources.Fonts.Arcade, "COPYRIGHT 2024 ENLOARTOLAMEZA STUDIOS", new Vector2(10, 30), Color.White);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
    
    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
        Console.WriteLine("Another fine release of Enloartolameza Studios!");
        Environment.Exit(Environment.ExitCode);
    }
}
