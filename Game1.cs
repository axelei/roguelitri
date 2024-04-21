using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Model.Scenes;
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
        IsMouseVisible = false;
        _globalKeys = new GlobalKeys(this);
    }

    protected override void Initialize()
    {
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Resources.LoadContent(Content);
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        SceneManager.SetScene(new TitleScene());
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _globalKeys.Update();

        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SceneManager.Draw(gameTime, _spriteBatch);

        base.Draw(gameTime);
    }
    
    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
        Console.WriteLine("Another fine release from Enloartolameza Studios!");
        Environment.Exit(Environment.ExitCode);
    }
}
