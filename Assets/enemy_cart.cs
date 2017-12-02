using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cart : MonoBehaviour {
	Transform targetCart;
	bool followCart = false;
	float speed = .05f;
	float cartDistance = 2f;
	float fightDistance = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator fightCheck() {
		yield return new WaitForSeconds (2f);
		float distance = Vector3.Distance (transform.position, targetCart.position);
		if (distance < cartDistance) {
			Debug.Log ("Fight!!");
			var shopper = (GameObject) Instantiate(Resources.Load("prefab/shopper"), GetComponent<Transform>().position, GetComponent<Transform>().rotation) ;
		}
		else {
			StartCoroutine (fightCheck ());	
		}

	}

	IEnumerator cartAggroed() {
		yield return new WaitForSeconds (0.5f);
		followCart = true;
		GetComponent<SpriteRenderer> ().flipY = false;
	}
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "cart") {
			Debug.Log ("Cart is aggroed");
			GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.red);
			GetComponent<SpriteRenderer> ().flipY = true;
			StartCoroutine (cartAggroed ());
			targetCart = coll.transform;
			StartCoroutine (fightCheck ());
		}
	}

	void FixedUpdate () {

		if (followCart) {
			transform.position = Vector3.MoveTowards(transform.position, targetCart.position, speed);			
			//			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			//			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
			//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			float distance = Vector3.Distance (transform.position, targetCart.position);
			if (distance < cartDistance) {
				Debug.Log ("Cart caught");
				GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				followCart = false;

				// and they fight
			}
		}


	}
}
