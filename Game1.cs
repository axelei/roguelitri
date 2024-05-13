using System;
using Gum.DataTypes;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using MonoSound;
using RenderingLibrary;
using roguelitri.Model.Save;
using roguelitri.Model.Scenes;
using roguelitri.Service;
using roguelitri.Util;
using InputHelper = Apos.Input.InputHelper;

namespace roguelitri;

public class Game1 : Game
{

    public const string ContentFolder = "Content";
    
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static Settings Settings;
    public static GumProjectSave GumProject { get; private set; }
    
    private SpriteBatch _spriteBatch;
    public static RenderTarget2D RenderTarget; // As shown in https://www.youtube.com/watch?v=Zla4q0Z6Zwc
    private Rectangle _renderDestination;

    private readonly GlobalKeysManager _globalKeysManager;
    
    private TextRuntime _debugText;


    public Game1()
    {
        Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        _globalKeysManager = new GlobalKeysManager(this);
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        
        Logger.Initialize();

        Settings = SaveManager.LoadSettings();
        ApplySettings(Settings);
        
        Graphics.PreferredBackBufferWidth = Misc.NativeWidth;
        Graphics.PreferredBackBufferHeight = Misc.NativeHeight;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.SynchronizeWithVerticalRetrace = false;
        Graphics.ApplyChanges();
        
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.Title = Misc.AppName + " v" + Misc.AppVersion;
        Window.ClientSizeChanged += OnClientWindowSizeChanged;
        CalculateRenderDestination();
        Graphics.GraphicsDevice.Viewport = new Viewport(_renderDestination);
        
        RenderTarget = new RenderTarget2D(GraphicsDevice, Misc.NativeWidth, Misc.NativeHeight);
        
        SystemManagers.Default = new SystemManagers(); 
        SystemManagers.Default.Initialize(Graphics.GraphicsDevice, fullInstantiation: true);
        
        GumProject = GumProjectSave.Load("ui/roguelitri.gumx");
        ObjectFinder.Self.GumProjectSave = GumProject;
        GumProject.Initialize();
        
        MonoSoundLibrary.Init(this);
        
        _debugText = Misc.AddText("FPS: -", new Vector2(0, 0));
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        ResourcesManager.LoadContent(Content);
        
        EnemiesManager.LoadEnemies();
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        SceneManager.SetScene(new TitleScene());
        SceneManager.Content = Content;
        
        InputHelper.Setup(this);
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        InputHelper.UpdateSetup();
        _globalKeysManager.Update();

        SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
        
        SceneManager.Update(gameTime);

        InputHelper.UpdateCleanup();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        GraphicsDevice.SetRenderTarget(RenderTarget);

        SceneManager.Draw(gameTime, _spriteBatch);
        SystemManagers.Default.Draw();
        
#if DEBUG
        _debugText.Text = "FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.00");
#endif
        
        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState : SamplerState.PointClamp);
        _spriteBatch.Draw(RenderTarget, _renderDestination, Color.White);
        _spriteBatch.End();
        
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
        
        MonoSoundLibrary.DeInit();
        Console.WriteLine("Another fine release from Enloartolameza Studios!");
        Logger.Log("Finished shutting down.");
        Logger.Dispose();
        Environment.Exit(Environment.ExitCode);
    }

    private void ApplySettings(Settings settings)
    {
        
    }
    
    private void OnClientWindowSizeChanged(object sender, EventArgs e)
    {
        CalculateRenderDestination();
    }
    
    private void CalculateRenderDestination()
    {
        const float targetAspectRatio = (float) Misc.NativeWidth / Misc.NativeHeight;
        int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
        int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
        float currentAspectRatio = (float) width / height;
        
        if (currentAspectRatio > targetAspectRatio)
        {
            width = (int) (height * targetAspectRatio + 0.5f);
        }
        else
        {
            height = (int) (width / targetAspectRatio + 0.5f);
        }

        _renderDestination = new Rectangle(
            GraphicsDevice.PresentationParameters.BackBufferWidth / 2 - width / 2,
            GraphicsDevice.PresentationParameters.BackBufferHeight / 2 - height / 2,
            width,
            height);
    }
}
