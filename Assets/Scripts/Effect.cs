using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour {
	public float Damage = 100f;

	public abstract void Activate();	
}
