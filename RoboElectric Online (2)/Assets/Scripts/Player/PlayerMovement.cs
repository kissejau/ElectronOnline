using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class PlayerMovement : NetworkBehaviour
{

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public float horInp;
    public float vertInp;
    

    private Vector2 _movement;
    private float _speed = 5f;

    public bool Paused;

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

        if (this.isLocalPlayer && !Paused)
        {
            Movement();
        }

    }

    private void Movement()
    {
        vertInp = Input.GetAxisRaw("Horizontal");
        horInp = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(vertInp * _speed, horInp * _speed);
        rb.velocity = _movement;
    }

    public void Pause()
    {
        Paused = true;
        rb.velocity = Vector2.zero;
    }

    public void UnPause()
    {
        Paused = false;
    }
}
