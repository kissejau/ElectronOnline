using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScoreBoard : NetworkBehaviour
{
    [SerializeField]
    private ShowScoreBoard shower;
    public Dictionary<string, int> Board = new Dictionary<string, int>();
    int i = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {            
            foreach (KeyValuePair<string, int> pair in Board)
            {
                print(this.gameObject.name);
                print($"{pair.Key} {pair.Value}");
            }
            shower.Board = Board;
            shower.UpdateScores(Board);
            UpdateScoreBoard(this.gameObject, ++i);
        }
    }

    [Command(requiresAuthority = false)]
    public void UpdateScoreBoard(GameObject player, int score)
    {
        if (!Board.ContainsKey(player.name))
        {
            Board.Add(player.name, score);
        }
        else
        {
            Board[player.name] = score;
        }
        var names = new List<string>();
        var scores = new List<int>();
        foreach (KeyValuePair<string, int> pair in Board)
        {
            names.Add(pair.Key);
            scores.Add(pair.Value);            
        }
        UpdateScoreBoardClients(names, scores);
    }

    [ClientRpc]
    public void UpdateScoreBoardClients(List<string> names, List<int> scores)
    {
        if (!isServer)
        {
            Board = new Dictionary<string, int>();
            for (int i = 0; i < names.Count; i++)
            {
                Board.Add(names[i], scores[i]);
            }
        }
    }
}
