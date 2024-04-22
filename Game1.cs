using System;
using Gum.DataTypes;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RenderingLibrary;
using roguelitri.Model.Scenes;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri;

public class Game1 : Game
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static GumProjectSave GumProject { get; private set; }
    private SpriteBatch _spriteBatch;

    private readonly GlobalKeysManager _globalKeysManager;

    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        _globalKeysManager = new GlobalKeysManager(this);
    }

    protected override void Initialize()
    {
        
        Logger.Initialize();
        
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.ApplyChanges();
        
        SystemManagers.Default = new SystemManagers(); 
        SystemManagers.Default.Initialize(Graphics.GraphicsDevice, fullInstantiation: true);
        
        GumProject = GumProjectSave.Load("ui/roguelitri.gumx", out GumLoadResult result);
        result.ErrorMessage.Length.ToString();
        ObjectFinder.Self.GumProjectSave = GumProject;
        GumProject.Initialize();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ResourceManager.LoadContent(Content);
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        SceneManager.SetScene(new TitleScene());
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _globalKeysManager.Update();

        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
        
        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SceneManager.Draw(gameTime, _spriteBatch);
        SystemManagers.Default.Draw();

        base.Draw(gameTime);
    }
    
    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
        Logger.Log("Shutting down...");
        Console.WriteLine("Another fine release from Enloartolameza Studios!");
        Logger.Dispose();
        Environment.Exit(Environment.ExitCode);
    }
}
