using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;

    public int maxHealth = 5;

    [HideInInspector] public int health { get { return currrentHealth; } }
    int currrentHealth;

    Rigidbody2D playerRB;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        currrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
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
        currrentHealth = Mathf.Clamp(currrentHealth + amount, 0, maxHealth);
        Debug.Log(currrentHealth + "/" + maxHealth);
    }
}
