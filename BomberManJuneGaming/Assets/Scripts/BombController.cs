using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombBlastTimer;
    public float bombStrength;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private void Update()
    {
        bombBlastTimer -= Time.deltaTime;
        if (bombBlastTimer <= 0.1f)
        {
            TileMapController.instance.Explode(transform.position);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerView>())
        {
            boxCollider2D.isTrigger = false;
        }
    }
}
