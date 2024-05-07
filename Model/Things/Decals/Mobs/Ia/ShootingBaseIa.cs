using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Bullets;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Ia;

public class ShootingBasicIa : BasicIa
{
    private GameScene _gameScene;
    private Mob _mob;
    
    public ShootingBasicIa(GameScene gameScene, Mob mob) : base(gameScene)
    {
        _gameScene = gameScene;
        _mob = mob;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (Misc.Random.Next(0, (int) (3000 / gameTime.ElapsedGameTime.TotalMilliseconds)) == 0)
        {
            EnemyBullet enemyBullet = new EnemyBullet(_gameScene, _mob.Position);
            _gameScene.Bullets.Add(enemyBullet);
        }
    }

}