using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri;

public class Game1 : Game
{
    public GraphicsDeviceManager Graphics { get; }
    private SpriteBatch _spriteBatch;

    private SpriteFont _ibmVgaFont;

    private readonly GlobalKeys _globalKeys;

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        IsMouseVisible = true;
        _globalKeys = new GlobalKeys(this);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _ibmVgaFont = Content.Load<SpriteFont>("gfx/fonts/ibmVga");
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _globalKeys.Update();

        // TODO: Add your update logic here

    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        _spriteBatch.Begin();

        GraphicsDevice.Clear(Color.White);
        _spriteBatch.DrawString(_ibmVgaFont, "Roguelitri SNAPSHOT áéíóçñ", new Vector2(100, 100), Color.Black);

        _spriteBatch.End();
        // TODO: Add your drawing code here

    }
    
    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
        Environment.Exit(Environment.ExitCode);
    }
}
