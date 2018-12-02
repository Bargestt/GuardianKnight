using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRobot : Killable {

	public float FlySpeed = 1.5f;
	public GameObject Target;
	public GameObject ProjectilePrefab;

	public float DestroyDelay = 10;

	
	private Rigidbody2D _Rb;
	private SpriteRenderer _Rend;
	private Animator _Anim;

	public float ShootDelay = 5f;
	private float _LastShootTime;
	private bool LookingRight = true;	

	private float _FlyHeight;


	void Awake () {
		_Rb = GetComponent<Rigidbody2D>();
		_Rend = GetComponent<SpriteRenderer>();
		_Anim = GetComponent<Animator>();
	}

	private void Start() {
		Respawn();
		Target = GameObject.FindGameObjectWithTag("Player");

		_FlyHeight = transform.position.y;
	}
	
	private void FixedUpdate() {
		
		if(Target != null)
		{
			float dist = Target.transform.position.x - transform.position.x;
			if(Mathf.Abs(dist) > 4)
			{
				float dir  = Mathf.Sign(dist);
				_Rb.velocity = new Vector2(dir * FlySpeed, _Rb.velocity.y);

				if(dir < -0.1f) LookingRight = true; 
				if(dir > 0.1f) LookingRight = false;
				_Rend.flipX = LookingRight;		
			}
		}
		else{
			_Rb.velocity = new Vector2(0, 0);
		}	

		if(_Rb.position.y > _FlyHeight+1) _Rb.AddForce(Vector2.down * 1f);
		else if(_Rb.position.y < _FlyHeight-1) _Rb.AddForce(Vector2.up * 1f);
		
		_LastShootTime += Time.deltaTime;
		if(_LastShootTime > ShootDelay)
		{
			Shoot();
			_LastShootTime = 0;
		}
	}

	protected override void OnDie()
	{
		_Anim.SetTrigger("Death");		
		
		GetComponent<Collider2D>().enabled = false;	
		_Rb.simulated = false;
		enabled = false;
		Target = null;
		_Rb.velocity = new Vector2(0, -2);	
		Destroy(gameObject, 1);
	}

	private void Shoot()
	{
		if(Target == null) return;

		Vector2 dir = Target.transform.position - transform.position;
		float angle = Vector2.SignedAngle(Vector2.right, dir);
		GameObject.Instantiate( ProjectilePrefab, 
								transform.position, 
								Quaternion.Euler(0, 0, angle)
							  );	
	}
}
