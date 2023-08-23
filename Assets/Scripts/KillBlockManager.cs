using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBlockController : MonoBehaviour
{   

private GameObject[] RespawnPoints;
private GameObject RespawnPoint;
private Rigidbody2D rb;

void Awake()
{
    RespawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
    RespawnPoint = RespawnPoints[0];

}
void OnCollisionEnter2D(Collision2D other)
{
    if(other.gameObject.CompareTag("Player"))
    {
        rb = other.gameObject.GetComponent<Rigidbody2D>();
        other.gameObject.transform.position = RespawnPoint.transform.position + new Vector3(0, Vector2.up.y + 0.5f, 0);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

}
}
