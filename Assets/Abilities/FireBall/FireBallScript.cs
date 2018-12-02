using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class FireBallScript : MonoBehaviour {
	public float Speed = 10;
	public float DestroyDelay = 0.3f;
	public ExplosionScript Explosion;
	

	private Rigidbody2D _Rb;
	private SpriteRenderer _SpriteRend;

	private void Awake() {	
		_Rb = GetComponent<Rigidbody2D>();
		_SpriteRend = GetComponent<SpriteRenderer>();
		_SpriteRend.sortingLayerName = "Effect";
	}
	private void Start() {
		Launch(transform.right);
	}
	
	public void Launch(Vector2 dir)
	{
		_Rb.velocity = dir * Speed;
		float angle = Vector2.Angle(Vector2.right, dir);
		_Rb.rotation = angle;

		if(angle > 90) _SpriteRend.flipX = true;
	}

	private void _Explode()
	{
		_Rb.velocity = Vector2.zero;
		_SpriteRend.enabled = false;		
		Explosion.Activate();	
			
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
