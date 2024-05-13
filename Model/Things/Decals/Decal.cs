using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using roguelitri.Util;

namespace roguelitri.Model.Things.Decals;

public class Decal : Thing
{
    
    private const float HitBoxFactor = 0.9f;
    private const float HeightHitBoxFactor = 0.65f;

    public RectangleF HitBox;
    public RectangleF HitBoxMoved => Misc.MoveRect(HitBox, Position);
    
    public bool Solid;
    public float Depth = 0;
    public bool Important;
    
    public bool FlipX;
    public int Width;
    public int Height;
    public int Frames = 1;
    public int Front, Side, Back = -1;
    public int TextureOffsetX, TextureOffsetY;
    public double FaceDirection = Math.PI / 2;

    private int _spawnTime = 0;
    
    public Decal() : base()
    {
        Width = Texture.Width;
        Height = Texture.Height;
        CalculateHitBox();
    }

    public virtual void Update(GameTime gameTime)
    {
        if (_spawnTime == 0)
        {
            _spawnTime = gameTime.TotalGameTime.Milliseconds % 1000;
        }
        if (Frames > 1)
        {
            int frame = (_spawnTime + gameTime.TotalGameTime.Milliseconds) / 100 % Frames;
            TextureOffsetX = Width * frame - 1;
        }
        CalculateOffsets();
    }

    protected void CalculateOffsets()
    {
        // West
        if (Side > -1 && (FaceDirection >= -3 * Math.PI / 4 && FaceDirection > 3 * Math.PI / 4))
        {
            TextureOffsetY = Side * Height;
        }
        // East
        else if (Side > -1 && (FaceDirection >= -Math.PI / 4 && FaceDirection < Math.PI / 4))
        {
            TextureOffsetY = Side * Height;
        }
        // South
        else if (Front > -1 && (FaceDirection >= Math.PI / 4 && FaceDirection < 3 * Math.PI / 4))
        {
            TextureOffsetY = Front * Height;
        }
        // North
        else if (Back > -1 && (FaceDirection >= -3 * Math.PI / 4 && FaceDirection < -Math.PI / 4))
        {
            TextureOffsetY = Back * Height;
        }
        // Default
        else
        {
            TextureOffsetY = 0;
        }
        FlipX = (FaceDirection < -Math.PI / 2 || FaceDirection > Math.PI / 2); 
        
    }

    protected void CalculateHitBox()
    {
        float collisionBoxHeight = Height * HitBoxFactor * HeightHitBoxFactor;
        float collisionBoxWidth = Width * HitBoxFactor;
        float collisionBoxStartTop = Height - collisionBoxHeight;
        float collisionBoxStartLeft = (Width - collisionBoxWidth) / 2;
        HitBox = new RectangleF(collisionBoxStartLeft, collisionBoxStartTop, collisionBoxWidth, collisionBoxHeight);
    }
}