using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player/NewPlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{


    public float movementSpeed;
    public float bombDropRate;
    public PlayerView playerView;
}
