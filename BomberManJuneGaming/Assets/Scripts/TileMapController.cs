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
    public Tile boundaryTile;
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private EnemyController enemyController;
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
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        gameplayTileMap.ClearAllTiles();
        GenerateBoundaries();
        GenerateRandomWalls();
        GenerateRandomDestructiveWalls();
        GenerateRandomEnemies();
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
    private void GenerateBoundaries()
    {
        Vector3 _minXMaxY = minXMaxY.position + new Vector3(-1, +1, 1);
        Vector3 _maxXminY = maxXMinY.position + new Vector3(+1, -1, 1);

        for (int i = (int)Mathf.Round(_minXMaxY.x) - 1; i <= Mathf.Round(_maxXminY.x); i++)
        {
            Vector3 tilepos = gameplayTileMap.GetCellCenterLocal(new Vector3Int(i, (int)Mathf.Round(_minXMaxY.y), 0));

            gameplayTileMap.SetTile(new Vector3Int((int)tilepos.x, (int)tilepos.y, 0), boundaryTile);
        }
        for (int i = (int)Mathf.Round(_minXMaxY.x); i <= Mathf.Round(_maxXminY.x); i++)
        {
            Vector3 tilepos = gameplayTileMap.GetCellCenterLocal(new Vector3Int(i, (int)Mathf.Round(_maxXminY.y) - 1, 0));

            gameplayTileMap.SetTile(new Vector3Int((int)tilepos.x, (int)tilepos.y, 0), boundaryTile);
        }



        for (int i = (int)Mathf.Round(_minXMaxY.y); i >= Mathf.Round(_maxXminY.y) - 1; i--)
        {
            Vector3 tilepos = gameplayTileMap.GetCellCenterLocal(new Vector3Int((int)Mathf.Round(_minXMaxY.x) - 1, i, 0));

            gameplayTileMap.SetTile(new Vector3Int((int)tilepos.x, (int)tilepos.y, 0), boundaryTile);
        }
        for (int i = (int)Mathf.Round(_minXMaxY.y); i >= Mathf.Round(_maxXminY.y) - 1; i--)
        {
            Vector3 tilepos = gameplayTileMap.GetCellCenterLocal(new Vector3Int((int)Mathf.Round(_maxXminY.x), i, 0));

            gameplayTileMap.SetTile(new Vector3Int((int)tilepos.x, (int)tilepos.y, 0), boundaryTile);
        }

    }
    private void GenerateRandomWalls()
    {
        int minX = (int)minXMaxY.localPosition.x;
        int maxX = (int)maxXMinY.localPosition.x;

        int minY = (int)maxXMinY.localPosition.y;
        int maxY = (int)minXMaxY.localPosition.y;

        for (int i = 0; i < 20; i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            if (!gameplayTileMap.HasTile(randomPosition))
                gameplayTileMap.SetTile(randomPosition, wallTile);
            else
                i--;
        }
    }
    private void GenerateRandomDestructiveWalls()
    {
        int minX = (int)minXMaxY.localPosition.x;
        int maxX = (int)maxXMinY.localPosition.x;

        int minY = (int)maxXMinY.localPosition.y;
        int maxY = (int)minXMaxY.localPosition.y;

        for (int i = 0; i < 15; i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            if (!gameplayTileMap.HasTile(randomPosition))
                gameplayTileMap.SetTile(randomPosition, destructableTile);
            else
                i--;
        }
    }
    private void GenerateRandomEnemies()
    {
        int minX = (int)minXMaxY.localPosition.x;
        int maxX = (int)maxXMinY.localPosition.x;

        int minY = (int)maxXMinY.localPosition.y;
        int maxY = (int)minXMaxY.localPosition.y;

        for (int i = 0; i < 5; i++)
        {
            Vector3Int randomPosition = new Vector3Int(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            if (!gameplayTileMap.HasTile(randomPosition))
            {
                //SpawnEnemy
                Vector3 spwanPos = gameplayTileMap.GetCellCenterLocal(randomPosition);
                Instantiate(enemyController.gameObject, spwanPos, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
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
