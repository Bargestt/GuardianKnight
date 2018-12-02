using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class AbilityState : MonoBehaviour {

	public AbilityOverdrivable Target;
	public Slider Level;
	public Image SliderFill;
	public Color NormalState = Color.green; 
	public Color OverdrivenState = Color.yellow;
	public Color CrippledState = Color.red;

	public Image Button;

	public Sprite NormalSprite;
	public Sprite InProgressSprite;
	public Sprite UnAvailableSprite;
	
	// Update is called once per frame
	void Update () {
		if(Target == null) return;

		Level.value = Target.GetCurrentIndex()+1;
		
		Level.maxValue = Target.Levels.Length; 
		
		OverdriveState state = Target.GetState();
		if(state == OverdriveState.Available)
		{
			SliderFill.color = NormalState;
			Button.sprite = NormalSprite;
		}
		else if(state == OverdriveState.InProgress)
		{
			SliderFill.color = OverdrivenState;
			Button.sprite = InProgressSprite;
		}
		else if(state == OverdriveState.Unavailable)
		{ 
			SliderFill.color = CrippledState; 
			Button.sprite = UnAvailableSprite; 
			Level.value = 0;
		}
		
	}
}
