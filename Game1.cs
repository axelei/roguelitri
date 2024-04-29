using System;
using FmodForFoxes;
using Gum.DataTypes;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using roguelitri.Model.Save;
using roguelitri.Model.Scenes;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri;

public class Game1 : Game
{
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static Settings Settings;
    public static GumProjectSave GumProject { get; private set; }
    
    private SpriteBatch _spriteBatch;
    private INativeFmodLibrary _nativeLibrary;

    private readonly GlobalKeysManager _globalKeysManager;
    
    private TextRuntime _fpsText;


    public Game1(INativeFmodLibrary nativeLibrary)
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        _globalKeysManager = new GlobalKeysManager(this);
        _nativeLibrary = nativeLibrary;
    }

    protected override void Initialize()
    {
        
        Logger.Initialize();

        Settings = SaveManager.LoadSettings();
        ApplySettings(Settings);
        
        Graphics.PreferredBackBufferWidth = 1280;
        Graphics.PreferredBackBufferHeight = 720;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.ApplyChanges();
        
        SystemManagers.Default = new SystemManagers(); 
        SystemManagers.Default.Initialize(Graphics.GraphicsDevice, fullInstantiation: true);
        
        GumProject = GumProjectSave.Load("ui/roguelitri.gumx", out GumLoadResult result);
        ObjectFinder.Self.GumProjectSave = GumProject;
        GumProject.Initialize();
        
        FmodManager.Init(_nativeLibrary, FmodInitMode.Core, "Content");
        
        _fpsText = Misc.addText("FPS: -", new Vector2(0, 0));
        
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
        FmodManager.Update();
        
        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        SceneManager.Draw(gameTime, _spriteBatch);
        SystemManagers.Default.Draw();
        
        _fpsText.Text = "FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.00");

        base.Draw(gameTime);
    }
    
    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);
        Logger.Log("Shutting down...");

        if (Settings.AutoSave)
        {
            SaveManager.SaveSettings(Settings);
        }
        
        Console.WriteLine("Another fine release from Enloartolameza Studios!");
        FmodManager.Unload();
        Logger.Log("Finished shutting down.");
        Logger.Dispose();
        Environment.Exit(Environment.ExitCode);
    }

    private void ApplySettings(Settings settings)
    {
        
    }
}
