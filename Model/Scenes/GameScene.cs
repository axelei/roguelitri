using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using roguelitri.Service;

namespace roguelitri.Model.Scenes;

public class GameScene : Scene
{
    public override void Initialize()
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

        spriteBatch.DrawString(Resources.Fonts.Arcade, "SCORE: 0", new Vector2(10, 10), Color.White);
        
        spriteBatch.End();
    }

    public override void Dispose()
    {
    }
}