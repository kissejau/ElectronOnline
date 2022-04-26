using System.Collections.Generic;

public class APdebug : UnityEngine.MonoBehaviour
{
    public static void OutputPracticeList(List<Practice> list)
    {
        int cnt = 0;
        foreach (var l in list)
        {
            cnt++;
            print($"*** APdebug *** {cnt}. {l.question} : {l.answers[0]}, {l.answers[2]}, {l.answers[2]}, {l.answers[3]}");
        }
    }
}