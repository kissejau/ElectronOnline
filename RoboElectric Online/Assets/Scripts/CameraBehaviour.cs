using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{


    public Transform _gameObject;
    private Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    void Update()
    {

        UpdateCameraPosition();

    }

    void UpdateCameraPosition()
    {

        try
        {
            transform.position = new Vector3(
                _gameObject.position.x,
                _gameObject.position.y + 1,
                -10.0f
            );
        }
        catch (Exception error)
        {
            Debug.LogError(error);
        }

    }
}