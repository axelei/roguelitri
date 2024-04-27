using System.Collections.Generic;
using System.Linq;
using FmodForFoxes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
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
    
    private readonly List<Thing> _things = new ();
    
    public override void Initialize()
    {
        _player = new Player(0);

        _playing = ResourceManager.Music.IntoTheNight.Play();
        _playing.Looping = true;
        
        _camera = new Camera(Game1.Graphics.GraphicsDevice.Viewport);

        AddGrid();

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.UpdateCamera(Game1.Graphics.GraphicsDevice.Viewport);
        _camera.Position = _player.Position;

        _things.Add(_player);

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, _camera.Transform);

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
        float camX = _camera.Position.X;
        float camY = _camera.Position.X;

        for (float y = -camY % 1024 - 1024; y < 1024 + camY % 1024; y += 1024)
        {
            for (float x = -camX % 1024 - 1024; x < 1024 + camX % 1024; x += 1024)
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
    
    #if DEBUG

    private void AddGrid()
    {
        int bigTiles = 8;
        for (int x = -1024 * bigTiles; x < 1024 * bigTiles; x += 32)
        {
            for (int y = -1024 * bigTiles; y < 1024 * bigTiles; y+= 32)
            {
                _things.Add(new Thing
                {
                    Position = new Vector2(x, y),
                    Texture = ResourceManager.Pixel
                });
            }
        }
    }
    
    #endif
}