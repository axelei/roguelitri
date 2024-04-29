﻿using System;
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
    private readonly INativeFmodLibrary _nativeLibrary;
    private RenderTarget2D _renderTarget; // As shown in https://www.youtube.com/watch?v=Zla4q0Z6Zwc
    private Rectangle _renderDestination;

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
        
        Graphics.PreferredBackBufferWidth = Misc.NativeWidth;
        Graphics.PreferredBackBufferHeight = Misc.NativeHeight;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.ApplyChanges();
        
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.Title = Misc.AppName + " v" + Misc.AppVersion;
        Window.ClientSizeChanged += OnClientWindowSizeChanged;
        CalculateRenderDestination();
        Graphics.GraphicsDevice.Viewport = new Viewport(_renderDestination);
        
        _renderTarget = new RenderTarget2D(GraphicsDevice, Misc.NativeWidth, Misc.NativeHeight);
        
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
        GraphicsDevice.SetRenderTarget(_renderTarget);

        SceneManager.Draw(gameTime, _spriteBatch);
        SystemManagers.Default.Draw();
        
#if DEBUG
        _fpsText.Text = "FPS: " + (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString("0.00");
#endif
        
        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState : SamplerState.PointClamp);
        _spriteBatch.Draw(_renderTarget, _renderDestination, Color.White);
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
        
        Console.WriteLine("Another fine release from Enloartolameza Studios!");
        FmodManager.Unload();
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
        float targetAspectRatio = (float) Misc.NativeWidth / Misc.NativeHeight;
        int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
        int height = GraphicsDevice.PresentationParameters.BackBufferHeight;
        float currentAspectRatio = (float) width / height;
        if (currentAspectRatio > targetAspectRatio)
        {
            width = (int)(height * targetAspectRatio + 0.5f);
        }
        else
        {
            height = (int)(width / targetAspectRatio + 0.5f);
        }
        _renderDestination = new Rectangle(
            GraphicsDevice.PresentationParameters.BackBufferWidth / 2 - width / 2,
            GraphicsDevice.PresentationParameters.BackBufferHeight / 2 - height / 2,
            width,
            height);
        
        /*
        Point size = GraphicsDevice.Viewport.Bounds.Size;

        float scaleX = (float) size.X / _renderTarget.Width;
        float scaleY = (float)size.Y / _renderTarget.Height;
        float scale = Math.Min(scaleX, scaleY);

        _renderDestination.Width = (int)(_renderTarget.Width * scale);
        _renderDestination.Height = (int)(_renderTarget.Height * scale);

        _renderDestination.X = (size.X - _renderDestination.Width) / 2;
        _renderDestination.Y = (size.Y - _renderDestination.Height) / 2;*/
        
    }
}
