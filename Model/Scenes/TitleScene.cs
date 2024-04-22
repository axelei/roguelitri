using System.Linq;
using Gum.DataTypes;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Scenes;

public class TitleScene : Scene
{
    private GraphicalUiElement _screen;
    
    public override void Initialize()
    {
        _screen = Game1.GumProject.Screens.First().ToGraphicalUiElement(SystemManagers.Default, addToManagers:true);
        
        MusicManager.Play(ResourceManager.Music.TestSong);
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.AnyKeyPressed())
        {
            SceneManager.SetScene(new GameScene());
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);
        
        spriteBatch.Draw(ResourceManager.Gfx.Title.Logo, new Vector2(100, 100), Color.White);

        spriteBatch.End();
    }

    public override void Dispose()
    {
        _screen.RemoveFromManagers();
        MusicManager.Stop();
    }
}