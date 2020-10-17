using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum Movement
    {
        Right = 0,
        Left,
        Up,
        Down,
    }
    public float movementSpeed;
    public LayerMask Walls;
    private Movement currentMovement = Movement.Right;
    private Rigidbody2D rb2d;
    private float currentTime = 0;
    private float changeDirectionTimer = 3f;
    private static int enemiesDied = 0;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemiesDied = 0;

    }


    void Update()
    {
        if (currentTime < Time.time)
        {
            currentTime = changeDirectionTimer + Time.time;

            currentMovement = (Movement)Random.Range(0, 4);
            Debug.Log("Each 2 secs");
        }

        switch (currentMovement)
        {
            case Movement.Right:
                MoveInRightDirection();
                break;
            case Movement.Left:
                MoveInLeftDirection();
                break;
            case Movement.Up:
                MoveInUptDirection();
                break;
            case Movement.Down:
                MoveInDownDirection();
                break;
        }

    }
    private void MoveInRightDirection()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, Walls);

        if (hitInfo)
        {
            while (currentMovement == Movement.Right)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(movementSpeed, 0f);
        }
    }
    private void MoveInLeftDirection()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, Walls);
        if (hitInfo)
        {
            while (currentMovement == Movement.Left)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(-movementSpeed, 0f);
        }
    }
    private void MoveInUptDirection()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, Walls);
        if (hitInfo)
        {
            while (currentMovement == Movement.Up)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }

        }
        else
        {
            rb2d.velocity = new Vector2(0f, movementSpeed);
        }
    }
    private void MoveInDownDirection()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, Walls);
        if (hitInfo)
        {
            while (currentMovement == Movement.Down)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0f, -movementSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        enemiesDied++;
        UIService.instance.UpdateScore();
        Debug.Log("Eenmies Died" + enemiesDied);
        if (enemiesDied == 2)
        {
            UIService.instance.ShowWinScreen();
        }
    }

}
