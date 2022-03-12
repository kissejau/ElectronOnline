using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Teleport : Interactable
{
    [SerializeField]
    private NetworkManager networkManager;
    [SerializeField]
    private string _sceneName;
    public override void Interaction(NetworkIdentity networkIdentity)
    {
        ChangeSceneOnServer(networkIdentity);            
        Debug.Log("First!");
    }

    [Command(requiresAuthority = false)]
    private void ChangeSceneOnServer(NetworkIdentity networkIdentity)
    {        
        networkManager.ServerChangeScene(_sceneName);
        ChangeSceneOnClients(networkIdentity);
    }

    [ClientRpc]
    private void ChangeSceneOnClients(NetworkIdentity networkIdentity)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
    }

}
