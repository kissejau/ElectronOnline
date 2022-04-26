using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : Interactable
{
    private int _rightAnswers;
    private int _failedAnswers;
    private int _currentTask;
    private Practice[] _tasks;
    [SerializeField]
    private Text _taskText;
    [SerializeField]
    private Button[] _buttons;
    [SerializeField]
    private Text[] _buttonsTexts;
    [SerializeField]
    private Canvas _canvas;

    private NetworkIdentity _networkIdentity;

    private bool _done;
    private void Awake()
    {
        _rightAnswers = 0;
        _failedAnswers = 0;
        _currentTask = 0;
        _done = false;
        GetPractices();
    }
    public override void Interaction(NetworkIdentity gameObject)
    {
        if (gameObject.isLocalPlayer && !_done)
        {            

            _canvas.enabled = true;
            gameObject.gameObject.GetComponent<PlayerMovement>().Pause();
            _done = true;
            _networkIdentity = gameObject;
            ShowTest();
        }
    }

    public void ShowTest()
    {
        _taskText.text = _tasks[_currentTask].question;
        for (int i = 0; i < _tasks[_currentTask].answers.Count; i++)
        {
            _buttons[i].gameObject.SetActive(true);
            _buttonsTexts[i].text = _tasks[_currentTask].answers[i];
        }
    }

    public void SendAnswer(int buttonIndex)
    {
        string answer = _buttonsTexts[buttonIndex].text;
        if (answer == _tasks[_currentTask].rightAnswer)
        {
            _rightAnswers++;
            print("YES");
        }
        else
        {
            print("NO");
        }
        MoveNext();
    }
    private void MoveNext()
    {
        if (_currentTask + 1 >= _tasks.Length)
        {
            Exit();
        }
        else
        {
            _currentTask++;
            ShowTest();
        }
    }

    private void Exit()
    {
        _canvas.enabled = false;        
        _networkIdentity.gameObject.GetComponent<PlayerMovement>().UnPause();
    }

    public void GetPractices()
    {
        //Андрей спасибо за реализацию
        _tasks = new Practice[4];
        for(int i = 0; i < _tasks.Length; i++)
        {
            _tasks[i].rightAnswer = i.ToString();
            _tasks[i].question = i.ToString();
            for(int j = 0; j < 4;j++)
            {
                _tasks[i].answers = new List<string>();
                _tasks[i].answers.Add(j.ToString());
            }
        }
    }

}