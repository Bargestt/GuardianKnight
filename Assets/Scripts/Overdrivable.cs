using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Overdrivable : MonoBehaviour {	
	public OverdriveMode Mode = OverdriveMode.Multiplier;

	public float OverdriveMultiplier = 3;
	public float CrippleMultiplier = 2;

	public abstract void Overdrive();
	public abstract void Normalize();
	public abstract void Cripple();
	

	public float OverdriveFloat(float normalFloat)
	{
		return normalFloat * OverdriveMultiplier;
	}	
	public float NormalizeFloat(float overdrivenFloat)
	{
		return overdrivenFloat / OverdriveMultiplier;
	}	
	public float CrippleFloat(float normalFloat)
	{
		return normalFloat / CrippleMultiplier;
	}
} 

