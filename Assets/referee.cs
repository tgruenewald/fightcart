using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour {
	Vector3 whichSideVector = new Vector3(.3f,0,0);
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
			var shopper1 = c1.create_shopper(c1fl, whichSideVector);
			var shopper2 = c2.create_shopper(c1fl, -whichSideVector);

			shopper1.GetComponent<SpriteRenderer> ().flipX = true;
			shopper1.GetComponent<Shopper> ().cart = c1;
			shopper2.GetComponent<Shopper> ().cart = c2;

			// color the hats
			shopper1.GetComponent<Shopper> ().hat.GetComponent<SpriteRenderer>().color = c1.hatColor;
			shopper2.GetComponent<Shopper> ().hat.GetComponent<SpriteRenderer>().color = c2.hatColor;
			c1.shopper.GetComponent<SpriteRenderer>().enabled = false;
			c2.shopper.GetComponent<SpriteRenderer>().enabled = false;
			c1.shopper.GetComponent<Animator>().SetBool("isAggro", false);
			c2.shopper.GetComponent<Animator>().SetBool("isAggro", false);
			c1.hat.GetComponent<SpriteRenderer>().enabled = false;
			c2.hat.GetComponent<SpriteRenderer>().enabled = false;
			shopper1.transform.localScale = c1.shopper.transform.localScale;
			shopper2.transform.localScale = c2.shopper.transform.localScale;

			// the fewer items, the stronger you are			
			shopper1.GetComponent<Shopper> ().setHealth();
			shopper2.GetComponent<Shopper> ().setHealth();


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
