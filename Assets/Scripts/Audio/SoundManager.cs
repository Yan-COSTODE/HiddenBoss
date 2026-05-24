using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.iOS;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameWindows cookieWindowsObject;
    [SerializeField] private GameWindows investmentWindowsObject;
    [SerializeField] private Boss bossObject;
    [SerializeField] private TaskGenerator taskGeneratorObject;
    
    [SerializeField] private AudioSource AS_Ambiance;
    [SerializeField] private AudioSource AS_Music;
    [SerializeField] private AudioSource AS_UI_Click_In;
    [SerializeField] private AudioSource AS_UI_Click_Out;
    [SerializeField] private AudioSource AS_UI_Score_Positive;
    [SerializeField] private AudioSource AS_UI_Working;
    [SerializeField] private AudioSource AS_SFX_Boss_DoorOpen;
    [SerializeField] private AudioSource AS_SFX_Boos_DoorClose;
    [SerializeField] private AudioSource AS_SFX_Boss_Incoming;
    [SerializeField] private AudioSource AS_SFX_Boss_FakeIncoming;
    [SerializeField] private AudioSource AS_SFX_Boss_Talk;

    [SerializeField] private AudioMixer masterMixer;

    [SerializeField] private float fadeVolumeIncrement = 0.01f;
    
    private bool ambianceVolumeChecked = true;
    private bool musicVolumeChecked = true;

    private float ambianceVolumeAimed = 1.0f;
    private float musicVolumeAimed = 0.0f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterMixer.SetFloat("volMaster", 0.0f);

        taskGeneratorObject.onTask += Working;
        cookieWindowsObject.OnOpened += GameOpen;
        cookieWindowsObject.OnClosed += GameClose;
        investmentWindowsObject.OnOpened += GameOpen;
        investmentWindowsObject.OnClosed += GameClose;
        ControlManager.Instance.OnMouseClickPressed += ClickPressed;
        ControlManager.Instance.OnMouseClickReleased += ClickReleased;
        Player.Instance.OnScoreAdded += ScorePositive;
        bossObject.OnWordTalked +=  BossTalk;
        bossObject.OnBossCheck += BossIncoming;
        bossObject.OnBossFakeCheck += BossFakeIncoming;
        bossObject.onBossEnter += BossOpenDoor;
        bossObject.onBossExit += BossCloseDoor;
        bossObject.onGameFinished += LooseGame;
        
        GameClose();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        taskGeneratorObject.onTask -= Working;
        cookieWindowsObject.OnOpened -= GameOpen;
        cookieWindowsObject.OnClosed -= GameClose;
        investmentWindowsObject.OnOpened -= GameOpen;
        investmentWindowsObject.OnClosed -= GameClose;
        ControlManager.Instance.OnMouseClickPressed -= ClickPressed;
        ControlManager.Instance.OnMouseClickReleased -= ClickReleased;
        Player.Instance.OnScoreAdded -= ScorePositive;
        bossObject.OnWordTalked -=  BossTalk;
        bossObject.OnBossCheck -= BossIncoming;
        bossObject.OnBossFakeCheck -= BossFakeIncoming;
        bossObject.onBossEnter -= BossOpenDoor;
        bossObject.onBossExit -= BossCloseDoor;
        bossObject.onGameFinished -= LooseGame;
    }

    private void Update()
    {
        ChangeVolume(AS_Ambiance, ambianceVolumeChecked, ambianceVolumeAimed);
        ChangeVolume(AS_Music, musicVolumeChecked, musicVolumeAimed);
    }

    private void ChangeVolume(AudioSource soundEmitter, bool soundVolumeChecked, float soundVolumeAimed)
    {
        if (soundVolumeChecked) return;
        if (Math.Round(soundEmitter.volume, 3) != Math.Round(soundVolumeAimed, 3)) soundEmitter.volume += Mathf.Clamp(Mathf.Sign(soundVolumeAimed-soundEmitter.volume)*fadeVolumeIncrement, -soundEmitter.volume, 1.0f - soundEmitter.volume);
        else soundVolumeChecked = true;
    }

    void BossTalk()
    {
        if (AS_SFX_Boss_Talk.isPlaying) return;
        AS_SFX_Boss_Talk.Play();
    }

    void BossOpenDoor()
    {
        AS_SFX_Boss_DoorOpen.Play();
    }

    void BossCloseDoor()
    {
        AS_SFX_Boos_DoorClose.Play();
    }

    void BossIncoming()
    {
        AS_SFX_Boss_Incoming.Play();
    }

    void BossFakeIncoming()
    {
        AS_SFX_Boss_FakeIncoming.Play();
    }

    void ScorePositive()
    {
        AS_UI_Score_Positive.Play();
    }

    void ClickPressed()
    {
        AS_UI_Click_In.Play();
    }

    void ClickReleased()
    {
        AS_UI_Click_Out.Play();
    }

    void GameOpen()
    {
        ambianceVolumeAimed = 0.2f;
        musicVolumeAimed = 1.0f;
        ambianceVolumeChecked = false;
        musicVolumeChecked = false;
    }

    void GameClose()
    {
        ambianceVolumeAimed = 1.0f;
        musicVolumeAimed = 0.0f;
        ambianceVolumeChecked = false;
        musicVolumeChecked = false;
    }

    void Working()
    {
        AS_UI_Working.Play();
    }
    
    void LooseGame()
    {
        StartCoroutine(FadeOutMaster());
    }
    
    
    IEnumerator FadeOutMaster()
    {
        float value = 0.0f;

        while (value > -80.0f)
        {
            value = Mathf.Clamp(value - 1.0f, -80.0f, 0.0f);
            masterMixer.SetFloat("volMaster", value);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
