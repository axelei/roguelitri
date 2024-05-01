using Microsoft.Xna.Framework;
using roguelitri.Model.Things.Decals.Mobs.Ia;

namespace roguelitri.Model.Things.Decals.Mobs;

public class Mob : Decal
{
    public float Health = 100;
    public float Speed = 0.2f;
    protected IA Ia = new NoIa();
    
    public virtual void Update(GameTime gameTime)
    {
    }
    
}