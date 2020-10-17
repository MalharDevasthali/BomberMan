using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Transform spawnPoint;
    private Rigidbody2D rigidbody;
    private PlayerController controller;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = spawnPoint.position;
    }
    private void Update()
    {

        controller.Movement();

    }
    public void SetController(PlayerController _controller)
    {
        controller = _controller;
    }
    public Rigidbody2D GetRigidBody()
    {
        return rigidbody;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            controller.PlayerDied();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            //controller.PlayerDied();
        }
    }

}
