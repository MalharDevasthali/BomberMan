using UnityEngine.Tilemaps;
public class PlayerModel
{
    public float movementSpeed { get; private set; }

    public PlayerModel(PlayerScriptableObject playerSO)
    {
        movementSpeed = playerSO.movementSpeed;

    }

}