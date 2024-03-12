using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D playerRB;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
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
        position.x = position.x + 3f * horizontal * Time.deltaTime;
        position.y = position.y + 3f * vertical * Time.deltaTime;
        playerRB.MovePosition(position);
    }
}
