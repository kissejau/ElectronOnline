using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class FirstTeleport : Interactable
{
    [SerializeField]
    private NetworkManager networkManager;
    public override void Interaction(NetworkIdentity networkIdentity)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSceneOnServer(networkIdentity);
            //SceneManager.LoadScene("FirstLevel");
            Debug.Log("First!");
        }
    }

    [Command(requiresAuthority = false)]
    private void ChangeSceneOnServer(NetworkIdentity gameObject)
    {        
        networkManager.ServerChangeScene("FirstLevel");
        ChangeSceneOnClients(gameObject);
    }

    [ClientRpc]
    private void ChangeSceneOnClients(NetworkIdentity gameObject)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("FirstLevel"));
    }

}
