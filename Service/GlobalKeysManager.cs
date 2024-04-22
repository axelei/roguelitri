using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelitri.util;

namespace roguelitri.Service;

public class GlobalKeysManager
{
    
    private readonly Game1 _gameInstance;
    
    public GlobalKeysManager(Game1 gameInstance)
    {
        _gameInstance = gameInstance;

    }
    
    public void Update()
    {
        // Exit
        if (Input.ButtonPressed(PlayerIndex.One, Buttons.Back) || Input.KeyPressed(Keys.Escape) || Input.KeyPressed(Keys.LeftAlt, Keys.F4))
        {
            _gameInstance.Exit();
        }

        // Full screen
        if ((Input.KeyPressed(Keys.LeftAlt) || Input.KeyPressed(Keys.RightAlt)) && Input.KeyPressed(Keys.Enter))
        {
            Game1.Graphics.ToggleFullScreen();
        }

        // Screenshot
        if (Input.HasBeenPressed(Keys.F12))
        {
            var filename = $"{Misc.AppName}_{GameUtils.EpochMillis()}.png";
            var graphics = Game1.Graphics;
            var presentationParams = graphics.GraphicsDevice.PresentationParameters;

            int[] backBuffer = new int[presentationParams.BackBufferWidth * presentationParams.BackBufferHeight];
            graphics.GraphicsDevice.GetBackBufferData(backBuffer);

            using var texture = new Texture2D(graphics.GraphicsDevice, presentationParams.BackBufferWidth, presentationParams.BackBufferHeight, false, presentationParams.BackBufferFormat);
            texture.SetData(backBuffer);

            using var stream = File.OpenWrite(filename);

            texture.SaveAsPng(stream, presentationParams.BackBufferWidth, presentationParams.BackBufferHeight);

            Logger.Log("Wrote screenshot: " + filename);
        }
        
    }


}