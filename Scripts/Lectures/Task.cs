using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public string Question;
    public string RightVariant;    
    public string[] Variants;
    public int VariantsCount => Variants.Length;

    public Task(string Question,string[] Variants, string RightVariant)
    {
        this.Question = Question;
        this.Variants = Variants;
        this.RightVariant = RightVariant;
    }
}
