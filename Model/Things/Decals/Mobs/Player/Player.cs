using roguelitri.Service;

namespace roguelitri.Model.Things.Decals.Mobs.Player;

public class Player : Mob
{
    public int PlayerIndex;
    
    public Player(int playerIndex)
    {
        Speed = 0.3f;
        Texture = ResourceManager.Gfx.Player;
        PlayerIndex = playerIndex;
        CollisionFactor = 0.5f;
    }
}