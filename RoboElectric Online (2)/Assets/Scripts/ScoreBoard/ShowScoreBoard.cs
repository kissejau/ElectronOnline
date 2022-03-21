using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ShowScoreBoard : NetworkBehaviour
{
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private ScoreBoard _scoreBoard;    
    [SerializeField]
    private List<Text> _names;
    [SerializeField]
    private List<Text> _scores;


    public Dictionary<string, int> Board = new Dictionary<string, int>();

    private bool _showing = false;


    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!_showing)
                    Show();
                else
                    Hide();
            }
            if(_showing)
            {
                //UpdateScores();
            }
        }
    }

    private void Show()
    {
        _canvas.enabled = true;
        //UpdateScores();
        _showing = true;
    }

    private void Hide()
    {
        _showing = false;
        _canvas.enabled = false;
    }

    public void UpdateScores(Dictionary<string, int> board)
    {
        var names = new List<string>();
        var scores = new List<int>();

        foreach(var name in board)
        {
            names.Add(name.Key);
            scores.Add(name.Value);
        }

        for(int i = 0; i < names.Count; i++)
        {
            bool wasSwap = false;
            for(int j = 0; j < names.Count - 1; j++)
            {
                if(scores[j] < scores[j + 1])
                {
                    (scores[j], scores[j + 1]) = (scores[j + 1], scores[j]);
                    (names[j], names[j + 1]) = (names[j + 1], names[j]);
                    wasSwap = true;
                }
            }
            if (!wasSwap)
                break;
        }



        for(int i = 0; i < 10 && i < names.Count; i++)
        {
            _names[i].text = names[i];
            _scores[i].text = scores[i].ToString();
        }
    }    
}
