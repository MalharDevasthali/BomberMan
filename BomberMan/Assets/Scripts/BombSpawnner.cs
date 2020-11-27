using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BombSpawnner : MonoBehaviour
{
    public Tilemap groundTilemap;
    public BombController bomb;
    private BombController currentBomb;
    public static BombSpawnner instance;

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

    public bool canPlaceBomb = true;
    private void Update()
    {
        if (currentBomb && currentBomb.bombBlastTimer <= 0.2)
            canPlaceBomb = true;

        if (Input.GetKeyDown(KeyCode.Space) && canPlaceBomb)
        {
            DropBomb();
        }
    }
    private void DropBomb()
    {
        canPlaceBomb = false;
        Vector3 playerPosition = PlayerService.instace.GetPlayerController().GetPlayerView().transform.transform.position;
        Vector3Int cell = groundTilemap.LocalToCell(playerPosition);
        Vector3 cellCentre = groundTilemap.GetCellCenterLocal(cell);
        currentBomb = GameObject.Instantiate(bomb, cellCentre, Quaternion.identity).GetComponent<BombController>();

    }
}
