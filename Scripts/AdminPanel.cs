using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute.ClearExtensions;
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
    [SerializeField]
    GameObject exampleBtn;
    
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
        practiceSpace.SetActive(false);
        AddPractice();
        foreach (var i in list)
        {
            print($"list {i.practice.Count}");
        }
    }

    public void AddPractice()  
    {
        solution.SetActive(false);

        print("ADD PRACTICE WITH count = " + (list.Count - 1));
        GameObject obj;
        if (practiceSpace.activeSelf)
        {
            print(1);
            obj = practiceSpawn.AddPractice(practiceButton, _currentButton);
            list[_currentButton].practice.Add(new Practice(obj));
        }
        else
        {
            print(2);
            obj = practiceSpawn.AddPractice(practiceButton, list.Count - 1);
            list[list.Count - 1].practice.Add(new Practice(obj));
        }
        CorrectButton(obj, INDENT, addPracticeButton, list[_currentButton].practice.Count);
        RedefineButtons();

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
        _currentPracticeButton = 0;
        print(_currentButton + " currentBtn");
        LevelEditI.SetActive(true);
        CloseLecture();
        OpenPractice();
    }

    public void EditPractice(int index, GameObject obj)
    {
        _currentPracticeButton = System.Convert.ToInt32(obj.GetComponentInChildren<Text>().text) - 1;
        print(_currentPracticeButton + " currentPracticeBtn");
        print(_currentButton + " currentBtn");
        print("edit practice");
        SetPractice();
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
        var objs = practiceSpace.transform.Find($"ButtonPracticeSpawner/page_{_currentButton}").gameObject;
        if (objs.transform.childCount > 0)
        {
            Destroy(list[_currentButton].practice[_currentPracticeButton].gameObject);
            list[_currentButton].practice.RemoveAt(_currentPracticeButton);
            RedefineList(_currentPracticeButton, addPracticeButton);
        }

    }

    public void RedefineList(int index, GameObject button)
    {
        print($"REDEFINE!!! {index}");
        button.GetComponent<RectTransform>().localPosition += new Vector3(0, INDENT, 0);
        if (button == addButton)
        {
            for (int i = index; i < list.Count; i++)
            {
                print(index + " btn");
                CorrectButton(list[i].gameObject, -INDENT, true);
            }
        }
        else
        {
            for (int i = index; i < list[_currentButton].practice.Count; i++)
            {
                print(index + " practiceBtn");
                CorrectButton(list[_currentButton].practice[i].gameObject, -INDENT, true);
            }
        }
    }

    public void RedefineButtons()
    {
        var objs = practiceSpace.transform.Find($"ButtonPracticeSpawner/page_{_currentButton}").gameObject;
        int cnt = objs.transform.childCount;
        try
        {
            for (int i = 0; i < cnt; i++)
            {
                var child = objs.transform.GetChild(i).gameObject;
                child.transform.localPosition = exampleBtn.transform.localPosition + new Vector3(0, -INDENT * i, 0);
                child.GetComponentInChildren<Text>().text = $"{i+1}";
            }
            addPracticeButton.transform.localPosition = objs.transform.GetChild(cnt-1).gameObject.transform.localPosition + new Vector3(0, -INDENT);

        }
        catch(Exception ex){}

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
        var objs = practiceSpace.transform.Find("ButtonPracticeSpawner").gameObject;
        print(objs.name);
        for(int i=0; i< objs.transform.childCount; i++)
        {
            var child = objs.transform.GetChild(i).gameObject;
            if(child != null)
                child.SetActive(false);
            print(child.name + " off");
        }
        print("OpenPractice + " + objs.name);
        objs.transform.Find($"page_{_currentButton}").gameObject.SetActive(true);
        CloseLecture();
        RedefineButtons();
    }
    public void ClosePractice()
    {
        var obj = practiceSpace.transform.GetChild(_currentButton).gameObject;
        practiceSpace.SetActive(true);
        obj.SetActive(false);
        solution.SetActive(false);
    }


    public void SetPractice()
    {
        print($"list count = {list.Count}; list[{_currentButton}].practice count = {list[_currentButton].practice.Count}");
        // print(_currentButton);
        // print(_currentPracticeButton);
        // print(list.Count);
        // print(list[_currentButton])
        print("SET PRACTICE");
        practiceAskInput.GetComponent<TMP_InputField>().text = list[_currentButton].practice[_currentPracticeButton].question;
        practiceAnswerInput1.GetComponent<TMP_InputField>().text = list[_currentButton].practice[_currentPracticeButton].answers[0];
        practiceAnswerInput2.GetComponent<TMP_InputField>().text = list[_currentButton].practice[_currentPracticeButton].answers[1];
        practiceAnswerInput3.GetComponent<TMP_InputField>().text = list[_currentButton].practice[_currentPracticeButton].answers[2];
        practiceAnswerInput4.GetComponent<TMP_InputField>().text = list[_currentButton].practice[_currentPracticeButton].answers[3];
    }
    public void SubmitPractice()
    {
        print("SUBMIT PRACTICE");
        list[_currentButton].practice[_currentPracticeButton].question = practiceAskInput.GetComponent<TMP_InputField>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[0] = practiceAnswerInput1.GetComponent<TMP_InputField>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[1] = practiceAnswerInput2.GetComponent<TMP_InputField>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[2] = practiceAnswerInput3.GetComponent<TMP_InputField>().text;
        list[_currentButton].practice[_currentPracticeButton].answers[3] = practiceAnswerInput4.GetComponent<TMP_InputField>().text;
        APdebug.OutputPracticeList(list[_currentButton].practice);
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
