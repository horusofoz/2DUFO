using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidBody;
    [SerializeField] float playerSpeed = 10;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rigidBody.AddForce(movement * playerSpeed);
    }
}
