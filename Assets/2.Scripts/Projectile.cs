using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigi2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigi2D = GetComponent<Rigidbody2D>();    
    }

    public void Launch(Vector2 direction, float force)
    {
        rigi2D.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController e = collision.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.magnitude > 300.0f)
        {
            Destroy(gameObject);
        }
    }
}
