using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class PlayerMovement : NetworkBehaviour
{

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private float _horInp;
    private float _vertInp;
    private Vector2 _movement;
    private float _speed = 5f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    private void FixedUpdate()
    {
        
        if (this.isLocalPlayer)
        {
            Movement();
        }

    }

    private void Movement()
    {
        _vertInp = Input.GetAxisRaw("Horizontal");
        _horInp = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(_vertInp * _speed, _horInp * _speed);
        rb.velocity = _movement;
    }
}
