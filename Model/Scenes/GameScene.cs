using System;
using System.Collections.Generic;
using System.Linq;
using Apos.Input;
using Apos.Shapes;
using Apos.Spatial;
using FmodForFoxes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGameGum.GueDeriving;
using roguelitri.Model.Things.Decals.Mobs;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Model.Things.Decals.Mobs.Player;
using roguelitri.Service;
using roguelitri.Util;
using InputHelper = roguelitri.Util.InputHelper;
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
    private readonly ShapeBatch _sb = new (Game1.Graphics.GraphicsDevice, SceneManager.Content);
    
    private readonly AABBTree<Mob> _mobs = new (1024, 0, 1024);
    
    private readonly ICondition _pauseCondition = new KeyboardCondition(Keys.P);
    private readonly ICondition _createEnemyCondition = new KeyboardCondition(Keys.E);
    
    public override void Initialize()
    {
        CurrLevel = LevelManager.LoadLevel("Content/lvl/test.json");
        CurrLevel.Random = new Random(CurrLevel.Seed);

        _playing = ResourcesManager.Music.IntoTheNight.Play();
        _playing.Volume = Game1.Settings.MusicVolume;
        _playing.Looping = true;
        
        _camera = new Camera2D(Misc.NativeWidth, Misc.NativeHeight);
        _camera.Origin = new Vector2(Misc.NativeWidth / 2f, Misc.NativeHeight / 2f);

        Player = new Player(0);
        Player.Leaf = _mobs.Add(Player.HitBox, Player);

        _debugText = Misc.AddText("", new Vector2(0, 25));
        _uiElements.Add(_debugText);
        
#if DEBUG
#endif

    }

    public override void Update(GameTime gameTime)
    {
        UpdateControls(gameTime);

        _camera.Position = Player.Position;
        
        foreach (Mob mob in _mobs.ToList())
        {
            if (MobIsFarUnimportant(mob) || mob.Dead)
            {
                _mobs.Remove(mob.Leaf);
                continue;
            }
            mob.Update(gameTime);
        }
        
        foreach (Mob mob in _mobs.ToList().Where(mob => mob.Solid))
        {
            _mobs.Update(mob.Leaf, mob.HitBoxMoved);
        }

        foreach (Mob mob in _mobs.Where(mob => mob.Solid))
        {
            CalculateCollisions(mob, gameTime);
        }

        
#if DEBUG
        _debugText.Text = $"POS X/Y: {Player.Position.X:0000.00}/{Player.Position.Y:0000.00} MOBS: {_mobs.Count:0000}";
#endif

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: _camera.TransformationMatrix);
        
        // Draw game world
        DrawFloor(spriteBatch);
        DrawThings(spriteBatch);
        
        spriteBatch.End();

#if DEBUG
        _sb.Begin(view: _camera.TransformationMatrix);
        foreach (RectangleF rect in _mobs.DebugAllNodes()) {
            _sb.BorderRectangle(rect.TopLeft, rect.Size, Color.White * 0.3f);
        }
        _sb.End();
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
        foreach (Mob mob in _mobs.Where(MobInCameraView).OrderByDescending(mob => mob.Depth).ThenBy(mob => mob.Position.Y))
        {
            spriteBatch.Draw(mob.Texture, mob.Position, new Rectangle(0,0,mob.Texture.Width, mob.Texture.Height), 
                mob.Color, 0f, Vector2.Zero, mob.Scale, SpriteEffects.None, 0f);
#if DEBUG
                Rectangle pos = Misc.MoveRect(mob.HitBox, mob.Position).ToRectangle();
                spriteBatch.Draw(ResourcesManager.Rectangle, pos, Color.Red * 0.2f);
#endif
        }
    }
    
    private bool MobInCameraView(Mob mob)
    {
        //TODO: Implement camera view check
        return true;
    }

    private void UpdateControls(GameTime gameTime)
    {
        if (_pauseCondition.Pressed())
        {
            SceneManager.PushScene(new PauseScene(Misc.GetScreenshot()));
        }
        
        Vector2 moveVector = Vector2.Zero;

        if (InputHelper.KeysPressed(Keys.Up, Keys.W)) moveVector.Y -= 1;
        if (InputHelper.KeysPressed(Keys.Down, Keys.S)) moveVector.Y += 1;
        if (InputHelper.KeysPressed(Keys.Left, Keys.A)) moveVector.X -= 1;
        if (InputHelper.KeysPressed(Keys.Right, Keys.D)) moveVector.X += 1;

        if (moveVector != Vector2.Zero)
        {
            moveVector.Normalize();
            Player.Position += moveVector * Player.Speed * gameTime.ElapsedGameTime.Milliseconds;
            _mobs.Update(Player.Leaf, Player.HitBoxMoved);
        }

        if (_createEnemyCondition.Pressed() || InputHelper.KeysPressed(Keys.R))
        {
            Random rnd = new Random();
            Enemy enemy = new Enemy(this)
            {
                Position = Player.Position + new Vector2(rnd.Next(-400, 400), rnd.Next(-400, 400))
            };
            enemy.Leaf = _mobs.Add(enemy.HitBoxMoved, enemy);
        }
        
    }

    private bool MobIsFarUnimportant(Mob mob)
    {
        return !mob.Important && Vector2.Distance(mob.Position, Player.Position) > Misc.NativeWidth * 2;
    }

    private void CalculateCollisions(Mob mob, GameTime gameTime)
    {
        foreach (Mob otherMob in _mobs.Query(mob.HitBoxMoved).Where(otherMob => otherMob.Solid))
        {
            if (otherMob.Id == mob.Id) continue;
            otherMob.Collide(mob, gameTime);
        }
    }
    
}