using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Iteract : NetworkBehaviour
{
    Interactable i;
    private bool isCollision = false;
    Collider2D collider;

    private void Update()
    {
        Teleport();        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("ENTER");
        isCollision = true;
        this.collider = collider;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("EXIT");
        isCollision = false;
        this.collider = null;
    }

    private void Teleport()
    {
        if (collider != null && isCollision)
        {
            if (!collider.gameObject.TryGetComponent(out i))
            {
                return;
            }
            else
            {
                i.Interaction(this.gameObject.GetComponent<NetworkIdentity>());
            }
        }
    }

}
