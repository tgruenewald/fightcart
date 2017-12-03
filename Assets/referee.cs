using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour {
	Vector3 offsetVector = new Vector3(.3f,0,0);
	public void start_fight(GameObject cart1, GameObject cart2) {

		FightCart c1 = cart1.GetComponent<FightCart> ();			
		FightCart c2 = cart2.GetComponent<FightCart> ();
		if (c1 == null) {
			Debug.Log(cart1.name + "is null");
		}
		if (c2 == null) {
			Debug.Log(cart2.name + "is null");
		}		
		if (!c1.coolingDown && !c2.coolingDown && !c1.inFight && !c2.inFight) {
			Debug.Log ("Starting fight");
			c1.inFight = true;
			c2.inFight = true;
			// cart1.GetComponent<Rigidbody2D> ().isKinematic = true;
			cart1.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;

			cart2.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
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
			var shopper1 = (GameObject) Instantiate(Resources.Load("prefab/shopper"), loc.position + offsetVector, loc.transform.rotation) ;
			var shopper2 = (GameObject) Instantiate(Resources.Load("prefab/shopper"), loc.position - offsetVector, loc.transform.rotation) ;

			shopper2.GetComponent<SpriteRenderer> ().flipX = true;
			shopper1.GetComponent<Shopper> ().cart = c1;
			shopper2.GetComponent<Shopper> ().cart = c2;

			// the fewer items, the stronger you are			
			shopper1.GetComponent<Shopper> ().health = 6 - cart1.GetComponent<FightCart>().inventory.Count;
			shopper2.GetComponent<Shopper> ().health = 6 - cart2.GetComponent<FightCart>().inventory.Count;

			if (shopper1.GetComponent<Shopper> ().health <= 0) {
				shopper1.GetComponent<Shopper> ().health = Random.Range(1,2);
			}
			if (shopper2.GetComponent<Shopper> ().health <= 0) {
				shopper2.GetComponent<Shopper> ().health = Random.Range(1,2);
			}

			shopper1.GetComponent<Shopper> ().fight (shopper2.GetComponent<Shopper> ());
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
