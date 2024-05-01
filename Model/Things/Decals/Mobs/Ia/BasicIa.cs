using System;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Util;

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
        return Misc.AngleVector(_gameScene.Player.Position, currentPosition);
    }

    public void Update()
    {
        
    }
}