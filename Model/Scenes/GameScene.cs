﻿using System.Collections.Generic;
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
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace roguelitri.Model.Scenes;

public class GameScene : Scene
{

    private Channel _playing;
    private Camera2D _camera;
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
        
        _camera = new Camera2D(Misc.NativeWidth, Misc.NativeHeight);

        _things.Add(_player);

        _posText = Misc.addText("POS X/Y: ", new Vector2(0, 25));
        _uiElements.Add(_posText);
        
#if DEBUG
#endif

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.Position = _player.Position - new Vector2(Misc.NativeWidth / 2, Misc.NativeHeight / 2);
        
#if DEBUG
        _posText.Text = $"POS X/Y: {_player.Position.X}/{_player.Position.Y}";
#endif

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, camera: _camera, blendState: BlendState.AlphaBlend);
        spriteBatch.Begin(transformMatrix: _camera.TransformationMatrix);
        
        // Draw game world
        DrawFloor(spriteBatch);
        DrawThings(spriteBatch);
        
        spriteBatch.End();
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
        int resX2 = Game1.Graphics.GraphicsDevice.Viewport.Width * 2;
        int resY2 = Game1.Graphics.GraphicsDevice.Viewport.Height * 2;
        int textureX = ResourceManager.Gfx.Textures.Dirt.Width;
        int textureY = ResourceManager.Gfx.Textures.Dirt.Height;
        
        int camX = (int) _camera.Position.X;
        int camY = (int) _camera.Position.Y;

        int nearestX = Misc.nearestMultiple(camX, textureX);
        int nearestY = Misc.nearestMultiple(camY, textureY);
        
        for (int x = nearestX - resX2; x < resX2 + nearestX; x += textureX)
        {
            for (int y = nearestY - resY2; y < resY2 + nearestY; y += textureY)
            {
                spriteBatch.Draw(ResourceManager.Gfx.Textures.Dirt, new Vector2(x, y), Color.White);
            }
        }
    }
    
    private void DrawThings(SpriteBatch spriteBatch)
    {
        foreach (Thing thing in _things)
        {
            spriteBatch.Draw(thing.Texture, thing.Position, new Rectangle(0,0,thing.Texture.Width, thing.Texture.Height), 
                Color.White, 0f, Vector2.Zero, thing.Scale, SpriteEffects.None, 0f);
        }
    }

    private void UpdateControls(GameTime gameTime)
    {
        Vector2 moveVector = Vector2.Zero;

        if (Input.AnyKeyPressed(Keys.Up, Keys.W)) moveVector.Y -= 1;
        if (Input.AnyKeyPressed(Keys.Down, Keys.S)) moveVector.Y += 1;
        if (Input.AnyKeyPressed(Keys.Left, Keys.A)) moveVector.X -= 1;
        if (Input.AnyKeyPressed(Keys.Right, Keys.D)) moveVector.X += 1;

        if (moveVector != Vector2.Zero)
        {
            moveVector.Normalize();
            _player.Position += moveVector * _player.Speed * gameTime.ElapsedGameTime.Milliseconds ;
        }
        
    }
    
}