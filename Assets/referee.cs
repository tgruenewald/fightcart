using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class referee : MonoBehaviour {
	Vector3 offsetVector = new Vector3(1,0,0);
	public void start_fight(GameObject cart1, GameObject cart2) {

		enemy_cart c1 = cart1.GetComponent<enemy_cart> ();			
		enemy_cart c2 = cart2.GetComponent<enemy_cart> ();
		if (!c1.inFight && !c2.inFight) {
			Debug.Log ("Starting fight");
			c1.inFight = true;
			c2.inFight = true;
			Transform c1fl = c1.transform.FindChild ("fight_left");
			Transform c1fr = c1.transform.FindChild ("fight_right");
			Transform c2fl = c2.transform.FindChild ("fight_left");
			Transform c2fr = c2.transform.FindChild ("fight_right");
			// find smallest x
			float x = -1;
			Transform loc = c1fl;
			if (c1fl.position.x < x) {
				x = c1fl.position.x;
				loc = c1fl;
			}
			if (c1fr.position.x < x) {
				x = c1fr.position.x;
				loc = c1fr;
			}
			if (c2fl.position.x < x) {
				x = c2fl.position.x;
				loc = c2fl;
			}
			if (c2fr.position.x < x) {
				x = c2fr.position.x;
				loc = c2fr;
			}
			var shopper1 = (GameObject) Instantiate(Resources.Load("prefab/shopper"), loc.position + offsetVector, c1.transform.rotation) ;
			var shopper2 = (GameObject) Instantiate(Resources.Load("prefab/shopper"), loc.position - offsetVector, c1.transform.rotation) ;
			shopper2.GetComponent<SpriteRenderer> ().flipX = true;
		}


	}
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
