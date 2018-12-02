using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Killable {

	public float WalkSpeed = 1;
	public bool Walking = true;
	public GameObject Target;

	public Effect DeathEffect;
	private bool _Exploded = false;
	public float ExplosionDelay = 1f;
	public float DestroyDelay = 10;




	private Rigidbody2D _Rb;
	private SpriteRenderer _Rend;
	private Animator _Anim;
	
	private bool LookingRight = true;	

	private void Awake() {
		_Rb = GetComponent<Rigidbody2D>();
		_Rend = GetComponent<SpriteRenderer>();
		_Anim = GetComponent<Animator>();
	}
	private void Start() {
		Target = GameObject.FindGameObjectWithTag("Player");
		_Anim.SetBool("Walking", Walking);
		Respawn();
	}

	private void FixedUpdate() {
		if(Target != null && Walking)
		{
			float dir  = Mathf.Sign(Target.transform.position.x - transform.position.x);
			_Rb.velocity = new Vector2(dir * WalkSpeed, _Rb.velocity.y);

			if(dir < -0.1f) LookingRight = true; 
			if(dir > 0.1f) LookingRight = false;
			_Rend.flipX = LookingRight;		
		}
		else{
			_Rb.velocity = new Vector2(0, _Rb.velocity.y);
		}		
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Enemy"))
		{
			Physics2D.IgnoreCollision(other.collider, other.otherCollider);
		}

		if(other.collider.CompareTag("Player"))
		{
			Walking = false;
			_Anim.SetBool("Walking", Walking);
			if(!_Exploded)
				StartCoroutine(_ExplodeDelayed(ExplosionDelay));
		}
		
	}

	private void _Explode()
	{
		if(_Exploded) return;
		
		DeathEffect.Activate();
		
		Die();
	}
	protected override void OnDie()
	{
		_Anim.SetTrigger("Death");		
		_Exploded = true;
		GetComponent<Collider2D>().enabled = false;	
		_Rb.simulated = false;

		Destroy(gameObject, DestroyDelay);
	}

	private IEnumerator _ExplodeDelayed(float time)
	{
		yield return new WaitForSeconds(time);
		_Explode();
	}
	
}
