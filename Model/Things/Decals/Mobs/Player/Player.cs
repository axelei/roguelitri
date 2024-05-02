using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Service;

namespace roguelitri.Model.Things.Decals.Mobs.Player;

public class Player : Mob
{
    public int PlayerIndex;
    public float Defense = 0f;
    public float InvulnerabilityTime;
    public bool Invulnerable => InvulnerabilityTime > 0;
    
    public Player(int playerIndex) : this()
    {
        PlayerIndex = playerIndex;
    }
    
    public Player() : base()
    {
        Important = true;
        Speed = 0.3f;
        Texture = ResourcesManager.Gfx.Player;
        CollisionFactor = 0.3f;
        CalculateHitBox();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (InvulnerabilityTime > 0)
        {
            InvulnerabilityTime -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

    public override void Collide(Mob mob, GameTime gameTime)
    {
        base.Collide(mob, gameTime);
        if (!Invulnerable && mob is Enemy enemy)
        {
            Health -= Math.Max(enemy.Attack - Defense, 0.01f);
            InvulnerabilityTime += 1;
            ResourcesManager.Sfx.PlayerHit.Play();
        }
    }
}