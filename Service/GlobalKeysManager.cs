using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
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
            _gameInstance.Graphics.ToggleFullScreen();
        }

        if (Input.HasBeenPressed(Keys.F12))
        {
            var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}_{GameUtils.EpochMillis()}.png";
            var graphics = _gameInstance.Graphics;
            var presentationParams = graphics.GraphicsDevice.PresentationParameters;

            int[] backBuffer = new int[presentationParams.BackBufferWidth * presentationParams.BackBufferHeight];
            graphics.GraphicsDevice.GetBackBufferData(backBuffer);

            using var texture = new Texture2D(graphics.GraphicsDevice, presentationParams.BackBufferWidth, presentationParams.BackBufferHeight, false, presentationParams.BackBufferFormat);
            texture.SetData(backBuffer);

            using var stream = File.OpenWrite(filename);

            texture.SaveAsPng(stream, presentationParams.BackBufferWidth, presentationParams.BackBufferHeight);

            // TODO make it use system culture or be able to change
            Console.WriteLine(GeneralLangResource.Culture = CultureInfo.CurrentCulture);
            Console.WriteLine(Thread.CurrentThread.CurrentCulture.Name);
            Console.WriteLine(GeneralLangResource.wrote_screenshot_console_text, filename);
        }
        
    }


}