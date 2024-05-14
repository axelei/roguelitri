using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using RenderingLibrary.Graphics;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Scenes;

public class PauseScene : Scene
{
    private TextRuntime _pauseText;
    private readonly Texture2D _background;

    private readonly ICondition _unpauseCondition =
        new KeyboardCondition(Keys.P);
    
    public PauseScene(Texture2D background)
    {
        _background = background;
    }

    public override void Initialize()
    {
        _pauseText = Misc.AddText("GAME PAUSED", new Vector2(Misc.NativeWidth / 2f, Misc.NativeHeight / 2f));
        _pauseText.HorizontalAlignment = HorizontalAlignment.Center;
        _pauseText.VerticalAlignment = VerticalAlignment.Center;
        
        MusicManager.Pause();
        
    }

    public override void Update(GameTime gameTime)
    {
        if (_unpauseCondition.Pressed())
        {
            MusicManager.Resume();
            SceneManager.PopScene();
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_background, new Rectangle(0, 0, Misc.NativeWidth, Misc.NativeHeight), Color.White);
        spriteBatch.End();
    }

    public override void Dispose()
    {
        _pauseText.RemoveFromManagers();
    }
}