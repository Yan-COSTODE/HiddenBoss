using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private SerializableDictionary<string, Timer> activeTimers = new();
	private readonly List<string> timersToRemove = new();
	#endregion
	
	#region Properties
	#endregion
	#endregion

	#region Methods
    private void Update()
    {
	    foreach (KeyValuePair<string, Timer> _timer in activeTimers)
		    TreatTimer(_timer.Value);
	    
	    DeleteTimers();
    }

    private void TreatTimer(Timer _timer)
    {
	    if (_timer.Paused)
		    return;
	    
	    _timer.UpdateTimer();
	    
	    if (_timer.Percent >= 1.0f)
		    timersToRemove.Add(_timer.ID);
    }
    
    private void DeleteTimers()
    {
	    if (timersToRemove.Count <= 0)
		    return;
	    
	    foreach (string _id in timersToRemove)
		    activeTimers.Remove(_id);
	    
	    timersToRemove.Clear();
    }

    public string Create(float _duration, EEasing _easing = EEasing.EASE_NONE, Action _onComplete = null,
	    Action _onStart = null, Action<float> _onUpdate = null, bool _isLooping = false, bool _useUnscaledTime = false)
    {
	    string  _id = GenerateTimerID();
	    return Create(_id, _duration, _easing, _onComplete, _onStart, _onUpdate, _isLooping, _useUnscaledTime);
    }

    private string Create(string _id, float _duration, EEasing _easing = EEasing.EASE_NONE, Action _onComplete = null, Action _onStart = null,
	    Action<float> _onUpdate = null, bool _isLooping = false, bool _useUnscaledTime = false)
    {
	    activeTimers.Remove(_id);
	    Timer _timer = new Timer(_id, _duration, 0.0f, _easing, _onComplete, _onStart, _onUpdate, _isLooping, false, _useUnscaledTime);
	    activeTimers.Add(_id, _timer);
	    _onStart?.Invoke();
	    return _id;
    }
    
    public void PauseTimer(string _id)
    {
	    if (activeTimers.Contains(_id))
		    activeTimers[_id].SetPaused(true);
    }
    
    public void ResumeTimer(string _id)
    {
	    if (activeTimers.Contains(_id))
		    activeTimers[_id].SetPaused(false);
    }
    
    public void StopTimer(string _id) => activeTimers.Remove(_id);
    
    public Timer GetTimerInfo(string _id) => activeTimers.TryGetValue(_id, out Timer _timer) ? _timer : null;
    
    public bool IsTimerActive(string _id) => activeTimers.Contains(_id);
    
    public void StopAllTimers()
    {
	    activeTimers.Clear();
	    timersToRemove.Clear();
    }

    private string GenerateTimerID() => IDGenerator.GenerateID("timer_");
    #endregion Methods
}
