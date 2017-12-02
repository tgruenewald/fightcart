using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : MonoBehaviour {
	public float speed = .2f;
	private Vector3 targetPosition;
	private bool isMousePositionSet = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate () {


		if (Input.GetMouseButtonDown (0)) {
			//targetPosition = Input.mousePosition;
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			targetPosition.z = transform.position.z; // so it stays on the same z axis
			isMousePositionSet = true;
		}
		if (isMousePositionSet) {
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);			
//			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
//			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
			//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			float distance = Vector3.Distance (transform.position, targetPosition);
			if (distance < 0.1f) {
				isMousePositionSet = false;
			}
		}


	}
}
