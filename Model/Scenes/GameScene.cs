using System;
using System.Collections.Generic;
using System.Linq;
using Apos.Input;
using Apos.Spatial;
using FmodForFoxes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGameGum.GueDeriving;
using roguelitri.Model.Things.Decals.Bullets;
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
    public Level CurrLevel { get; private set; }
    public Camera2D Camera { get; private set; }
    
    private Channel _playing;
    private readonly HashSet<GraphicalUiElement> _uiElements = new ();

    private TextRuntime _debugText;
    
    public readonly AABBTree<Mob> Mobs = new (1024, 0, 1024);
    public readonly HashSet<Bullet> Bullets = new ();
    
    private readonly ICondition _pauseCondition = new KeyboardCondition(Keys.P);
    private readonly ICondition _createEnemyCondition = new KeyboardCondition(Keys.E);
    
    public override void Initialize()
    {
        CurrLevel = LevelManager.LoadLevel("Content/data/lvl/test.json");
        CurrLevel.Random = new Random(CurrLevel.Seed);

        _playing = ResourcesManager.Music.IntoTheNight.Play();
        _playing.Volume = Game1.Settings.MusicVolume;
        _playing.Looping = true;
        
        Camera = new Camera2D(Misc.NativeWidth, Misc.NativeHeight);
        Camera.Origin = new Vector2(Misc.NativeWidth / 2f, Misc.NativeHeight / 2f);

        Player = new Player(this, 0);
        Player.Leaf = Mobs.Add(Player.HitBox, Player);

        _debugText = Misc.AddText("", new Vector2(0, 25));
        _uiElements.Add(_debugText);
        
#if DEBUG
#endif

    }

    public override void Update(GameTime gameTime)
    {
        UpdateMobs(gameTime);

        Camera.Position = Player.Position;
        
        foreach (Mob mob in Mobs.ToList())
        {
            mob.Update(gameTime);
            if ((MobIsFarUnimportant(mob) || mob.Dead) && mob is not Things.Decals.Mobs.Player.Player)
            {
                Mobs.Remove(mob.Leaf);
            }
        }
        foreach (Mob mob in Mobs.ToList().Where(mob => mob.Solid))
        {
            Mobs.Update(mob.Leaf, mob.HitBoxMoved);
        }
        foreach (Mob mob in Mobs.Where(mob => mob.Solid))
        {
            CalculateCollisions(mob, gameTime);
        }

        foreach (Bullet bullet in Bullets.ToList())
        {
            bullet.Update(gameTime);
            CalculateBulletCollisions(bullet, gameTime);

            if (MobIsFarUnimportant(bullet) || bullet.Dead)
            {
                Bullets.Remove(bullet);
            }
        }

        
#if DEBUG
        _debugText.Text = $"POS X/Y: {Player.Position.X:0000.00}/{Player.Position.Y:0000.00} MOBS: {Mobs.Count:0000}";
#endif

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(transformMatrix: Camera.TransformationMatrix);
        
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
        
        int nearestX = Misc.NearestMultiple((int) Camera.Position.X, textureX);
        int nearestY = Misc.NearestMultiple((int) Camera.Position.Y, textureY);
        
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
        RectangleF cameraRect = new RectangleF(Camera.Position.X - Misc.NativeWidth / 2f, Camera.Position.Y - Misc.NativeHeight / 2f, Misc.NativeWidth, Misc.NativeHeight);
        foreach (Mob mob in Mobs.Query(cameraRect).OrderByDescending(mob => mob.Depth).ThenBy(mob => mob.Position.Y))
        {
            SpriteEffects flipEffect = mob.FlipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(mob.Texture, mob.Position, new Rectangle(mob.TextureOffsetX, mob.TextureOffsetY, mob.Width, mob.Height), 
                mob.Color, mob.Rotation, Vector2.Zero, mob.Scale, flipEffect, mob.Depth);
#if DEBUG
                //Rectangle pos = Misc.MoveRect(mob.HitBox, mob.Position).ToRectangle();
                //spriteBatch.Draw(ResourcesManager.Rectangle, pos, Color.Red * 0.2f);
#endif
        }

        foreach (Bullet bullet in Bullets)
        {
            spriteBatch.Draw(bullet.Texture, bullet.Position,
                new Rectangle(0, 0, bullet.Texture.Width, bullet.Texture.Height),
                bullet.Color, bullet.Rotation, Vector2.Zero, bullet.Scale, SpriteEffects.None, bullet.Depth);
        }
    }

    private void UpdateMobs(GameTime gameTime)
    {
        if (_pauseCondition.Pressed())
        {
            SceneManager.PushScene(new PauseScene(Misc.GetScreenshot()));
        }
        
        Player.Update(gameTime);
        Mobs.Update(Player.Leaf, Player.HitBoxMoved);

        if (_createEnemyCondition.Pressed() || InputHelper.KeysPressed(Keys.R))
        {
            Random rnd = new Random();
            Enemy enemy = new Enemy(this, "kobold")
            {
                Position = Player.Position + new Vector2(rnd.Next(-400, 400), rnd.Next(-400, 400)),
            };
            enemy.Leaf = Mobs.Add(enemy.HitBoxMoved, enemy);
        }
        
    }

    private bool MobIsFarUnimportant(Mob mob)
    {
        return mob.Position.X > Camera.Position.X + Misc.NativeWidth * 2f 
            || mob.Position.X < Camera.Position.X - Misc.NativeWidth * 2f 
            || mob.Position.Y > Camera.Position.Y + Misc.NativeHeight * 2f 
            || mob.Position.Y < Camera.Position.Y - Misc.NativeHeight * 2f;
    }

    private void CalculateCollisions(Mob mob, GameTime gameTime)
    {
        foreach (Mob otherMob in Mobs.Query(mob.HitBoxMoved).Where(otherMob => otherMob.Solid))
        {
            if (otherMob.Id == mob.Id) continue;
            otherMob.Collide(mob, gameTime);
        }
    }

    private void CalculateBulletCollisions(Bullet bullet, GameTime gameTime)
    {
        foreach (Mob otherMob in Mobs.Query(bullet.HitBoxMoved).Where(otherMob => otherMob.Solid))
        {
            bullet.Collide(otherMob, gameTime);
        }
    }
    
}