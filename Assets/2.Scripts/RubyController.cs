using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    #region public
    [HideInInspector]
    public const int GOAL_FIXED_ROBOT = 3;
    [HideInInspector]
    public int health { get { return currentHealth; } }
    [HideInInspector]
    public int maxHealth = 5;
    //[HideInInspector]
    //public int FixedRobotCount { get { return fixedRobot; } set { fixedRobot = value; } }

    public AudioClip[] audioclip;
    public AudioSource audioSource;
    public GameObject projectilePrefab;
    public GameObject hitEffect;
    public float timeInvincible = 2.0f;
    public float speed = 23.0f;
    #endregion

    #region private

    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigi2D;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    
    int fixedRobot;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float atktime = Time.deltaTime;

        #region 움직이는 스크립팅(애니메이션 포함)
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(hori, vert);
        if (!Mathf.Approximately(move.x, 0.0f) ||
            !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigi2D.position;
        position += speed * move * Time.deltaTime;
        
        rigi2D.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        #endregion
        #region 공격
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        #endregion

        NPCCheck();
    }

    #region 퀘스트관련
    public void TellMeFixed()
    {
        fixedRobot++;
        
    }

    public bool IsQuestComplete()
    {
        bool val = false;
        if (fixedRobot == GOAL_FIXED_ROBOT)
        {
            val = true;
            PlaySound(audioclip[3]);
        }
        return val;
    }
    #endregion

    #region 오디오
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    #endregion

    #region NPC Check
    void NPCCheck()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                rigi2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter jambi = hit.collider.GetComponent<NonPlayerCharacter>();
                if (jambi != null)
                {
                    if (IsQuestComplete())
                    {
                        jambi.ChangeDialog();
                    }
                    jambi.DisplayDialog();
                }
            }
        }
    }
    #endregion

    #region HP
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            PlaySound(audioclip[0]);
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Instantiate(hitEffect, rigi2D.position + Vector2.up * 0.5f, Quaternion.identity);
        }        

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }
    #endregion

    #region 공격
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab,
            rigi2D.position + Vector2.up * 0.5f,
            Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(audioclip[2]);
    }
    #endregion

}
