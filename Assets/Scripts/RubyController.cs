using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;

    [SerializeField] GameObject projectilePrefab;

    [SerializeField] AudioClip throwingSound;

    public int maxHealth = 5;

    [HideInInspector] public int health { get { return currrentHealth; } }
    int currrentHealth;

    bool isInvincible;
    float invincibleTimer;
    [SerializeField] float timeInvincible;

    Rigidbody2D playerRB;
    Animator animator;
    AudioSource audioSource;

    Vector2 lookDirection = new Vector2(1,0);

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        currrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();

        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0 ) 
            { 
                isInvincible = false; 
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(playerRB.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialogue();
                }
            }
        }

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void FixedUpdate()
    {
        Vector2 position = playerRB.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        playerRB.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currrentHealth = Mathf.Clamp(currrentHealth + amount, 0, maxHealth);

        UIHealthBar.Instance.SetValue(currrentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, playerRB.position + Vector2.up * 0.5f,Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        audioSource.PlayOneShot(throwingSound);
    }
}
