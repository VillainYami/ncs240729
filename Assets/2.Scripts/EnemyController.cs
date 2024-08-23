using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region public
    public ParticleSystem pts;

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public GameObject fixedEffect;
    #endregion

    #region private
    Rigidbody2D rigi2D;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
        broken = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigi2D.position;
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            
        }

        rigi2D.MovePosition(position);
    }

    //루비에게 닿을경우 데미지 입히는 코드
    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = 
            collision.gameObject.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }

    //로봇 고쳐지는 애니메이션
    public void Fix()
    {
        broken = false;
        rigi2D.simulated = false;
        Instantiate(fixedEffect, rigi2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //pts.Stop();
        Destroy(pts.gameObject);
        animator.SetTrigger("Fixed");
    }
}
