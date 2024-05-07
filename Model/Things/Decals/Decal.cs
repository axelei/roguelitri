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
    public bool HasSide, HasBack;
    public int TextureOffsetX, TextureOffsetY;
    public double FaceDirection = Math.PI / 2;
    
    public Decal() : base()
    {
        Width = Texture.Width;
        Height = Texture.Height;
        CalculateHitBox();
    }
    
    public virtual void Update(GameTime gameTime)
    {
        if (Frames > 1)
        {
            int frame = gameTime.TotalGameTime.Milliseconds / 100 % Frames;
            TextureOffsetX = Width * frame - 1;
        }
        CalculateOffsets();
        

    }

    protected void CalculateOffsets()
    {
        // West
        if (HasSide && (FaceDirection >= Math.PI / 4 && FaceDirection < 3 * Math.PI / 4))
        {
            TextureOffsetY = Height;
            FlipX = true;
        }
        // East
        else if (HasSide && (FaceDirection >= -3 * Math.PI / 4 && FaceDirection < -Math.PI / 4))
        {
            TextureOffsetY = Height;
            FlipX = false;
        }
        // South
        else if (FaceDirection >= -Math.PI / 4 && FaceDirection < Math.PI / 4)
        {
            TextureOffsetY = 0;
            FlipX = false;
        }
        // North
        else if (HasBack && (FaceDirection >= 3 * Math.PI / 4 || FaceDirection < -3 * Math.PI / 4))
        {
            TextureOffsetY = Height * 2;
            FlipX = false;
        }
        // Defaults to south
        else
        {
            FlipX = false;
            TextureOffsetY = 0;
        }
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