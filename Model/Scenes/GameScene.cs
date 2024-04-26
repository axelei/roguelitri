using FmodForFoxes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelitri.Model.Things.Decals.Mobs.Player;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Scenes;

public class GameScene : Scene
{

    private Channel _playing;
    private Camera _camera;
    private Player _player;
    
    public override void Initialize()
    {
        _player = new Player(0);

        _playing = ResourceManager.Music.IntoTheNight.Play();
        _playing.Looping = true;
        
        _camera = new Camera(Game1.Graphics.GraphicsDevice.Viewport);
        
        
#if DEBUG
        
#endif

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.UpdateCamera(Game1.Graphics.GraphicsDevice.Viewport);
        _camera.Position = _player.Position;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, _camera.Transform);

        drawFloor(spriteBatch);
        
        drawMobs(spriteBatch);
        
        spriteBatch.End();
        
#if DEBUG
        spriteBatch.Begin();
        
        spriteBatch.End();
#endif
    }

    public override void Dispose()
    {
        
    }

    private void drawFloor(SpriteBatch spriteBatch)
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
        
        //spriteBatch.Draw(ResourceManager.Gfx.Textures.Dirt, new Vector2(0, 0), Color.White);
    }
    
    private void drawMobs(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_player.Texture, _player.Position, Color.White);
    }

    private void UpdateControls(GameTime gameTime)
    {
        if (Input.KeyPressed(Keys.Up))
        {
            _player.Position -= new Vector2(0, gameTime.ElapsedGameTime.Milliseconds) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Down))
        {
            _player.Position -= new Vector2(0, -gameTime.ElapsedGameTime.Milliseconds) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Left))
        {
            _player.Position -= new Vector2(gameTime.ElapsedGameTime.Milliseconds, 0) * _player.Speed;
        }

        if (Input.KeyPressed(Keys.Right))
        {
            _player.Position -= new Vector2(-gameTime.ElapsedGameTime.Milliseconds, 0) * _player.Speed;
        }
    }
}