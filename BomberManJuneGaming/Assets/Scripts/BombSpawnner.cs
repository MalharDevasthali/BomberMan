using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BombSpawnner : MonoBehaviour
{
    public Tilemap groundTilemap;
    public GameObject bomb;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }
    private void DropBomb()
    {
        Vector3 playerPosition = PlayerService.instace.GetPlayerController().GetPlayerView().transform.transform.position;
        Vector3Int cell = groundTilemap.LocalToCell(playerPosition);
        Vector3 cellCentre = groundTilemap.GetCellCenterLocal(cell);
        Debug.Log(playerPosition);
        Debug.Log(cell);
        Debug.Log(cellCentre);
        GameObject.Instantiate(bomb, cellCentre, Quaternion.identity);

    }
}
