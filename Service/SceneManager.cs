using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Model.Scenes;

namespace roguelitri.Service;

public static class SceneManager
{

    private static Scene _currentScene;

    public static void SetScene(Scene scene)
    {
        _currentScene?.Dispose();
        _currentScene = scene;
        _currentScene.Initialize();
    }

    public static void Update(GameTime gameTime)
    {
        _currentScene.Update(gameTime);
    }

    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _currentScene.Draw(gameTime, spriteBatch);
    }
}