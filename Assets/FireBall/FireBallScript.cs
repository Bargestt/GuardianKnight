using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {

	public float Speed = 10;
	public float DestroyDelay = 0.3f;
	
	private Animator _Anim;

	private Rigidbody2D _Rb;

	private void Awake() {
		_Anim = GetComponent<Animator>();
		_Rb = GetComponent<Rigidbody2D>();
	}
	private void Start() {
		Launch(transform.right);
	}
	
	public void Launch(Vector2 dir)
	{
		_Rb.velocity = dir * Speed;
		float angle = Vector2.Angle(Vector2.right, dir);
		_Rb.rotation = angle;

		if(angle > 90) GetComponent<SpriteRenderer>().flipX = true;
	}

	private void _Explode()
	{
		_Rb.velocity = Vector2.zero;
		_Anim.SetTrigger("Explode");

		Destroy(gameObject, DestroyDelay);
	}	

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Item"))
		{
			Physics2D.IgnoreCollision(other.collider, other.otherCollider);
			return;
		}

		float angle = Vector2.SignedAngle(Vector2.right,  other.GetContact(0).normal) - 90;		
		_Rb.rotation = angle;
		
		_Explode();
	}
}
