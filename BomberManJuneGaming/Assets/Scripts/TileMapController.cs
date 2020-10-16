using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Threading.Tasks;

public class TileMapController : MonoBehaviour
{
    public static TileMapController instance;
    public Tilemap gameplayTileMap;
    public Tile wallTile;
    public Tile destructableTile;
    public GameObject ExplosionPrefab;
    [SerializeField] private Transform minXMaxY;
    [SerializeField] private Transform maxXMinY;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private async void Start()
    {
        await Task.Delay(500);
        GenerateRandomWalls();
        GenerateRandomDestructiveWalls();
    }
    public void Explode(Vector2 positionToExplode)
    {
        Vector3Int originalCell = gameplayTileMap.WorldToCell(positionToExplode);

        ExplodeCell(originalCell);

        for (int i = 1; i <= BombSpawnner.instance.bomb.bombStrength; i++)
        {
            if (ExplodeCell(originalCell + new Vector3Int(i - 1, 0, 0)))
                ExplodeCell(originalCell + new Vector3Int(i, 0, 0));
            else break;
        }
        for (int i = 1; i <= BombSpawnner.instance.bomb.bombStrength; i++)
        {
            if (ExplodeCell(originalCell + new Vector3Int(0, i - 1, 0)))
                ExplodeCell(originalCell + new Vector3Int(0, i, 0));
            else break;
        }
        for (int i = 1; i <= BombSpawnner.instance.bomb.bombStrength; i++)
        {
            if (ExplodeCell(originalCell + new Vector3Int(-i + 1, 0, 0)))
                ExplodeCell(originalCell + new Vector3Int(-i, 0, 0));
            else break;
        }
        for (int i = 1; i <= BombSpawnner.instance.bomb.bombStrength; i++)
        {
            if (ExplodeCell(originalCell + new Vector3Int(0, -i + 1, 0)))
                ExplodeCell(originalCell + new Vector3Int(0, -i, 0));
            else break;
        }
    }
    private void GenerateRandomWalls()
    {
        int minX = (int)minXMaxY.localPosition.x;
        int maxX = (int)maxXMinY.localPosition.x;

        int minY = (int)maxXMinY.localPosition.y;
        int maxY = (int)minXMaxY.localPosition.y;

        for (int i = 0; i < Random.Range(10, 15); i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            if (!isTileOnPlayer(randomPosition))
                gameplayTileMap.SetTile(randomPosition, wallTile);
        }

    }
    private void GenerateRandomDestructiveWalls()
    {
        int minX = (int)minXMaxY.localPosition.x;
        int maxX = (int)maxXMinY.localPosition.x;

        int minY = (int)maxXMinY.localPosition.y;
        int maxY = (int)minXMaxY.localPosition.y;

        for (int i = 0; i < Random.Range(10, 15); i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            if (!isTileOnPlayer(randomPosition))
                gameplayTileMap.SetTile(randomPosition, destructableTile);
        }
    }

    private bool isTileOnPlayer(Vector3Int tilePos)
    {
        if (tilePos == PlayerService.instace.GetPlayerController().GetPlayerView().spawnPoint.localPosition)
        {
            return true;
        }
        else
            return false;
    }

    private bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = gameplayTileMap.GetTile<Tile>(cell);
        if (tile == wallTile)
        {
            return false;
        }
        else if (tile == destructableTile)
        {
            gameplayTileMap.SetTile(cell, null);
        }
        Vector3 pos = gameplayTileMap.GetCellCenterWorld(cell);
        Instantiate(ExplosionPrefab, pos, Quaternion.identity);
        return true;
    }
}
