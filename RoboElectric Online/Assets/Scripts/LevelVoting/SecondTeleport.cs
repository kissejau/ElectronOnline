using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondTeleport : Interactable
{

    public override void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("SecondLevel");
            Debug.Log("Second!");
        }
    }

}
