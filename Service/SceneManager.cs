using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Model.Scenes;

namespace roguelitri.Service;

public static class SceneManager
{

    private static readonly Stack<Scene> SceneStack = new ();

    public static ContentManager Content;

    public static void SetScene(Scene scene)
    {
        while (SceneStack.Any())
        {
            Scene pop = SceneStack.Pop();
            pop.Dispose();
        }
        
        PushScene(scene);
    }

    public static void PushScene(Scene scene)
    {
        scene.Initialize();
        SceneStack.Push(scene);
    }

    public static Scene PopScene()
    {
        Scene pop = SceneStack.Pop();
        pop.Dispose();
        return pop;
    }

    public static void Update(GameTime gameTime)
    {
        SceneStack.Peek().Update(gameTime);
    }

    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        SceneStack.Peek().Draw(gameTime, spriteBatch);
    }
}