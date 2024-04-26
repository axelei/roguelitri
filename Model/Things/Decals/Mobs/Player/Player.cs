using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelitri.Service;

namespace roguelitri.Model.Things.Decals.Mobs.Player;

public class Player : Mob
{
    public int PlayerIndex;
    public Texture2D Texture = ResourceManager.Gfx.Player;
    
    public Player(int playerIndex)
    {
        PlayerIndex = playerIndex;
    }
}