using System;
using System.IO;
using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelitri.Util;

namespace roguelitri.Service;

public class GlobalKeysManager
{
    
    private readonly Game1 _gameInstance;
    
    private readonly ICondition _exitCondition =
        new AnyCondition(
            new AllCondition(new KeyboardCondition(Keys.RightAlt), new KeyboardCondition(Keys.F4)),
            new AllCondition(new KeyboardCondition(Keys.LeftAlt), new KeyboardCondition(Keys.F4)),
            new KeyboardCondition(Keys.Escape),
            new GamePadCondition(GamePadButton.Back, 0)
        );
    private readonly ICondition _fullScreenCondition = new AllCondition(
        new KeyboardCondition(Keys.LeftAlt), new KeyboardCondition(Keys.Enter));
    private readonly ICondition _screenshotCondition = new KeyboardCondition(Keys.F12);
    
    public GlobalKeysManager(Game1 gameInstance)
    {
        _gameInstance = gameInstance;

    }
    
    public void Update()
    {
        // Exit
        if (_exitCondition.Pressed())
        {
            _gameInstance.Exit();
        }

        // Full screen
        if (_fullScreenCondition.Pressed())
        {
            Game1.Graphics.ToggleFullScreen();
        }

        // Screenshot
        if (_screenshotCondition.Pressed())
        {
            var filename = $"{Misc.AppName}_{GameUtils.EpochMillis()}.png";
            Logger.Log($"Taking screenshot: {filename}");

            Texture2D screenshot = Misc.GetScreenshot();
            using FileStream stream = File.OpenWrite(filename);
            screenshot.SaveAsPng(stream, screenshot.Width, screenshot.Height);

            Logger.Log($"Wrote screenshot: {filename}");
        }
        
    }
    



}