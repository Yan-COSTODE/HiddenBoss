using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum ETaskType
{
    NONE,
    TASK1,
    TASK2,
    TASK3,
    TASK4
}

public class TaskGenerator : MonoBehaviour
{
    public Action onTask;
    
    [Header("References")]
    [SerializeField] private Button task1Button;
    [SerializeField] private Button task2Button;
    [SerializeField] private Button task3Button;
    [SerializeField] private Button task4Button;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private TMP_FontAsset taskConsoleFont;
    [SerializeField] private TMP_FontAsset taskExcelFont;
    [SerializeField] private GameObject excelBackground;
    [SerializeField] private GameObject mailVideo;
    [SerializeField] private GameObject officeVideo;
    [Header("Settings")] 
    [SerializeField, Range(0.0f, 30.0f)] private float fTaskTime = 10.0f;
    private int iLineCount = 0;


    private void Awake()
    {
        task1Button.onClick.AddListener(() => StartTask(ETaskType.TASK1));
        task2Button.onClick.AddListener(() => StartTask(ETaskType.TASK2));
        task3Button.onClick.AddListener(() => StartTask(ETaskType.TASK3));
        task4Button.onClick.AddListener(() => StartTask(ETaskType.TASK4));
    }

    private void SetAllButtonVisibility(bool _status)
    {
        task1Button.interactable = _status;
        task2Button.interactable = _status;
        task3Button.interactable = _status;
        task4Button.interactable = _status;
        GetComponentInParent<GameWindows>().CloseButton.interactable = _status;
    }
    
    private void StartTask(ETaskType _taskType)
    {
        Player.Instance.SetTaskType(_taskType);
        ControlManager.Instance.SetEnable(false);
        taskText.text = "";
        iLineCount = 0;
        SetAllButtonVisibility(false);
        onTask?.Invoke();

        switch (_taskType)
        {
            case ETaskType.NONE:
                break;
            case ETaskType.TASK1: Task1(); 
                break;
            case ETaskType.TASK2: Task2(); 
                break;
            case ETaskType.TASK3: Task3(); 
                break;
            case ETaskType.TASK4: Task4(); 
                break;
        }
    }

    private void EndOfTask()
    {
        ControlManager.Instance.SetEnable(true);
        Player.Instance.SetTaskType(ETaskType.NONE);
        excelBackground.SetActive(false);
        officeVideo.SetActive(false);
        mailVideo.SetActive(false);
        taskText.font = taskConsoleFont;
        taskText.text = "";
        SetAllButtonVisibility(true);
    }

    private void AddLine(string _line)
    {
        iLineCount++;
        taskText.text += _line + '\n';

        if (iLineCount > 15)
        {
            int _index = taskText.text.IndexOf('\n', StringComparison.OrdinalIgnoreCase);
            taskText.text = taskText.text.Substring(_index + 1);
            
            iLineCount--;
        }
    }
    
    //Console
    private void Task1()
    {
        int _length = 50;
        taskText.font = taskConsoleFont;
        taskText.color = Color.green;
        
        TimerManager.Instance.Create(fTaskTime, EEasing.EASE_NONE, EndOfTask, null, f =>
        {
            string _line = "";
            
            for (int i = 0; i < _length; i++)
            {
                int _rand = Random.Range(0, 65);
                _line += _rand < 33 ? ' ' : (char)_rand;
            }
            
            AddLine(_line);
        });
    }
    
    private void AddExcel(string[] _tab)
    {
        for (int i = 0; i < 15; i++)
        {
            taskText.text += _tab[i] + '\n';
            int _index = taskText.text.IndexOf('\n', StringComparison.OrdinalIgnoreCase);
            taskText.text = taskText.text.Substring(_index);
        }
    }
    
    //Excel
    private void Task2()
    {
        string[] randWordTab = {"cat", "dog", "potatoe", "computer", "fuckboss", "evolve", "monday", "to-do", "employee", "taget", "leviosa", "boss mom", "month", "money"};
        string[] _tab = new string[15];
        int _id = Random.Range(000, 980);
        taskText.font = taskExcelFont;
        taskText.color = Color.black;
        excelBackground.SetActive(true);
        
        TimerManager.Instance.Create(fTaskTime, EEasing.EASE_NONE, EndOfTask, null, f =>
        {
            int _value = 0;
            _tab[0] = $"\n{" Nbr.", -5}{"  Object", -15}{"Value", 4}  .\n";
            
            for (int i = 1; i < 15; i++)
            {
                _value = Random.Range(1000, 9999);
                _tab[i] = $" {_id + i, -5} {randWordTab[Random.Range(0, randWordTab.Length)], -13}{_value, 4}" + "  .";
            }
            
            AddExcel(_tab);
        });
    }
    
    //Mail
    private void Task3()
    {
        mailVideo.SetActive(true);
        TimerManager.Instance.Create(fTaskTime, EEasing.EASE_NONE, EndOfTask);
    }
    
    //Office
    private void Task4()
    {
        officeVideo.SetActive(true);
        TimerManager.Instance.Create(fTaskTime, EEasing.EASE_NONE, EndOfTask);
    }
}
