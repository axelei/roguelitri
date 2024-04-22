using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Model.Scenes;

namespace roguelitri.Service;

public static class SceneManager
{

    private static Stack<Scene> _sceneStack = new Stack<Scene>();

    public static void SetScene(Scene scene)
    {
        while (_sceneStack.Any())
        {
            Scene pop = _sceneStack.Pop();
            pop.Dispose();
        }
        
        PushScene(scene);
    }

    public static void PushScene(Scene scene)
    {
        scene.Initialize();
        _sceneStack.Push(scene);
    }

    public static void PopScene(Scene scene)
    {
        Scene pop = _sceneStack.Pop();
        pop.Dispose();
    }

    public static void Update(GameTime gameTime)
    {
        _sceneStack.Peek().Update(gameTime);
    }

    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _sceneStack.Peek().Draw(gameTime, spriteBatch);
    }
}