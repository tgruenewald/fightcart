using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour {
	
	public FightCart cart;
	public int health = 3;
	Shopper aggressor = null;
	Shopper defender = null;
	IEnumerator fightMatch(Shopper s1, Shopper s2) {
		yield return new WaitForSeconds (1f);
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

		aggressor.GetComponent<SpriteRenderer> ().flipY = true;
		defender.GetComponent<SpriteRenderer> ().flipY = false;
		defender.health--;
		if (defender.health <= 0 || aggressor.health <= 0) {
			// fight over
			Debug.Log("fight over: " + s1.cart.cartName + ", " + s2.cart.cartName);
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
		}
		else {
			StartCoroutine (fightMatch (s1,s2));
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
