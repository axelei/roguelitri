using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Bullets;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Player;

public class Player : Mob
{
    public int PlayerIndex;
    public float Defense = 0f;
    public float InvulnerabilityTime;
    public bool Invulnerable => InvulnerabilityTime > 0;
    
    public const float InvulnerabilityTimeHit = 1000;
    
    public Player(GameScene gameScene, int playerIndex) : this(gameScene)
    {
        PlayerIndex = playerIndex;
    }
    
    public Player(GameScene gameScene) : base(gameScene)
    {
        Important = true;
        Speed = 0.2f;
        Texture = ResourcesManager.Gfx.Player;
        CollisionFactor = 0.3f;
        CalculateHitBox();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (InvulnerabilityTime > 0)
        {
            InvulnerabilityTime -= gameTime.ElapsedGameTime.Milliseconds;
        }
        
        Vector2 moveVector = Vector2.Zero;

        if (InputHelper.KeysPressed(Keys.Up, Keys.W)) moveVector.Y -= 1;
        if (InputHelper.KeysPressed(Keys.Down, Keys.S)) moveVector.Y += 1;
        if (InputHelper.KeysPressed(Keys.Left, Keys.A)) moveVector.X -= 1;
        if (InputHelper.KeysPressed(Keys.Right, Keys.D)) moveVector.X += 1;

        if (moveVector != Vector2.Zero)
        {
            moveVector.Normalize();
            Position += moveVector * Speed * gameTime.ElapsedGameTime.Milliseconds;
            FaceDirection = Misc.Angle(Vector2.Zero, moveVector);
        }
        
        if (InputHelper.KeysPressed(Keys.F))
        {
            PlayerBullet playerBullet = new PlayerBullet(GameScene, HitBoxMoved.Center);
            GameScene.Bullets.Add(playerBullet);
        }
    }

    public override void Collide(Mob mob, GameTime gameTime)
    {
        base.Collide(mob, gameTime);
        if (!Invulnerable && mob is Enemy enemy)
        {
            Health -= Math.Max(enemy.Attack - Defense, 0.01f);
            InvulnerabilityTime = Math.Max(InvulnerabilityTime, InvulnerabilityTimeHit);
            ResourcesManager.Sfx.PlayerHit.Play();
        }
    }
}