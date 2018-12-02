using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Enemy"))
		{
			Physics2D.IgnoreCollision(other.collider, other.otherCollider);
		}
	}
}
