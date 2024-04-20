using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri.Model.Scenes;

public abstract class Scene
{
    public abstract void Initialize();

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    public virtual void Dispose()
    {
    }

}