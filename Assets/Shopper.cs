using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour {
	
	public FightCart cart;
	public int health = 3;
	Shopper aggressor = null;
	Shopper defender = null;

	public GameObject hat;
	IEnumerator fightMatch(Shopper s1, Shopper s2) {
		yield return new WaitForSeconds (.2f);
		if (aggressor == null) {
			aggressor = s1;
			defender = s2;
		}
		if (aggressor == s1) {
			aggressor = s2;
			defender = s1;
		}
		else {
			aggressor = s1;
			defender = s2;
		}

		Debug.Log("FIGHT STATUS: " + defender.cart.cartName + ", " + aggressor.cart.cartName + " d: " + defender.health + ", a:" + aggressor.health);
		defender.cart.playPunch();
		aggressor.cart.playPunch();
		// aggressor.GetComponent<SpriteRenderer> ().flipY = true;
		// defender.GetComponent<SpriteRenderer> ().flipY = false;
		// if (defender.cart.iAmEnemy)  // TODO: remove later this way I win all fights
			defender.health--;
		if (defender.health <= 0 || aggressor.health <= 0) { 
			// fight over
			Debug.Log("fight over: " + defender.cart.cartName + ", " + aggressor.cart.cartName + " d: " + defender.health + ", a:" + aggressor.health);

			if (defender.health > 0) {
				Debug.Log("Defender wins: " + defender.cart.cartName);
				var item = aggressor.cart.takeNeededItem(defender.cart.wishList);
				Debug.Log("Taking item: " + item);
				defender.cart.addInventory(item);				
			}
			else {
				Debug.Log("aggressor wins: " + aggressor.cart.cartName);
				var item = defender.cart.takeNeededItem(aggressor.cart.wishList);
				Debug.Log("Taking item: " + item);
				aggressor.cart.addInventory(item);
			}
			s1.cart.inFight = false;
			s2.cart.inFight = false;
			s1.cart.coolDown ();
			s2.cart.coolDown ();
			s1.cart.postFight();
			s2.cart.postFight();
			Vector3 moveAway = Random.onUnitSphere * 3f;
			Debug.Log("move away: " + moveAway);
			s2.cart.GetComponent<Rigidbody2D> ().velocity = moveAway;
			s1.cart.GetComponent<Rigidbody2D> ().velocity = -moveAway;
			Destroy(s1.gameObject);
			Destroy(s2.gameObject);
			s1.cart.shopper.GetComponent<SpriteRenderer>().enabled = true;
			s2.cart.shopper.GetComponent<SpriteRenderer>().enabled = true;
			s1.cart.hat.GetComponent<SpriteRenderer>().enabled = true;
			s2.cart.hat.GetComponent<SpriteRenderer>().enabled = true;
		}
		else {
			StartCoroutine (fightMatch (s1,s2));
		}


	}
	public virtual void setHealth() {
		health = 6 - cart.GetComponent<FightCart>().inventory.Count;
		if (health <= 0) {
			health = Random.Range(1,2);
		}		

	}
	public void fight(Shopper s2) {
		StartCoroutine (fightMatch (this,s2));
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
