using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelitri.util;

namespace roguelitri.Service;

public class GlobalKeys
{
    

    private readonly Game1 _gameInstance;
    
    public GlobalKeys(Game1 gameInstance)
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
            var graphics = _gameInstance.Graphics;
            graphics.ToggleFullScreen();
        }

        if (Input.HasBeenPressed(Keys.F12))
        {
            var filename = Assembly.GetExecutingAssembly().GetName().Name + "_" + GameUtils.EpochMillis() + ".png";
            var graphics = _gameInstance.Graphics;
            int w = graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int h = graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;
            int[] backBuffer = new int[w * h];
            
            graphics.GraphicsDevice.GetBackBufferData(backBuffer);
            Texture2D texture = new Texture2D(graphics.GraphicsDevice, w, h, false, graphics.GraphicsDevice.PresentationParameters.BackBufferFormat);
            texture.SetData(backBuffer);
            Stream stream = File.OpenWrite(filename);
            texture.SaveAsPng(stream, w, h);
            
            stream.Dispose();
            texture.Dispose();
            
            Console.WriteLine("Wrote screenshot: " + filename);
        }
        
    }


}