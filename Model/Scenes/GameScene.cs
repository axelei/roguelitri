using System;
using System.Collections.Generic;
using FmodForFoxes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using roguelitri.Model.Things;
using roguelitri.Model.Things.Decals.Mobs;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Model.Things.Decals.Mobs.Player;
using roguelitri.Service;
using roguelitri.Util;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace roguelitri.Model.Scenes;

public class GameScene : Scene
{
    public Player Player { get; private set; }
    public Level CurrLevel;
    
    private Channel _playing;
    private Camera2D _camera;
    private readonly HashSet<GraphicalUiElement> _uiElements = new ();

    private TextRuntime _debugText;
    
    private readonly List<Mob> _mobs = new ();
    
    public override void Initialize()
    {
        Player = new Player(0);
        CurrLevel = LevelManager.LoadLevel("Content/lvl/test.json");
        CurrLevel.Random = new Random(CurrLevel.Seed);
        

        _playing = ResourcesManager.Music.IntoTheNight.Play();
        _playing.Volume = Game1.Settings.MusicVolume;
        _playing.Looping = true;
        
        _camera = new Camera2D(Misc.NativeWidth, Misc.NativeHeight);
        _camera.Origin = new Vector2(Misc.NativeWidth / 2f, Misc.NativeHeight / 2f);

        _mobs.Add(Player);

        _debugText = Misc.AddText("POS X/Y: ", new Vector2(0, 25));
        _uiElements.Add(_debugText);
        
#if DEBUG
#endif

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.Position = Player.Position;
        
        List<Mob> mobsToRemove = new ();
        foreach (Mob mob in _mobs)
        {
            if (MobIsFarUnimportant(mob) || mob.Dead)
            {
                mobsToRemove.Add(mob);
                continue;
            }
            mob.Update(gameTime);
            if (mob.Solid)
            {
                CalculateCollisions(mob, gameTime);
            }
        }
        foreach (Mob mob in mobsToRemove)
        {
            _mobs.Remove(mob);
        }

        
#if DEBUG
        _debugText.Text = $"POS X/Y: {Player.Position.X:0000}/{Player.Position.Y:0000} THINGS: {_mobs.Count}";
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
        int textureX = ResourcesManager.Gfx.Textures.Dirt.Width;
        int textureY = ResourcesManager.Gfx.Textures.Dirt.Height;
        
        int nearestX = Misc.NearestMultiple((int) _camera.Position.X, textureX);
        int nearestY = Misc.NearestMultiple((int) _camera.Position.Y, textureY);
        
        for (int x = nearestX - resX2; x < resX2 + nearestX; x += textureX)
        {
            for (int y = nearestY - resY2; y < resY2 + nearestY; y += textureY)
            {
                spriteBatch.Draw(ResourcesManager.Gfx.Textures.Dirt, new Vector2(x, y), Color.White);
            }
        }
    }
    
    private void DrawThings(SpriteBatch spriteBatch)
    {
        foreach (Mob mob in _mobs)
        {
            spriteBatch.Draw(mob.Texture, mob.Position, new Rectangle(0,0,mob.Texture.Width, mob.Texture.Height), 
                mob.Color, 0f, Vector2.Zero, mob.Scale, SpriteEffects.None, 0f);
#if DEBUG
                Rectangle pos = new Rectangle((int) mob.Position.X, (int) mob.Position.Y, mob.HitBox.Width, mob.HitBox.Height);
                spriteBatch.Draw(ResourcesManager.Rectangle, pos, Color.Red * 0.2f);
#endif
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
            Player.Position += moveVector * Player.Speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        if (Input.AnyKeyPressed(Keys.E))
        {
            Random rnd = new Random();
            Enemy enemy = new Enemy(this)
            {
                Position = Player.Position + new Vector2(rnd.Next(-400, 400), rnd.Next(-400, 400))
            };
            _mobs.Add(enemy);
        }
        
    }

    private bool MobIsFarUnimportant(Mob mob)
    {
        return !mob.Important && Vector2.Distance(mob.Position, Player.Position) > Misc.NativeWidth * 2;
    }

    private void CalculateCollisions(Mob mob, GameTime gameTime)
    {
        foreach (Mob otherMob in _mobs)
        {
            if (!mob.Solid || mob == otherMob)
            {
                continue;
            }
            Rectangle hitBox1 = new Rectangle(otherMob.HitBox.X + (int) otherMob.Position.X,
                otherMob.HitBox.Y + (int) otherMob.Position.Y,
                otherMob.HitBox.Width, otherMob.HitBox.Height);
            Rectangle hitBox2 = new Rectangle(mob.HitBox.X + (int) mob.Position.X,
                mob.HitBox.Y + (int) mob.Position.Y,
                mob.HitBox.Width, mob.HitBox.Height);
            if (hitBox1.Intersects(hitBox2))
            {
                otherMob.Collide(mob, gameTime);
            }
        }
    }
    
}