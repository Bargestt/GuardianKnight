using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public Killable Target;

	public UnityEngine.UI.Slider Slider;

	// Use this for initialization
	void Start () {
		if(Target == null)
		{
			Target = transform.GetComponentInParent<Killable>();
		}
		if(Slider == null)
		{
			Slider = transform.GetComponentInChildren<UnityEngine.UI.Slider>();
		}

		if(Target != null){
			Target.OnDamage += _UpdateValue;
			Target.OnDeath += Disable;			
		}
	}

	private void LateUpdate() {
		transform.eulerAngles = new Vector3(0,0,0);
	}

	public void UpdateValue()
	{
		Slider.value = Target.CurrentHP / Target.MaxHP;
	}
	
	private void _UpdateValue(float dmg)
	{
		UpdateValue();
	}
	public void Disable()
	{
		gameObject.SetActive(false);
	}

	private void OnDestroy() {
		if(Target != null){
			Target.OnDamage -= _UpdateValue;
			Target.OnDeath -= Disable;			
		}
	}

}
