using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using roguelitri.Service;
using roguelitri.util;

namespace roguelitri.Model.Scenes;

public class TitleScene : Scene
{
    public override void Initialize()
    {
        var titleText = new TextRuntime
        {
            UseCustomFont = true,
            CustomFontFile = Resources.Fonts.IbmVgaFont,
            Text = "Roguelitri tech demo - press space to start! - áéíóçñ - àç",
            X = 100,
            Y = 10
        };
        titleText.AddToManagers(SystemManagers.Default, null);
        var copyrightText = new TextRuntime
        {
            UseCustomFont = true,
            CustomFontFile = Resources.Fonts.Arcade,
            Text = "COPYRIGHT 2024 ENLOARTOLAMEZA STUDIOS",
            X = 100,
            Y = 30
        };
        copyrightText.AddToManagers(SystemManagers.Default, null);
        
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

        spriteBatch.End();
    }

    public override void Dispose()
    {
        MusicManager.Stop();
    }
}