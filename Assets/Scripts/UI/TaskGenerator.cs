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
    [Header("References")]
    [SerializeField] private Button task1Button;
    [SerializeField] private Button task2Button;
    [SerializeField] private Button task3Button;
    [SerializeField] private Button task4Button;
    [SerializeField] private TMP_Text taskText;
    [Header("Settings")] 
    [SerializeField, Range(0.0f, 30.0f)] private float fTaskTime = 10.0f;
    private ETaskType taskType = ETaskType.NONE;
    private int iLineCount = 0;

    public ETaskType TaskType => taskType;

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
        taskType = _taskType;
        ControlManager.Instance.SetEnable(false);
        taskText.text = "";
        iLineCount = 0;
        SetAllButtonVisibility(false);

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
        taskType = ETaskType.NONE;
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
    
    //Excel
    private void Task2()
    {
        
    }
    
    //Mail
    private void Task3()
    {
        
    }
    
    //Office
    private void Task4()
    {
        
    }
}
