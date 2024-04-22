using System.Linq;
using Gum.DataTypes;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using roguelitri.Service;
using roguelitri.util;

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
        _screen.RemoveFromManagers();
        MusicManager.Stop();
    }
}