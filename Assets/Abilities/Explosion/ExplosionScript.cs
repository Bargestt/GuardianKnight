using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class ExplosionScript : Effect {

	public float ExplosionRadius = 2f;
	public bool ScaleEffect = true;
	public LayerMask DamageMask;

	private Animator _Anim;

	private void Awake() {
		_Anim = GetComponent<Animator>();
		GetComponent<SpriteRenderer>().sortingLayerName = "Effect";
	}
	public override void Activate()
	{
		_Anim.SetTrigger("Explode");	

		if(ScaleEffect) transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, 1);	

		foreach(var obj in Physics2D.OverlapCircleAll(transform.position, ExplosionRadius, DamageMask))
		{
			var k = obj.GetComponent<Killable>();
			if(k != null)
			{
				k.ApplyDamage(Damage);				
			}
		}
	}
	
}
