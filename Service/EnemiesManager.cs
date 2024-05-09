using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using roguelitri.Model.Scenes;
using roguelitri.Model.Things.Decals.Mobs.Enemies;
using roguelitri.Model.Things.Decals.Mobs.Ia;
using roguelitri.Util;

namespace roguelitri.Service;

public static class EnemiesManager
{
    
    private static readonly Dictionary<string, Monster> Monsters = new();
    
    private const string MonstersFile = "Content/data/monsters.csv";
    
    public static void LoadEnemies()
    {
        Logger.Log("Loading enemies...");
        using var streamReader = new StreamReader(MonstersFile, Encoding.UTF8);
        while (streamReader.ReadLine() is { } line)
        {
            if (line.ToCharArray()[0] == ';') { continue; }
            string[] parts = line.Split(',');
            int i = 0;
            Monster monster = new Monster
            {
                Name = parts[i++],
                Texture = parts[i++],
                Frames = ParseInt(parts[i++]),
                Width = ParseInt(parts[i++]),
                Height = ParseInt(parts[i++]),
                Front = ParseInt(parts[i++]),
                Side = ParseInt(parts[i++]),
                Back = ParseInt(parts[i++]),
                Depth = ParseFloat(parts[i++]),
                Ia = parts[i++],
                Boss = ParseInt(parts[i++]),
                Health = ParseInt(parts[i++]),
                Speed = ParseFloat(parts[i++]),
                Attack = ParseFloat(parts[i++]),
                Color = GetColor(parts[i++]),
                Size = ParseFloat(parts[i++])
            };
            Monsters.Add(monster.Name, monster);
        }
    }
    
    private static int ParseInt(string input) => int.Parse(input, CultureInfo.InvariantCulture);
    private static float ParseFloat(string input) => float.Parse(input, CultureInfo.InvariantCulture);

    public static void SetEnemy(string name, GameScene gameScene, Enemy enemy)
    {
        Monster monster = Monsters[name];
        switch (monster.Texture)
        {
            case "bat": enemy.Texture = ResourcesManager.Gfx.Enemies.Bat; break;
            case "cacodemon": enemy.Texture = ResourcesManager.Gfx.Enemies.Cacodemon; break;
            case "kobold": enemy.Texture = ResourcesManager.Gfx.Enemies.Kobold; break;
        }
        enemy.Width = monster.Width;
        enemy.Height = monster.Height;
        enemy.Frames = monster.Frames;
        enemy.Front = monster.Front;
        enemy.Side = monster.Side;
        enemy.Back = monster.Back;
        enemy.Depth = monster.Depth;
        switch (monster.Ia)
        {
            case "no": enemy.Ia = new NoIa(); break;
            case "basic": enemy.Ia = new BasicIa(gameScene); break;
            case "shooting": enemy.Ia = new ShootingBasicIa(gameScene, enemy); break;
        }
        enemy.Important = monster.Boss == 1;
        enemy.Health = monster.Health;
        enemy.Speed = monster.Speed;
        enemy.Attack = monster.Attack;
        enemy.Color = monster.Color;
        enemy.Scale = new Vector2(monster.Size);
    }
    
    private static Color GetColor(string color)
    {
        return new Color(
            int.Parse(color.Substring(0, 2), NumberStyles.HexNumber),
            int.Parse(color.Substring(2, 2), NumberStyles.HexNumber),
            int.Parse(color.Substring(4, 2), NumberStyles.HexNumber),
            int.Parse(color.Substring(6, 2), NumberStyles.HexNumber)
        );
    }
    
}