using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D coll){
		// Debug.Log("coll.gameObject.tag: " + coll.gameObject.name);
		if (coll.gameObject.tag == "cart") {
			if (coll.gameObject.GetComponent<FightCart>().winner) {
				Debug.Log("GAME OVER");
				if (coll.gameObject.GetComponent<FightCart>().iAmEnemy) {
					Debug.Log("YOU LOSE");
				}
				else {
					Debug.Log("YOU WIN");
				}
			}
		}
	}	
}
