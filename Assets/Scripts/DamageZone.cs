using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }
}
