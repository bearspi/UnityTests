using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
public float BoosterVelocity;
private Rigidbody2D rb;

void OnTriggerEnter2D(Collider2D other)
{
    if(other.gameObject.CompareTag("Player"))
    {
        rb = other.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, BoosterVelocity);
    }
}

}
