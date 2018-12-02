using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	private static Score _Instance;
	public static Score Instance{get{return _Instance;}}

	public static void IncreaseStatic(int amount){
		_Instance.Increase(amount);		
	}

	public int CurScore{ get; private set;}
	public UnityEngine.UI.Text Display;

	private void Awake() {
		CurScore = 0;
		if(_Instance == null) _Instance = this;
		else
			Destroy(this);
	}

	public void Increase(int amount)
	{
		CurScore++;
		if(Display != null)
		{
			Display.text = CurScore.ToString();
		}
	}
}
