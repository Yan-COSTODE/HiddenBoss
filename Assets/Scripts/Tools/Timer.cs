using System;
using UnityEngine;

[Serializable]
public class Timer
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private string sId;
	[SerializeField] private float fDuration;
	[SerializeField] private float fTime;
	private EEasing easing;
	private Action onComplete;
	private Action onStart;
	private Action<float> onUpdate;
	[SerializeField] private bool bIsLooping;
	[SerializeField] private bool bIsPaused;
	private bool bUseUnscaledTime;
	#endregion
	
	#region Properties
	public string ID => sId;
	public bool Paused => bIsPaused;
	public Action OnComplete => onComplete;
	public Action OnStart => onStart;
	public Action<float> OnUpdate => onUpdate;
	public float Percent => GetCurrentTime() / fDuration;
	#endregion
	#endregion

	#region Methods
	public Timer(string _sId, float _fDuration, float _fTimeRemaining, EEasing _easing = EEasing.EASE_NONE, Action _onComplete = null,
		Action _onStart = null, Action<float> _onUpdate = null, bool _bIsLooping = false, bool _bIsPaused = false,
		bool _bUseUnscaledTime = false)
	{
		sId = _sId;
		fDuration = _fDuration;
		fTime = _fTimeRemaining;
		easing = _easing;
		onComplete = _onComplete;
		onStart = _onStart;
		onUpdate = _onUpdate;
		bIsLooping = _bIsLooping;
		bIsPaused = _bIsPaused;
		bUseUnscaledTime = _bUseUnscaledTime;
	}

	public void RemoveRemainingTime(float _duration) => fTime += _duration;

	public void UpdateTimer()
	{
		fTime += bUseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
		onUpdate?.Invoke(GetCurrentTime());
		
		if (fTime < fDuration)
			return;
		
		onComplete?.Invoke();

		if (!bIsLooping) 
			return;
		
		fTime = 0.0f;
		onStart?.Invoke();
	}
	
	private float GetCurrentTime()
	{
		float _percent = fTime / fDuration;
		return Easing.Ease(_percent, easing) * fDuration;
	}

	public void SetPaused(bool _status) => bIsPaused = _status;
	#endregion Methods
}
