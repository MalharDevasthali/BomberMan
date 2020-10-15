
using UnityEngine;

public class PlayerController
{
    private PlayerView view;
    private PlayerModel model;
    public PlayerController(PlayerView _view, PlayerModel _model)
    {
        view = GameObject.Instantiate<PlayerView>(_view);
        model = _model;
        view.SetController(this);
    }
    public void Movement()
    {
        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        if (HorizontalMovement > 0)
        {
            view.GetRigidBody().velocity = new Vector2(model.movementSpeed, 0);
            Debug.Log("Moving Right");
        }
        else if (HorizontalMovement < 0)
        {
            view.GetRigidBody().velocity = new Vector2(-model.movementSpeed, 0);
            Debug.Log("Moving Left");
        }
        else if (VerticalMovement > 0)
        {
            view.GetRigidBody().velocity = new Vector2(0f, model.movementSpeed);
            Debug.Log("Up");
        }
        else if (VerticalMovement < 0)
        {
            view.GetRigidBody().velocity = new Vector2(0f, -model.movementSpeed);
            Debug.Log("Down");
        }
        else
            view.GetRigidBody().velocity = Vector2.zero;
    }

}