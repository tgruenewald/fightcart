using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public float speed = 0.5f;
	public bool isConnected = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(howFarAwayFromParent());
	}
	IEnumerator howFarAwayFromParent() {
		yield return new WaitForSeconds (0.2f);
		if (gameObject.transform.parent != null) {
			float distance = Vector3.Distance (transform.position, gameObject.transform.parent.position);
			if (distance > 2f) {
				Debug.Log("bye bye");
				gameObject.transform.parent = null;
				isConnected = false;
			}
		}
		if (!isConnected) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
			if (hit.collider != null) {
					// Debug.Log("hit ray: "+ hit.collider.gameObject.name);
					if (hit.collider.gameObject.tag == "cart") {
						// Debug.Log("move to cart");
					transform.position = Vector3.MoveTowards(transform.position,hit.collider.gameObject.transform.position, speed);			
					//			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
					//			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
					//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
					float distance = Vector3.Distance (transform.position,  hit.collider.gameObject.transform.position);
					// Debug.Log("dist: " + distance + ", " + cartDistance);
					if (distance < 0.01f) {
						// parent the item and stop raycast
						// hit.collider.gameObject.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
						gameObject.transform.parent = hit.collider.gameObject.transform;
						isConnected = true;
					}
				}
			}
		}		

		StartCoroutine(howFarAwayFromParent());
	}
	// Update is called once per frame
	void FixedUpdate () {


	}
}
