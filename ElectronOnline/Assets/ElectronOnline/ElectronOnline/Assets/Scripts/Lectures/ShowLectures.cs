using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShowLectures : Interactable
{
    [SerializeField]
    private GameObject _warningMessage;
    [SerializeField]
    private Text _lectureText;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private Lecture _lecture;
    [SerializeField]
    private GameObject _nextButton;
    [SerializeField]
    private GameObject _prevButton;
    [SerializeField]
    private GameObject _exitButton;

    private bool _wasOnLastPage;

    private NetworkIdentity _player;

    public override void Interaction(NetworkIdentity gameObject)
    {        
        if (!gameObject.gameObject.GetComponent<PlayerMovement>().Paused)
        {
            if (_lecture.SheetCount != 1)
            {
                _wasOnLastPage = false;
            }
            else
            {
                _wasOnLastPage = true;
            }
            _player = gameObject;
            _canvas.enabled = true;
            gameObject.gameObject.GetComponent<PlayerMovement>().Pause();
            _lectureText.text = _lecture.GetCurrentSheetText();            
        }
        else
        {
            Exit();
        }
    }
    public void ShowCurrentSheet()
    {
        _lectureText.text = _lecture.GetCurrentSheetText();
    }

    public void MoveNextSheet()
    {            
        if(_lecture.MoveNext())
        {
            if(_lecture.CurrentSheet == _lecture.SheetCount - 1)
            {
                _wasOnLastPage = true;
            }
            ShowCurrentSheet();
        }
        CheckButtons();
    }
    public void MovePreviousSheet()
    {
        if (_lecture.MovePrevious())
        {
            ShowCurrentSheet();
        }
        CheckButtons();
    }

    private void CheckButtons()
    {
        if (_lecture.CurrentSheet == _lecture.SheetCount - 1)
        {
            _nextButton.SetActive(false);
        }
        else
        {
            _nextButton.SetActive(true);
        }
        if (_lecture.CurrentSheet > 0)
        {
            _prevButton.SetActive(true);
        }
        else
        {
            _prevButton.SetActive(false);
        }
    }

    public void Exit()
    {
        if (_wasOnLastPage)
        {
            _canvas.enabled = false;
            _player.gameObject.GetComponent<PlayerMovement>().UnPause();
            _prevButton.SetActive(false);
            _nextButton.SetActive(true);
            _lecture.CurrentSheet = 0;
            _lectureText.text = _lecture.GetCurrentSheetText();
        }
        else
        {
            _lectureText.text = string.Empty;
            ThrowWarningMessage();
        }
    }
    
    private void ThrowWarningMessage()
    {
        SetButtonsStatus(false);
        _warningMessage.SetActive(true);
    }

    public void ExitAfterWarning()
    {
        SetButtonsStatus(true);
        _wasOnLastPage = true;
        _warningMessage.SetActive(false);
        Exit();
    }


    public void ContinueAfterWarning()
    {
        SetButtonsStatus(true);
        _warningMessage.SetActive(false);
        ShowCurrentSheet();
    }

    private void SetButtonsStatus(bool status)
    {
        _prevButton.GetComponent<Button>().interactable = status;
        _nextButton.GetComponent<Button>().interactable = status;
        _exitButton.GetComponent<Button>().interactable = status;
    }
}