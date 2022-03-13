using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    [SerializeField]
    private ShowLectures _shower;
    
    public void Confirm()
    {
        _shower.ExitAfterWarning();
    }

    public void Decline()
    {
        _shower.ContinueAfterWarning();
        gameObject.SetActive(false);
    }
}
