using System;
using UnityEngine;

public static class Tweening
{
	public static void DOMoveLocal(this RectTransform _transform, Vector2 _position, float _duration, EEasing _easing = EEasing.EASE_NONE)
	{
		Vector2 _basePositon = _transform.anchoredPosition;
		TimerManager.Instance.Create(_duration, _easing, () => SafeMoveLocal(_transform, _position), null, (_time) =>
		{
			SafeMoveLocal(_transform, Vector2.Lerp(_basePositon, _position, _time /  _duration));
		});
	}
	
	private static void SafeMoveLocal(RectTransform _transform, Vector3 _position)
	{
		if (!_transform)
			return;

		_transform.anchoredPosition = _position;	
	}
	
	public static void DOMove(this Transform _transform, Vector3 _position, float _duration, EEasing _easing = EEasing.EASE_NONE)
	{
		Vector3 _basePositon = _transform.position;
		TimerManager.Instance.Create(_duration, _easing, () => SafeMove(_transform, _position), null, (_time) =>
		{
			SafeMove(_transform, Vector3.Slerp(_basePositon, _position, _time /  _duration));
		});
	}
	
	private static void SafeMove(Transform _transform, Vector3 _position)
	{
		if (!_transform)
			return;

		_transform.position = _position;	
	}

	public static void DORotate(this Transform _transform, Vector3 _rotation, float _duration, EEasing _easing = EEasing.EASE_NONE)
	{
		Vector3 _baseRotation = _transform.eulerAngles;
		TimerManager.Instance.Create(_duration, _easing, () => SafeRotate(_transform, _rotation), null, (_time) =>
		{
			SafeRotate(_transform, Vector3.Slerp(_baseRotation, _rotation, _time /  _duration));
		});
	}
	
	private static void SafeRotate(Transform _transform, Vector3 _rotation)
	{
		if (!_transform)
			return;

		_transform.eulerAngles = _rotation;	
	}
	
	public static void DORotate(this Transform _transform, Quaternion _rotation, float _duration, EEasing _easing = EEasing.EASE_NONE)
	{
		Quaternion _baseRotation = _transform.rotation;
		TimerManager.Instance.Create(_duration, _easing, () => SafeRotate(_transform, _rotation), null, (_time) =>
		{
			SafeRotate(_transform, Quaternion.Slerp(_baseRotation, _rotation, _time /  _duration));
		});
	}
	
	private static void SafeRotate(Transform _transform, Quaternion _rotation)
	{
		if (!_transform)
			return;

		_transform.rotation = _rotation;	
	}

	public static void DOScale(this Transform _transform, Vector3 _scale, float _duration, EEasing _easing = EEasing.EASE_NONE)
	{
		Vector3 _baseScale = _transform.localScale;
		TimerManager.Instance.Create(_duration, _easing, () => SafeScale(_transform, _scale), null, (_time) =>
		{
			SafeScale(_transform, Vector3.Slerp(_baseScale, _scale, _time /  _duration));
		});
	}

	private static void SafeScale(Transform _transform, Vector3 _scale)
	{
		if (!_transform)
			return;

		_transform.localScale = _scale;	
	}
}
