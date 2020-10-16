using UnityEngine.Tilemaps;
public class PlayerModel
{
    public float movementSpeed { get; private set; }
    public float bombDropRate { get; private set; }

    // public Tilemap groundTileMap { get; private set; }
    public PlayerModel(PlayerScriptableObject playerSO)
    {
        movementSpeed = playerSO.movementSpeed;
        bombDropRate = playerSO.bombDropRate;
        // groundTileMap = playerSO.groundTileMap;
    }

}