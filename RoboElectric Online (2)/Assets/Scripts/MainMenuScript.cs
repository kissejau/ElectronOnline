using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    Canvas admin;
    [SerializeField]
    AdminPanel adminPanel;
    [SerializeField]
    Canvas canvas;


    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    private void Awake()
    {

    }

    public void OpenAdminPanel()
    {
        canvas.gameObject.SetActive(false);
        admin.gameObject.SetActive(true);
        adminPanel.Restart();
    }

    public void CloseAdminPanel()
    {
        canvas.gameObject.SetActive(true);
        admin.gameObject.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
