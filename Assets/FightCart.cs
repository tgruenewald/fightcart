using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCart : MonoBehaviour {
	Transform targetCart = null;
	Transform targetItem = null;
	bool followCart = false;
	bool firstContact = false;
	public float speed = .05f;
	public float cartDistance = .5f;

	public float giveUpDistance = 4f;
	public string cartName = "other";
	public bool coolingDown = false;

	public bool inFight = false;
	GameObject collidedCart;

	public bool iAmEnemy = true;

	private IEnumerator fightTimer;

	public Queue<Transform> itemQueue = new Queue<Transform>();

	public string[] bubble = {"s", "t", "c"};
	public string[] wantedItem = {"square", "triangle", "circle"};
	public int wantedItemIndex = 0;

	// Use this for initialization
	void Start () {
		// fightTimer = fightCheck();
		// nextWantedItem() ;
		selectWantedItem();

		//GameObject.Find(wantedItem[wantedItemIndex]).GetCo//mponent<SpriteRenderer>().enabled = true;
	}


	public SpriteRenderer getBubble() {
		foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
			// Debug.Log("c = " + c.name);
			if (c.name == bubble[wantedItemIndex]) {
				Debug.Log("found bubble: " + c.name);
				return c;
			}
		}
		return null;
	}
	public void selectWantedItem() {
		Debug.Log("wanted item: " + bubble[wantedItemIndex]);
		foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
			// Debug.Log("c = " + c.name);
			if (c.name == bubble[wantedItemIndex]) {
				// Debug.Log("enabling");
				c.enabled = true;
				//c.color = Color.red; //material.SetColor ("_Color", Color.red);
			}

		}
	}
	public void nextWantedItem() {
		wantedItemIndex = Random.Range(0,3);
		Debug.Log("wanted item: " + bubble[wantedItemIndex]);
		selectWantedItem();
	}
	public bool hasExactItem(string neededItem) {
		foreach(Component c in GetComponentsInChildren<Component>()) {
			// Debug.Log("hasItem = " + c.name);
			if (c.name.Equals(neededItem)) {
				Debug.Log("EXACT MATCH!!!");
				return true;
			}
		}
		return false;
	}
	public bool hasItem(string neededItem) {
		foreach(Component c in GetComponentsInChildren<Component>()) {
			// Debug.Log("hasItem = " + c.name);
			if (c.name.Contains(neededItem)) {
				Debug.Log("MATCH!!!");
				return true;
			}
		}
		return false;
	}

	public void clearLastItem() {
		foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
			if (c.name == bubble[wantedItemIndex]) {
				c.enabled = false;
			}

		}		
	}
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator postFightTimer() {
        yield return new WaitForSeconds (1f);
        Debug.Log("Stopping cart " + cartName);
        GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
        // StartCoroutine(normalShoppingTimer());
    }

    public void postFight() {
		firstContact = false;
        StartCoroutine (postFightTimer ());

    }



	public void coolDown() {
		// GetComponent<Rigidbody2D> ().isKinematic = false;
		StartCoroutine (coolDownCoRoutine ());
	}
	IEnumerator coolDownCoRoutine() {
		coolingDown = true;
		targetItem = null;
		// itemQueue.Clear();
		yield return new WaitForSeconds (3f);
		coolingDown = false;
	}
	IEnumerator fightCheck() {
		Debug.Log("Starting fight check");
		yield return new WaitForSeconds (2f);
		float distance = Vector3.Distance (transform.position, targetCart.position);
		if (distance < cartDistance && !inFight) {
			// At this point i need to contact another game object that will start the fight.
			// To do this, each cart will need its id checked to make sure that it isn't already 
			// part of the fight.  This leaves the question what happens when 3 or more carts 
			// potentially enter into a fight?  Since only 2 carts can fight at once this 
			// will need to be resolved by the arbitrator.
			targetItem = null;
			var referee = GameObject.FindGameObjectWithTag ("referee");
			referee.GetComponent<Referee> ().start_fight (gameObject, collidedCart);

			Debug.Log ("Fight!!");

		}
		// else {
		// 	StartCoroutine (fightCheck ());	
		// }

	}
	public void loseItem() {
		Transform[] trans = GetComponentsInChildren<Transform>();
		Debug.Log("Loser is " + cartName);
		if (trans.Length > 0) {
			foreach( Transform t in trans) {
				if (t.tag == "item") {
					Debug.Log("removing item");
					// t.GetComponent<Rigidbody2D>().isKinematic = false;
					t.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 30f, ForceMode2D.Impulse);
					break;
				}
			}
		}
	}


	IEnumerator packageFlying(Transform t) {
		yield return new WaitForSeconds (1f);
		t.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	IEnumerator cartAggroed() {
		yield return new WaitForSeconds (0.5f);
		followCart = true;
		// itemQueue.Clear();
		getBubble().color =  Color.white;
		// GetComponent<SpriteRenderer> ().flipY = false;
	}
	void OnTriggerEnter2D(Collider2D coll){
		// Debug.Log("coll.gameObject.tag: " + coll.gameObject.name);
		if (iAmEnemy && coll.gameObject.tag == "item") {
			// Debug.Log(cartName + ":  coll.gameObject.tag: " + coll.gameObject.name);
			itemQueue.Enqueue(coll.gameObject.transform);
		}
		if (iAmEnemy && !followCart && targetItem == null && !coolingDown && !inFight) {
			// move to item
			Debug.Log("Moving toward item");
			if (itemQueue.Count > 0 && targetItem == null) {
				targetItem = itemQueue.Dequeue();
			}				
		}

		if (iAmEnemy && coll.gameObject.tag == "cart" && coll.gameObject.GetComponent<FightCart>().hasItem(wantedItem[wantedItemIndex]) && !inFight) {
			// StopAllCoroutines();
			firstContact = true;
			collidedCart = coll.gameObject;
			Debug.Log ("Cart("+cartName+") is aggroed by " + collidedCart.GetComponent<FightCart>().cartName);
			getBubble().color =  Color.red;
			// GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.red);
			// GetComponent<SpriteRenderer> ().flipY = true;
			StartCoroutine (cartAggroed ());
			targetCart = coll.transform;
			StartCoroutine (fightCheck ());
		}
	}

	void FixedUpdate () {
		if (iAmEnemy && !followCart && !inFight) {
			// if (targetItem != null) Debug.Log("targeetItem = " +  targetItem.name + ", " + targetItem.name.Contains(wantedItem[wantedItemIndex]));
			if (targetItem != null && !targetItem.name.Contains(wantedItem[wantedItemIndex])) {
				// Debug.Log("Removing item from queue");
				if (itemQueue.Count > 0) {
					itemQueue.Enqueue(targetItem); // put this at the end for later
					targetItem = itemQueue.Dequeue();	
				}				
				else {
					targetItem = null;
				}

			}
			if (targetItem != null && targetItem.name.Contains(wantedItem[wantedItemIndex])) {
				transform.position = Vector3.MoveTowards(transform.position, targetItem.position, speed);			
				float distance = Vector3.Distance (transform.position, targetItem.position);
				// Debug.Log("dist to item: " + distance + ", " + cartDistance);
				// transform.position = Vector3.MoveTowards(transform.position, targetCart.position, speed);			
				if (hasExactItem(targetItem.name)) {
					Debug.Log("FOUND ITEM: " + targetItem.name);
					clearLastItem();
					nextWantedItem();
					if (itemQueue.Count > 0) {
						targetItem = itemQueue.Dequeue();	
					}
					else {
						targetItem = null;
					}										
				}
				if (distance < 0.1f) {
					if (itemQueue.Count > 0) {
						targetItem = itemQueue.Dequeue();	
					}
					else {
						targetItem = null;
					}
										

				}	
			}
		
		}
		if (iAmEnemy && followCart) {
			transform.position = Vector3.MoveTowards(transform.position, targetCart.position, speed);			
			float distance = Vector3.Distance (transform.position, targetCart.position);
			// Debug.Log("dist: " + distance + ", " + cartDistance);
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

		// check for closeness to last fight
		if (!firstContact && !followCart && !coolingDown && targetCart != null) {
			float distance = Vector3.Distance (transform.position, targetCart.position);	
			if (distance < cartDistance) {
				coolDown();
				Vector3 moveAway = Random.onUnitSphere * 3f;
				Debug.Log("move away again: " + moveAway);
				GetComponent<Rigidbody2D> ().velocity = moveAway;				
			}

		}



	}
}
