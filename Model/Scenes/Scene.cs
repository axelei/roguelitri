using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri.Model.Scenes;

public abstract class Scene
{
    public abstract void Load(ContentManager content);

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch);

}