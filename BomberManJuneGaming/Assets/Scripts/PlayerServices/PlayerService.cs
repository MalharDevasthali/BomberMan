using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    public static PlayerService instace;
    private PlayerController controller;
    private PlayerModel model;
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    private void Awake()
    {
        if (instace != null)
        {
            Destroy(this.gameObject);
        }
        {
            instace = this;
        }
    }

    void Start()
    {
        CreatePlayer();
    }
    private void CreatePlayer()
    {
        model = new PlayerModel(playerScriptableObject);
        controller = new PlayerController(playerScriptableObject.playerView, model);
    }
}
