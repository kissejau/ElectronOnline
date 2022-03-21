using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPracticeSpawn : MonoBehaviour
{
    public GameObject AddPractice(GameObject gameObject)
    {
        {
            var obj = Instantiate(gameObject);
            obj.transform.SetParent(this.gameObject.transform);
            return obj;
        }
    }
}
