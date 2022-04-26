using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPracticeSpawn : MonoBehaviour
{
    private List<GameObject> list = new List<GameObject>();
    public GameObject AddPractice(GameObject gameObject, int practicePage)
    {
        {
            if (practicePage >= list.Count)
            {
                list.Add(new GameObject($"page_{practicePage}"));
                list[practicePage].transform.SetParent(this.gameObject.transform);
                list[practicePage].transform.localPosition = Vector3.zero;
            }
            var obj = Instantiate(gameObject);
            obj.transform.SetParent(list[practicePage].transform);
            return obj;
        }
    }
}
