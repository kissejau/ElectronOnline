using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class Teleport : Interactable
{
    public int Voted;
    [SerializeField]
    private NetworkIdentity _timer;
    [SerializeField]
    private NetworkManager _networkManager;
    [SerializeField]
    private string _sceneName;
    [SerializeField]
    private List<NetworkIdentity> _otherTeleports;

    public bool AlreadyStarting;

    public HashSet<NetworkIdentity> PlayersVoted;

    private void Start()
    {
        Voted = 0;
        AlreadyStarting = false;
        PlayersVoted = new HashSet<NetworkIdentity>();
    }

    public override void Interaction(NetworkIdentity networkIdentity)
    {

        if (PlayersVoted.Add(networkIdentity) && NotAlreadyStarting())
        {
            Started(this.gameObject);
            ChangeSceneAfterTime(5);
        }
        if (PlayersVoted.Add(networkIdentity) && false)
        {
            StopTimerOnServer();
            if (isServer)
            {
                StopTimerOnClients();
            }
            foreach (var teleport in _otherTeleports)
            {
                teleport.GetComponent<Teleport>().PlayersVoted.Add(networkIdentity);
            }
            VotedIncreaseServer(this.netIdentity);
            if (AllPlayersVoted())
            {
                ChooseMostVoted();
            }
            else
            {
                ChangeSceneAfterTime(15);
            }
        }
    }

    private bool NotAlreadyStarting()
    {
        foreach (var teleport in _otherTeleports)
        {
            if (teleport.GetComponent<Teleport>().AlreadyStarting)
                return false;
        }
        return !AlreadyStarting;
    }
    private void ChooseMostVoted()
    {
        print("CHOOSING MOST VOTED");
        NetworkIdentity maxVoted = this.netIdentity;
        List<NetworkIdentity> votes = new List<NetworkIdentity> { maxVoted };
        foreach (var teleport in _otherTeleports)
        {
            if (teleport.GetComponent<Teleport>().Voted > Voted)
            {
                maxVoted = teleport;
                votes.Clear();
            }
            else if (teleport.GetComponent<Teleport>().Voted == Voted)
            {
                votes.Add(teleport);
            }
        }
        StopTimerOnServer();
        if (votes.Count > 0)
        {
            votes[new System.Random().Next(0, votes.Count)].GetComponent<Teleport>().ChangeSceneAfterTime(5);
        }
        else
        {
            maxVoted.GetComponent<Teleport>().ChangeSceneAfterTime(5);
        }
    }

    private bool AllPlayersVoted()
    {
        var sum = Voted;

        foreach (var portal in _otherTeleports)
        {
            var a = portal.GetComponent<Teleport>();
            sum += a.Voted;
            print(sum);
        }

        return sum >= 2;
    }


    [Command(requiresAuthority = false)]
    public void ChangeSceneAfterTime(int t)
    {
        StopTimerOnServer();
        ChangeSceneAfterTimeClient(t);
    }
    [ClientRpc]
    private void ChangeSceneAfterTimeClient(int t)
    {
        StartCoroutine(FuncAfterTime(t));
    }

    private IEnumerator FuncAfterTime(int t)
    {
        var time = _timer.GetComponent<UnityEngine.UI.Text>();
        time.enabled = true;
        for (int i = 0; i <= t; i++)
        {
            time.text = (t - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        ChangeSceneOnServer();
        yield break;
    }

    [Command(requiresAuthority = false)]
    private void StopTimerOnServer()
    {
        StopAllCoroutines();
        StopTimerOnClients();
    }

    [ClientRpc]
    private void StopTimerOnClients()
    {
        StopAllCoroutines();
        var time = _timer.GetComponent<UnityEngine.UI.Text>();
        time.enabled = false;
    }

    [Command(requiresAuthority = false)]
    private void ChangeSceneOnServer()
    {
        _networkManager.ServerChangeScene(_sceneName);
        ChangeSceneOnClients();
    }

    [ClientRpc]
    private void ChangeSceneOnClients()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
    }


    [Command(requiresAuthority = false)]
    private void VotedIncreaseServer(NetworkIdentity networkIdentity)
    {
        networkIdentity.GetComponent<Teleport>().Voted += 1;
        VotedIncreaseClient(networkIdentity, networkIdentity.GetComponent<Teleport>().Voted);
    }

    [ClientRpc]
    private void VotedIncreaseClient(NetworkIdentity networkIdentity, int value)
    {
        networkIdentity.GetComponent<Teleport>().Voted = value;
    }

    [Command(requiresAuthority = false)]
    private void Started(GameObject gameObject)
    {
        gameObject.GetComponent<Teleport>().AlreadyStarting = true;
        StartedClient(gameObject.GetComponent<NetworkIdentity>());
    }
    [ClientRpc]
    private void StartedClient(NetworkIdentity identity)
    {
        print(identity.name);
        print(identity.gameObject.GetComponent<Teleport>().AlreadyStarting);
        identity.gameObject.GetComponent<Teleport>().AlreadyStarting = true;
        print(identity.gameObject.GetComponent<Teleport>().AlreadyStarting);
    }
}