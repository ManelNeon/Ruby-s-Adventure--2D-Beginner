using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    [SerializeField] float changeTime = 3.0f;

    [SerializeField] bool vertical;

    [SerializeField] ParticleSystem smokeEffect;

    Rigidbody2D enemyRB;

    Animator animator;

    float timer;

    int direction = 1;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;

            timer = changeTime;
        }
        if (!broken)
        {
            return;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = enemyRB.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else 
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move Y", 0);
            animator.SetFloat("Move X", direction);
        }

        enemyRB.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
        broken = false;
        enemyRB.simulated = false;
    }
}
