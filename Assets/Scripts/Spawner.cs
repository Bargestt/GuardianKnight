using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour {

	[System.Serializable]
	public class SpawnParams
	{
		public GameObject Prefab;
		public float RespawnTime;
		public int MaxActive;
		public bool GroundOnly;
	}
	public bool Once = false;
	public float StartDelay = 0;

	public GameObject[] SpawnPoints;

	[SerializeField]
	public SpawnParams SpawnParameters;
	private float _TimeFromLastRespawn;
	private float _CurActive = 0;
	private float _TotalSpawned;

	// Update is called once per frame
	void Update () {
		if(StartDelay > 0)
		{
			StartDelay -= Time.deltaTime;
			return;
		}

		if(_TimeFromLastRespawn < SpawnParameters.RespawnTime)
		{
			_TimeFromLastRespawn += Time.deltaTime;
		}
		else
		{
			_TrySpawn();
		}
	}

	private void _TrySpawn()
	{
		if(_CurActive > SpawnParameters.MaxActive) return;

		if(SpawnParameters.Prefab != null)
		{
			GameObject obj = GameObject.Instantiate(SpawnParameters.Prefab, transform);
			obj.transform.position = _SelectPoint(SpawnParameters.GroundOnly);
			_CurActive++;
			_TotalSpawned++;
			_Register(obj);
		}

		if(Once && _TotalSpawned >= SpawnParameters.MaxActive)
		{
			this.enabled = false; 
		}

		_TimeFromLastRespawn = 0;
	}

	private Vector3 _SelectPoint(bool ground = true)
	{
		Vector3 point;
		if(SpawnPoints.Length > 0)
			point = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;
		else
			point = transform.position;

		if(ground)
		{
			RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down);
			if(hit.collider != null)
				return hit.point;
			else
				return point;
		}
		else{
			return point;
		}
	}
	private void _SpawnedDied()
	{
		_CurActive--;
	}


	private void _Register(GameObject obj)
	{
		Killable killable = obj.GetComponent<Killable>();
		if(killable == null)
		{
			obj.AddComponent<Killable>();
		}
		killable.OnDeath += _SpawnedDied;
		
	}

	private void OnDestroy() {
		//Just in case
		foreach(Transform tr in transform)
		{
			Killable k = tr.GetComponent<Killable>();
			if(k != null)
			{
				k.OnDeath -= _SpawnedDied;
			}
		}
	}
}
