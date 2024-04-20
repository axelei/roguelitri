using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;
using roguelitri.Service;
using roguelitri.util;

namespace roguelitri.Model.Scenes;

public class TitleScene : Scene
{
    public override void Initialize()
    {
        MusicManager.Play(Resources.Music.TestSong);
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.HasBeenPressed(Keys.Space))
        {
            SceneManager.SetScene(new GameScene());
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

        spriteBatch.DrawString(Resources.Fonts.IbmVgaFont, "Roguelitri SNAPSHOT áéíóçñ - àç", new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(Resources.Fonts.Arcade, "COPYRIGHT 2024 ENLOARTOLAMEZA STUDIOS", new Vector2(10, 30), Color.White);
        
        spriteBatch.End();
    }

    public override void Dispose()
    {
        MusicManager.Stop();
    }
}