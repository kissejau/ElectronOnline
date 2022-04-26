using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture : Interactable
{
    private string _lecture;
    private TMPro.TMP_InputField _InputField;
    private Canvas _canvas;

    public override void Interaction(NetworkIdentity gameObject)
    {
        if (isLocalPlayer)
        {
            if (!_canvas.enabled)
            {
                gameObject.gameObject.GetComponent<PlayerMovement>().Pause();
                _canvas.enabled = true;
                _InputField.text = _lecture;
            }
            else
            {
                _canvas.enabled = false;
                gameObject.gameObject.GetComponent<PlayerMovement>().UnPause();
            }
        }
    }

    public void GetLectureText()
    {
        // Андрей спасибо за реализацию
        _lecture = string.Empty;
    }
}
