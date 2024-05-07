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
            }
            Width = monster.Width;
            Height = monster.Height;
            Frames = monster.Frames;
            HasSide = monster.HasSide;
            HasBack = monster.HasBack;
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
            Monster monster = new Monster
            {
                Name = parts[0],
                Texture = parts[1],
                Frames = int.Parse(parts[2], CultureInfo.InvariantCulture),
                Width = int.Parse(parts[3], CultureInfo.InvariantCulture),
                Height = int.Parse(parts[4], CultureInfo.InvariantCulture),
                HasSide = parts[5] == "1",
                HasBack = parts[6] == "1",
                Depth = float.Parse(parts[7], CultureInfo.InvariantCulture),
            };
            Monsters.Add(monster.Name, monster);
        }
    }

    private struct Monster
    {
        // ;name,texture,frames,width,height,hasSide,hasBack,depth
        public string Name;
        public string Texture;
        public int Frames;
        public int Width;
        public int Height;
        public bool HasSide;
        public bool HasBack;
        public float Depth;
    }
    
}