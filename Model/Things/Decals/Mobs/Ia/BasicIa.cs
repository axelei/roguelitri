using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;

namespace roguelitri.Model.Things.Decals.Mobs.Ia;

public class BasicIa : IA
{
    private GameScene _gameScene;
    
    public BasicIa(GameScene gameScene)
    {
        _gameScene = gameScene;
    }
    
    public Vector2 MovementVector(Vector2 currentPosition)
    {
        Vector2 playerPosition = _gameScene.Player.Position;
        double angle = Math.Atan2(playerPosition.Y - currentPosition.Y, playerPosition.X - currentPosition.X);
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }

    public void Update()
    {
        
    }
}