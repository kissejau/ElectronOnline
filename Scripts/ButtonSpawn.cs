using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawn : MonoBehaviour
{
    public GameObject AddButton(GameObject gameObject)
    {
        var obj = Instantiate(gameObject);
        obj.transform.SetParent(this.gameObject.transform);
        return obj;
    }
}
