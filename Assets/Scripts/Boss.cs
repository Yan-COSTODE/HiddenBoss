using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Boss : Singleton<Boss>
{
    private const int MAX_TIME = 86400;
    public Action OnWordTalked;
    public Action OnBossCheck;
    public Action OnBossFakeCheck;
    public Action onBossEnter;
    public Action onBossExit;
    
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float fSpeedOfSpeech = 0.1f;
    [SerializeField] private float fDeleteDelay = 5.0f;
    [SerializeField] private GameObject visual;
    private float fTimeOfTask = -1.0f;
    private ETaskType taskNeeded = ETaskType.NONE;
    private string dialogue = "";
    
    public float TimeOfTask => fTimeOfTask;
    public ETaskType TaskNeeded => taskNeeded;
    public string Dialogue => dialogue;

    private void Update()
    {
        Checker();
    }

    private void RandomChecker()
    {
        
    }
    
    private void Checker()
    {
        if (fTimeOfTask == -1.0f)
            return;
        
        
        if (Mathf.Abs(Clock.Instance.CurrentTime - fTimeOfTask) > 10.0f)
            return;

        fTimeOfTask = -1.0f;
        StartCoroutine(CheckerIE());
    }

    private IEnumerator CheckerIE()
    {
        OnBossCheck?.Invoke();
        Debug.Log("BossCheck");
        yield return new WaitForSeconds(5.0f);
        onBossEnter?.Invoke();
        Debug.Log("BossEnter");
        yield return new WaitForSeconds(2.0f);
        
        Debug.Log("BossVerif");
        
        if (Player.Instance.CurrentTask == taskNeeded)
        {
            GenerateTask("Well done, but you can't fool me.");
            yield return new WaitForSeconds(2.0f + fDeleteDelay);
        }
        else
        {
            dialogueText.text = "";
            visual.SetActive(true);
            dialogue =
                "OoooOOOOoOOoOOooOOoHHHHH WwwwwWHhHHHHAaAAaAATTTTTTtt AAAAaAaAaAaRRrrrrreeeeeeeEE YYYYYYYyyyyOOOOoooUUUUuuuUUu DDDDDddddOOOOOooIIIiiiinnnNNNGGGGggg !!????!!!";
            StartCoroutine(WriteText());
            yield return new WaitForSeconds(5.0f + fDeleteDelay);
            Reload();
        }
    }

    private void Reload()
    {
        LeaderboardManager.Instance.AddScoreToLeaderBoard(Player.Instance.PlayerData);
        string _currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_currentSceneName);
    }
    
    private IEnumerator WriteText()
    {
        WaitForSeconds _delay = new WaitForSeconds(fSpeedOfSpeech);

        for (int i = 0; i < dialogue.Length; ++i)
        {
            if (i == 0 || dialogueText.text[^1] == ' ')
                OnWordTalked?.Invoke();
            
            dialogueText.text = dialogue.Substring(0, i + 1);

            if (i < dialogue.Length - 5)
            {
                if (dialogue.Substring(i + 1, 3) == "<b>")
                    i += 3;
                else if (dialogue.Substring(i + 1, 4) == "</b>")
                    i += 4;
            }

            yield return _delay;
        }
        
        yield return new WaitForSeconds(fDeleteDelay);
        onBossExit?.Invoke();
        visual.SetActive(false);
    }
    
    public void GenerateTask(string _prefix = "")
    {
        visual.SetActive(true);
        taskNeeded = (ETaskType)Random.Range(1, 5);
        fTimeOfTask = Clock.Instance.CurrentTime + Random.Range(60.0f * 60.0f * 1.0f, 60.0f * 60.0f * 3.0f);
        fTimeOfTask %= MAX_TIME;
        GenerateDialogue(_prefix);
        StartCoroutine(WriteText());
    }

    private string GetTaskName()
    {
        switch (taskNeeded)
        {
            case ETaskType.TASK1: return "Console Programming";
            case ETaskType.TASK2: return "Tab Terminator";
            case ETaskType.TASK3: return "Mail Sender";
            case ETaskType.TASK4: return "Typer Flow";
        }
        
        return "None";
    }

    private string GetTime()
    {
        Vector3Int _time = Vector3Int.zero;
        _time.x = Mathf.FloorToInt(fTimeOfTask / 3600);
        _time.y = Mathf.FloorToInt((fTimeOfTask % 3600) / 60);
        _time.z = Mathf.FloorToInt(fTimeOfTask % 60);
        return $"{_time.x:00}:{_time.y:00}";
    }
    
    private void GenerateDialogue(string _prefix = "")
    {
        dialogue = _prefix == "" ? "Hi, you trash. You know i'm your favorite boss paying you for minimum wages" : _prefix;
        dialogue += " I want you to be on <b>";
        dialogue += GetTaskName();
        dialogue += "</b> by the time i come at around <b>";
        dialogue += GetTime();
        dialogue += "</b>. If i catch you doing something else. You're gonna remember this moment.";
    }
}
