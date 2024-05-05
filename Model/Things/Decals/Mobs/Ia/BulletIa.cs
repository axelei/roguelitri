using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Ia;

public class BulletIa : IA
{
    private double _angle;
    
    public BulletIa(GameScene gameScene, bool isEnemyBullet, Vector2 initialPosition)
    {
        Vector2 target;
        if (isEnemyBullet)
        {
            target = gameScene.Player.Position;
        }
        else
        {
            RectangleF cameraRect = new RectangleF(gameScene.Camera.Position.X - Misc.NativeWidth / 2f, gameScene.Camera.Position.Y - Misc.NativeHeight / 2f, Misc.NativeWidth, Misc.NativeHeight);
            Mob targetMob = gameScene.Mobs.Query(cameraRect).Where(mob => mob is Enemy)
                .MinBy(enemy => Vector2.Distance(enemy.Position, initialPosition));
            if (targetMob == null)
            {
                target = new Vector2((float) -Math.Cos(gameScene.Player.FaceDirection), (float) -Math.Sin(gameScene.Player.FaceDirection)) + initialPosition;
            }
            else
            {
                target = targetMob.HitBoxMoved.Center;
            }
            
        }

        _angle = Misc.Angle(initialPosition, target) - Math.PI / 2;
    }
    
    public Vector2 MovementVector(Vector2 currentPosition)
    {
        return new Vector2((float)Math.Sin(_angle), -(float)Math.Cos(_angle));
    }

    public void Update()
    {
        
    }
}