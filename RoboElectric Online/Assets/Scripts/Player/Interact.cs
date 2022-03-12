using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interact : NetworkBehaviour
{
    private Interactable i;
    private bool isCollision = false;
    private Collider2D collider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
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

    private void Interaction()
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
                print("Inter");
            }
        }
    }

}
