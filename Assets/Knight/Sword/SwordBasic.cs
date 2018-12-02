using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SwordBasic : Overdrivable {

	public float SlashDamage = 100;
	public float SlashTimeout = 0.5f;
	public float SlashDuration = 0.2f;
	
	
	protected PlayerController _Controller;
	protected Player _Player;


	private bool _SlashInProgress = false;
	private float _TimeFromLastSlash;
	private BoxCollider2D _Collider;
	private ContactFilter2D _Filter;

	private float _NormalSlashTimeout;

	protected virtual void Awake() 
	{
		_Controller = FindObjectOfType<PlayerController>();	
		_Controller.OnFire += _OnFire;
		
		
		_Player = FindObjectOfType<Player>();		
		_Collider = GetComponent<BoxCollider2D>();

		_Filter = new ContactFilter2D();
		_Filter.SetLayerMask(LayerMask.GetMask("Enemy"));
		
		_NormalSlashTimeout = SlashTimeout;
	}
	private void Update() {		
		if(_TimeFromLastSlash > SlashTimeout)
		{
			_Player.Animation.ResetTrigger("Slash");
			_SlashInProgress = false;
		}
		else
		{
			_TimeFromLastSlash += Time.deltaTime;
			if(_SlashInProgress && _TimeFromLastSlash > SlashDuration)
			{
				_SlashInProgress = false;
				_SlashEnd();
			}			
		}
	}
	
	protected virtual void OnDisable() {
		_SlashInProgress = false;
	}
	private void OnDestroy() {
		_Controller.OnFire -= _OnFire;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(_SlashInProgress && other.CompareTag("Enemy"))
		{
			_ApplyDamage(other.gameObject);
		}		
	}

	
	private void _OnFire(int i)
	{
		if(!isActiveAndEnabled) return;

		if(i == 0 && _TimeFromLastSlash >= SlashTimeout)
		{ 		
			_TimeFromLastSlash = 0;	 
			_SlashInProgress = true;	
			_SlashStart();	
		}
	}	
	protected virtual void _SlashStart()
	{
		_Player.Animation.SetTrigger("Slash");		

		Collider2D[] points = new Collider2D[10];		
		int size = _Collider.OverlapCollider(_Filter, points);
		
		for(int i = 0; i < size; i++)
		{
			_ApplyDamage(points[i].gameObject);			
		}
	}
	protected virtual void _SlashEnd()
	{		
	}

	protected void _ApplyDamage(GameObject target)
	{
		var k  = target.GetComponent<Killable>();
		if(k != null)
		{
			k.ApplyDamage(SlashDamage);
		}		
	}


	public override void Overdrive()
	{
		SlashDamage = OverdriveFloat(SlashDamage);
		SlashTimeout = _NormalSlashTimeout/2;
	}
	public override void Normalize()
	{
		SlashDamage = NormalizeFloat(SlashDamage);
		SlashTimeout = _NormalSlashTimeout;
	}
	public override void Cripple()
	{
		SlashDamage = CrippleFloat(SlashDamage);
		SlashTimeout = _NormalSlashTimeout*2;
	}
	

	public bool SlashInProgerss{ get{ return _SlashInProgress; } }
}
