using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float bombBlastTimer;
    public float bombStrength;
    private void Update()
    {
        bombBlastTimer -= Time.deltaTime;
        if (bombBlastTimer <= 0.1f)
        {
            TileMapController.instance.Explode(transform.position);
            Destroy(this.gameObject);
        }
    }
}
