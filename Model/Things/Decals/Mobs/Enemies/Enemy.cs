using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals.Mobs.Enemies;

public class Enemy : Mob
{
    
    private GameScene _gameScene;

    private static readonly Dictionary<string, Monster> Monsters = new();

    public Enemy(GameScene gameScene, string name = "") : base(gameScene)
    {

        if (name != "")
        {
            Monster monster = Monsters[name];
            switch (monster.Texture)
            {
                case "bat": Texture = ResourcesManager.Gfx.Enemies.Bat; break;
                case "cacodemon": Texture = ResourcesManager.Gfx.Enemies.Cacodemon; break;
            }
            Width = monster.Width;
            Height = monster.Height;
            Frames = monster.Frames;
            Front = monster.Front;
            Side = monster.Side;
            Back = monster.Back;
            Depth = monster.Depth;
            CalculateHitBox();
        }

        if (Misc.Random.Next(0, 3) == 0)
        {
            Ia = new ShootingBasicIa(gameScene, this);
        }
        else
        {
            Ia = new BasicIa(gameScene);
        }
        _gameScene = gameScene;
    }
    
    public override void Update(GameTime gameTime)
    {
        Ia.Update(gameTime);

        Vector2 movementVector = Ia.MovementVector(Position);
        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Vector2 oldPosition = Position;
            Position += movementVector * Speed * gameTime.ElapsedGameTime.Milliseconds;
            FaceDirection = Misc.Angle(Position, oldPosition) - Math.PI / 2;
        }
        
        base.Update(gameTime);
    }

    public static void LoadEnemies()
    {
        const int bufferSize = 128;
        using var fileStream = File.OpenRead("Content/data/monsters.csv");
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);
        while (streamReader.ReadLine() is { } line)
        {
            if (line.ToCharArray()[0] == ';') { continue; }
            string[] parts = line.Split(',');
            int i = 0;
            Monster monster = new Monster
            {
                Name = parts[i++],
                Texture = parts[i++],
                Frames = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Width = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Height = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Front = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Side = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Back = int.Parse(parts[i++], CultureInfo.InvariantCulture),
                Depth = float.Parse(parts[i++], CultureInfo.InvariantCulture),
            };
            Monsters.Add(monster.Name, monster);
        }
    }

    private class Monster
    {
        // ;name,texture,frames,width,height,hasSide,hasBack,depth
        public string Name;
        public string Texture;
        public int Frames;
        public int Width, Height;
        public int Front, Side, Back = -1;
        public float Depth;
    }
    
}