
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController
{
    private PlayerView view;
    private PlayerModel model;
    //  private Tilemap groundTilemap;
    public PlayerController(PlayerView _view, PlayerModel _model)
    {
        view = GameObject.Instantiate<PlayerView>(_view);
        model = _model;
        view.SetController(this);
        // groundTilemap = model.groundTileMap;
    }
    public PlayerView GetPlayerView()
    {
        return view;
    }
    public void Movement()
    {
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        if (HorizontalMovement > 0)
        {
            view.GetRigidBody().velocity = new Vector2(model.movementSpeed, 0);
        }
        else if (HorizontalMovement < 0)
        {
            view.GetRigidBody().velocity = new Vector2(-model.movementSpeed, 0);
        }
        else if (VerticalMovement > 0)
        {
            view.GetRigidBody().velocity = new Vector2(0f, model.movementSpeed);
        }
        else if (VerticalMovement < 0)
        {
            view.GetRigidBody().velocity = new Vector2(0f, -model.movementSpeed);
        }
        else
            view.GetRigidBody().velocity = Vector2.zero;
    }


}