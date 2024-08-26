using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();
        if (ruby != null)
        {
            if (ruby.health < ruby.maxHealth)
            {
                ruby.ChangeHealth(1);
                Destroy(gameObject);

                ruby.PlaySound(collectedClip);
            }
        }
    }
}
