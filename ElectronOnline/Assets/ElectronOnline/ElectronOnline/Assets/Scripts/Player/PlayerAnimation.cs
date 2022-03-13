using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PlayerAnimation : NetworkBehaviour
{

    public PlayerMovement pm;
    private Animator animator;
    private SpriteRenderer sr;

    private string _currentAnimation;


    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChangeDirection();
        ChangeAnimation();
    }

    private void ChangeDirection()
    {
        if (isLocalPlayer)
        {
            if (pm.vertInp < 0)
            {
                sr.flipX = true;
            }
            else if (pm.vertInp > 0)
            {
                sr.flipX = false;
            }
            SetRotationOnServer(sr.flipX, this.gameObject);
        }
    }

    private void ChangeAnimation()
    {
        if (isLocalPlayer && !isServer)
        {            
            if (pm.GetComponent<Rigidbody2D>().velocity.x != 0 || pm.GetComponent<Rigidbody2D>().velocity.y != 0)
            {
                _currentAnimation = "BluePlayerRun";
            }
            else
            {
                _currentAnimation = "BluePlayerAnim";
            }
            SetAnimationOnServer(_currentAnimation, this.gameObject);
        }
        else if (isServer)
        {            
            if (pm.GetComponent<Rigidbody2D>().velocity.x != 0 || pm.GetComponent<Rigidbody2D>().velocity.y != 0)
            {
                _currentAnimation = "RedPlayerRun";
            }
            else
            {
                _currentAnimation = "RedPlayerAnim";
            }
            SetAnimationOnServer(_currentAnimation, this.gameObject);
        }
    }


    [Command(requiresAuthority = false)]
    private void SetRotationOnServer(bool flip, GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = flip;
        SetRotationOnClients(flip, gameObject.GetComponent<NetworkIdentity>());
    }

    [ClientRpc]
    private void SetRotationOnClients(bool flip, NetworkIdentity netID)
    {
        netID.gameObject.GetComponent<SpriteRenderer>().flipX = flip;
    }

    [Command]
    private void SetAnimationOnServer(string currentAnimation, GameObject gameObject)
    {        
        gameObject.GetComponent<Animator>().Play(currentAnimation);
        SetAnimationOnClients(currentAnimation, gameObject.GetComponent<NetworkIdentity>());
    }

    [ClientRpc]
    private void SetAnimationOnClients(string currentAnimation, NetworkIdentity netID)
    {
        netID.gameObject.GetComponent<Animator>().Play(currentAnimation);
    }

}
