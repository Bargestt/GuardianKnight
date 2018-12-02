using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class SwordWind : Effect {
	private Animator _Anim;
	private ContactFilter2D _Filter;
	private Collider2D _Collider;

	private void Awake() {
		_Anim = GetComponent<Animator>();
		_Collider = GetComponent<Collider2D>();
		_Filter = new ContactFilter2D();
		_Filter.SetLayerMask(LayerMask.GetMask("Enemy"));
	}

	public override void Activate()
	{
		_Anim.SetTrigger("Slash");

		Collider2D[] points = new Collider2D[10];		
		int size = _Collider.OverlapCollider(_Filter, points);
		
		for(int i = 0; i < size; i++)
		{
			var k  = points[i].GetComponent<Killable>();
			if(k != null)
			{
				k.ApplyDamage(Damage);
			}
		}
	}
}
