using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

	[SerializeField]
	private Overdrivable _CurrentLevel;

	[SerializeField]
	private int _currentIndex; 
	
	public int StartIndex = 0;

	public Overdrivable[] Levels;


	
	protected virtual void Awake() {
		foreach(var l in Levels)
		{
			l.enabled  = false;
		}
		
		StartIndex = Mathf.Clamp(StartIndex, 0, Levels.Length-1);
		_currentIndex = StartIndex;
		if(Levels.Length < 1) return;
		CurrentLevel = Levels[_currentIndex]; 
	}

	public bool NextLevel()
	{
		if(_currentIndex + 1 > Levels.Length - 1) return false;
		
		_currentIndex++;
		CurrentLevel = Levels[_currentIndex]; 
		return true;
	}

	public bool PrevLevel()
	{
		if(_currentIndex - 1 < 0) return false;
		
		_currentIndex--;
		CurrentLevel = Levels[_currentIndex]; 
		return true;
	}

	public bool CanNextLevel{ get { return _currentIndex <= Levels.Length - 2; } }
	public bool CanPrevLevel{ get{ return _currentIndex >= 1; } }

	
	public Overdrivable GetCurrentLevel()
	{
		return _CurrentLevel;
	}
	public int GetCurrentIndex()
	{
		return _currentIndex;
	}

	public Overdrivable CurrentLevel
	{
		get{ return _CurrentLevel; }
		private set{ 
			if(_CurrentLevel != null) _CurrentLevel.enabled = false;
			_CurrentLevel = value;
			_CurrentLevel.enabled = true; 
		}
	}


}
