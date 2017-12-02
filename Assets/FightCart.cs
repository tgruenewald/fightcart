using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCart : MonoBehaviour {
	Transform targetCart;
	bool followCart = false;
	public float speed = .05f;
	public float cartDistance = 2f;

	public float giveUpDistance = 10f;
	public string cartName = "other";
	public bool coolingDown = false;

	public bool inFight = false;
	GameObject collidedCart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (followCart) {
			transform.position = Vector3.MoveTowards(transform.position, targetCart.position, speed);			
			//			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
			//			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
			//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			float distance = Vector3.Distance (transform.position, targetCart.position);
			Debug.Log("dist: " + distance);
			if (distance < cartDistance) {
				Debug.Log ("Cart caught");
				GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.white);
				followCart = false;

				// and they fight
			}

			if (distance > giveUpDistance) {
				// too far, give up
				Debug.Log("too far, give up: " + distance + ", " + giveUpDistance);
				followCart = false;
			}
		}		
	}
	public void coolDown() {
		// GetComponent<Rigidbody2D> ().isKinematic = false;
		StartCoroutine (coolDownCoRoutine ());
	}
	IEnumerator coolDownCoRoutine() {
		coolingDown = true;
		yield return new WaitForSeconds (3f);
		coolingDown = false;
	}
	IEnumerator fightCheck() {
		yield return new WaitForSeconds (2f);
		float distance = Vector3.Distance (transform.position, targetCart.position);
		if (distance < cartDistance && !inFight) {
			// At this point i need to contact another game object that will start the fight.
			// To do this, each cart will need its id checked to make sure that it isn't already 
			// part of the fight.  This leaves the question what happens when 3 or more carts 
			// potentially enter into a fight?  Since only 2 carts can fight at once this 
			// will need to be resolved by the arbitrator.

			var referee = GameObject.FindGameObjectWithTag ("referee");
			referee.GetComponent<Referee> ().start_fight (gameObject, collidedCart);

			inFight = true;
			Debug.Log ("Fight!!");

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
			collidedCart = coll.gameObject;
			Debug.Log ("Cart is aggroed");
			GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.red);
			GetComponent<SpriteRenderer> ().flipY = true;
			StartCoroutine (cartAggroed ());
			targetCart = coll.transform;
			StartCoroutine (fightCheck ());
		}
	}

	void FixedUpdate () {




	}
}
