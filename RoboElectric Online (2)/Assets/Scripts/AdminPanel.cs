using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Practice
{
    public Practice(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.question = "";
        this.rightAnswer = "";
        answers = new List<string>();
        answers.Add("");
        answers.Add("");
        answers.Add("");
        answers.Add("");
    }

    public string rightAnswer;
    public GameObject gameObject;
    public string question;
    public List<string> answers;
}
public class Level
{
    public Level(GameObject gameObject)
    {
        this.gameObject = gameObject;
        lecture = "";
        practice = new List<Practice>();
    }
    public GameObject gameObject;
    public string lecture;
    public List<Practice> practice;
}

public class AdminPanel : MonoBehaviour
{
    List<Level> list = new List<Level>();

    [SerializeField]
    ButtonSpawn spawn;
    [SerializeField]
    ButtonPracticeSpawn practiceSpawn;
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject practiceButton;
    [SerializeField]
    GameObject addButton;
    [SerializeField]
    GameObject addPracticeButton;
    [SerializeField]
    GameObject LevelEditI;
    [SerializeField]
    GameObject lectureSpace;
    [SerializeField]
    GameObject practiceSpace;
    [SerializeField]
    GameObject lectureInput;
    [SerializeField]
    GameObject practiceAskInput;
    [SerializeField]
    GameObject practiceAnswerInput1;
    [SerializeField]
    GameObject practiceAnswerInput2;
    [SerializeField]
    GameObject practiceAnswerInput3;
    [SerializeField]
    GameObject practiceAnswerInput4;
    [SerializeField]
    GameObject solution;
    private int _currentButton = 0;
    private int _currentPracticeButton = 0;

    private const float INDENT = 100f;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        LevelEditI.SetActive(false);
        lectureSpace.SetActive(false);
        practiceSpace.SetActive(false);
    }

    public void Restart() => Start();
    public void AddLevel()
    {
        var obj = spawn.AddButton(button);
        list.Add(new Level(obj));
        CorrectButton(obj, INDENT, addButton, list.Count);
        LevelEditI.SetActive(false);
    }

    public void AddPractice()
    {
        var obj = practiceSpawn.AddPractice(practiceButton);
        list[_currentButton].practice.Add(new Practice(obj));
        CorrectButton(obj, INDENT, addPracticeButton, list[_currentButton].practice.Count);
        solution.SetActive(false);
    }

    public void CorrectButton(GameObject obj, float indent, GameObject button, int len)
    {
        if (button == addButton)
        {
            obj.GetComponent<Button>().onClick.AddListener(() => EditLevel(len - 1, obj));
        }
        else
        {
            obj.GetComponent<Button>().onClick.AddListener(() => EditPractice(len - 1, obj));
        }
        obj.GetComponentInChildren<Text>().text = len.ToString();
        obj.GetComponent<RectTransform>().localPosition = button.GetComponent<RectTransform>().localPosition;
        button.GetComponent<RectTransform>().localPosition -= new Vector3(0, indent, 0);
        print("CHANGE!");
    }

    public void CorrectButton(GameObject obj, float indent, bool flag)
    {
        obj.GetComponentInChildren<Text>().text = (System.Convert.ToInt32(obj.GetComponentInChildren<Text>().text) - 1).ToString();
        obj.GetComponent<RectTransform>().localPosition -= new Vector3(0, indent, 0);
        print("CHANGE!");
    }

    public void EditLevel(int index, GameObject obj)
    {
        _currentButton = System.Convert.ToInt32(obj.GetComponentInChildren<Text>().text) - 1;
        LevelEditI.SetActive(true);
        CloseLecture();
        ClosePractice();
    }

    public void EditPractice(int index, GameObject obj)
    {
        _currentPracticeButton = System.Convert.ToInt32(obj.GetComponentInChildren<Text>().text) - 1;
        print(_currentPracticeButton);
        solution.SetActive(true);
    }

    public void DeleteLevel()
    {
        Destroy(list[_currentButton].gameObject);
        list.RemoveAt(_currentButton);
        RedefineList(_currentButton, addButton);
        LevelEditI.SetActive(false);
    }

    public void DeleteTask()
    {
        Destroy(list[_currentButton].practice[_currentPracticeButton].gameObject);
        list[_currentButton].practice.RemoveAt(_currentPracticeButton);
        RedefineList(_currentPracticeButton, addPracticeButton);
    }

    public void RedefineList(int index, GameObject button)
    {
        print($"REDEFINE!!! {index}");
        button.GetComponent<RectTransform>().localPosition += new Vector3(0, INDENT, 0);
        if (button == addButton)
        {
            for (int i = index; i < list.Count; i++)
            {
                CorrectButton(list[i].gameObject, -INDENT, true);
            }
        }
        else
        {
            for (int i = index; i < list[_currentButton].practice.Count; i++)
            {
                CorrectButton(list[_currentButton].practice[i].gameObject, -INDENT, true);
            }
        }
    }

    public void OpenLecture()
    {
        lectureSpace.SetActive(true);
        ClosePractice();
        SetLecture();
        print(_currentButton);
    }
    public void CloseLecture()
    {
        lectureSpace.SetActive(false);
    }

    public void OpenPractice()
    {
        practiceSpace.SetActive(true);
        CloseLecture();
        SetPractice();
    }
    public void ClosePractice()
    {
        practiceSpace.SetActive(false);
        solution.SetActive(false);
    }


    public void SetPractice()
    {
        // print(_currentButton);
        // print(_currentPracticeButton);
        // print(list.Count);
        // print(list[_currentButton])
        practiceAskInput.GetComponent<Text>().text = list[_currentButton].practice[_currentPracticeButton].question;
        practiceAnswerInput1.GetComponent<Text>().text = list[_currentButton].practice[_currentPracticeButton].answers[0];
        practiceAnswerInput2.GetComponent<Text>().text = list[_currentButton].practice[_currentPracticeButton].answers[1];
        practiceAnswerInput3.GetComponent<Text>().text = list[_currentButton].practice[_currentPracticeButton].answers[2];
        practiceAnswerInput4.GetComponent<Text>().text = list[_currentButton].practice[_currentPracticeButton].answers[3];
    }
    public void SubmitPractice()
    {
        list[_currentButton].practice[_currentPracticeButton].question = practiceAskInput.GetComponent<Text>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[0] = practiceAnswerInput1.GetComponent<Text>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[1] = practiceAnswerInput2.GetComponent<Text>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[2] = practiceAnswerInput3.GetComponent<Text>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[3] = practiceAnswerInput4.GetComponent<Text>().text;
    }
    public void SetLecture()
    {
        lectureInput.GetComponent<TMP_InputField>().text = list[_currentButton].lecture;
    }
    public void SubmitLecture()
    {
        list[_currentButton].lecture = lectureInput.GetComponent<TMP_InputField>().text;
        print(list[_currentButton].lecture);
        print(_currentButton);
    }
}
