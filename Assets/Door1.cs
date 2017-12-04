using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door1 : MonoBehaviour {

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
				SceneManager.LoadScene("tut2");
			}
		}
	}	
}
