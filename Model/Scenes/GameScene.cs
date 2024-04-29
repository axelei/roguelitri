using System;
using System.Collections.Generic;
using System.Linq;
using FmodForFoxes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using roguelitri.Model.Save;
using roguelitri.Model.Things;
using roguelitri.Model.Things.Decals.Mobs.Player;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Scenes;

public class GameScene : Scene
{

    private Channel _playing;
    private Camera _camera;
    private Player _player;
    private readonly HashSet<GraphicalUiElement> _uiElements = new ();

    private TextRuntime _posText;
    
    private readonly List<Thing> _things = new ();
    
    public override void Initialize()
    {
        _player = new Player(0);

        _playing = ResourceManager.Music.IntoTheNight.Play();
        _playing.Volume = Game1.Settings.MusicVolume;
        _playing.Looping = true;
        
        _camera = new Camera(Game1.Graphics.GraphicsDevice.Viewport);

        _things.Add(_player);

        _posText = Misc.addText("POS X/Y: ", new Vector2(0, 25));
        _uiElements.Add(_posText);

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.UpdateCamera(Game1.Graphics.GraphicsDevice.Viewport);
        _camera.Position = _player.Position;
        
#if DEBUG
        _posText.Text = $"POS X/Y: {_player.Position.X}/{_player.Position.Y}";
#endif

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, _camera.Transform);

        DrawFloor(spriteBatch);

        DrawThings(spriteBatch);
        
        spriteBatch.End();
        
#if DEBUG
        spriteBatch.Begin();
        spriteBatch.End();
#endif
    }

    public override void Dispose()
    {
        foreach (GraphicalUiElement graphicalUiElement in _uiElements)
        {
            graphicalUiElement.RemoveFromManagers();
        }
    }

    private void DrawFloor(SpriteBatch spriteBatch)
    {
        // 1024x1024
        int camX = (int) _camera.Position.X;
        int camY = (int) _camera.Position.Y;

        int nearestX = Misc.nearestMultiple(camX, 1024);
        int nearestY = Misc.nearestMultiple(camY, 1024);
        
        for (int x = nearestX - 2048; x < 2048 + nearestX; x += 1024)
        {
            for (int y = nearestY - 2048; y < 2048 + nearestY; y += 1024)
            {
                spriteBatch.Draw(ResourceManager.Gfx.Textures.Dirt, new Vector2(x, y), Color.White);
            }
        }
        
        spriteBatch.Draw(ResourceManager.Gfx.Textures.Dirt, new Vector2(0, 0), Color.White);
    }
    
    private void DrawThings(SpriteBatch spriteBatch)
    {
        foreach (Thing thing in _things)
        {
            spriteBatch.Draw(thing.Texture, thing.Position, Color.White);
        }
    }

    private void UpdateControls(GameTime gameTime)
    {
        if (Input.KeyPressed(Keys.Up))
        {
            _player.Position += new Vector2(0, -gameTime.ElapsedGameTime.Milliseconds) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Down))
        {
            _player.Position += new Vector2(0, gameTime.ElapsedGameTime.Milliseconds) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Left))
        {
            _player.Position += new Vector2(-gameTime.ElapsedGameTime.Milliseconds, 0) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Right))
        {
            _player.Position += new Vector2(gameTime.ElapsedGameTime.Milliseconds, 0) * _player.Speed;
        }
    }
    
}