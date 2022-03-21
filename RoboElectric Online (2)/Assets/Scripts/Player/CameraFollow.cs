using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField]
    private ScoreBoard costil;

    private Camera _camera;
    private Rigidbody2D _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    public override void OnStartLocalPlayer()
    {
        this.gameObject.name = Random.Range(1, 10).ToString();
        costil.UpdateScoreBoard(this.gameObject, 0);        
        if (_camera != null)
        {
            _camera.orthographic = false;
            _camera.transform.SetParent(transform);
            _camera.transform.localPosition = new Vector3(0f, 0f, -10f);
        }
    }


}
