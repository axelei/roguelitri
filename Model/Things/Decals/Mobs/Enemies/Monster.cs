using Microsoft.Xna.Framework;

namespace roguelitri.Model.Things.Decals.Mobs.Enemies;

public class Monster
{
    public string Name;
    public string Texture;
    public int Frames;
    public int Width, Height;
    public int Front, Side, Back = -1;
    public float Depth;
    public string Ia = "basic";
    public int Boss;
    public int Health = 100;
    public float Speed = 0.1f;
    public float Attack = 1;
    public Color Color = Color.White;
    public float Size = 1f;
}