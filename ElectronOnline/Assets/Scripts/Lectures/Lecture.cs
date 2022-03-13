using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture : MonoBehaviour
{
    [SerializeField]
    protected string lectureName;
    
    public string[] Text;
    public int SheetCount => Text.Length;
    public int CurrentSheet;

    private void Start()
    {        
        CurrentSheet = 0;
    }

    public virtual bool MoveNext()
    {
        if(CurrentSheet + 1 >= Text.Length)
            return false;
        CurrentSheet++;
        return true;
    }
    public virtual bool MovePrevious()
    {
        if(CurrentSheet - 1 < 0)
            return false;
        CurrentSheet--;
        return true;
    }
    public string GetCurrentSheetText()
    {
        return Text[CurrentSheet];
    }
}
