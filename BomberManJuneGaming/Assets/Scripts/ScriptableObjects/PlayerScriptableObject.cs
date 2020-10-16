using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player/NewPlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{


    public float movementSpeed;
    public float bombDropRate;
    public PlayerView playerView;
    //public Tilemap groundTileMap;
}
