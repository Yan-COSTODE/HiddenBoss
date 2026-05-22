using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SKeyValuePair<TKey, TValue>
{
	public TKey Key;
	public TValue Value;
	
	public SKeyValuePair(TKey _key, TValue _value)
	{
		Key = _key;
		Value = _value;
	}
	
	public void SetValue(TValue _value) => Value = _value;
}

[Serializable]
public class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private List<SKeyValuePair<TKey, TValue>> dictionary = new();
	#endregion
	
	#region Properties
	public int Count => dictionary.Count;
	public IEnumerable<TKey> Keys
	{
		get
		{
			foreach (SKeyValuePair<TKey, TValue> _pair in dictionary)
				yield return _pair.Key;
		}
	}

	public IEnumerable<TValue> Values
	{
		get
		{
			foreach (SKeyValuePair<TKey, TValue> _pair in dictionary)
				yield return _pair.Value;
		}
	}
	#endregion
	#endregion

	#region Methods
	public TValue this[TKey _key]
	{
		get
		{
			int _index = IndexOf(_key);
			return _index == -1 ? throw new KeyNotFoundException($"Key '{_key}' not found in dictionary") : dictionary[_index].Value;
		}
		set
		{
			int _index = IndexOf(_key);
			
			if (_index == -1)
				Add(_key, value);
			else
				dictionary[_index].SetValue(value);
		}
	}

	public bool TryGetValue(TKey _key, out TValue _value)
	{
		_value = default;
		int _index = IndexOf(_key);
		
		if (_index == -1)
			return false;
		
		_value = dictionary[_index].Value;
		return true;
	}

	private int IndexOf(TKey _key)
	{
		for (int i = 0; i < dictionary.Count; i++)
			if (dictionary[i].Key.Equals(_key))
				return i;

		return -1;
	}
	
	public void Clear() => dictionary.Clear();
	
	public void Add(TKey _key, TValue _value)
	{
		if (Contains(_key))
			throw new ArgumentException($"An element with key '{_key}' already exists");
		
		dictionary.Add(new SKeyValuePair<TKey, TValue>(_key, _value));
	}

	public void Remove(TKey _key)
	{
		int _index = IndexOf(_key);
		
		if (_index == -1)
			return;
		
		dictionary.RemoveAt(_index);
	}

	public bool Contains(TKey _key)
	{
		return IndexOf(_key) != -1;
	}
	
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		for (int i = 0; i < dictionary.Count; i++)
			yield return new KeyValuePair<TKey, TValue>(dictionary[i].Key, dictionary[i].Value);
	}
	#endregion Methods
}
