using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCart : MonoBehaviour {
	Transform targetCart = null;
	Transform targetItem = null;
	bool followCart = false;
	bool firstContact = false;
	public float speed = .05f;
	public float cartDistance = 2f;

	public float giveUpDistance = 4f;
	public string cartName = "other";
	public bool coolingDown = false;

	public bool inFight = false;
	GameObject collidedCart;

	public bool iAmEnemy = true;

	private IEnumerator fightTimer;

	public Queue<Transform> itemQueue = new Queue<Transform>();

	public string[] itemList2 = {"blue_circle", "blue_square", "blue_triangle", "cross", "doll"};

	public GameObject[] wishList = new GameObject[3];
	public ArrayList inventory = new ArrayList();
	Vector3[] offsetVector = {new Vector3(.3f,.35f,0), new Vector3(0f,.35f,0), new Vector3(-.3f,.35f,0)};
	private List<int> uniqueNumbers;
	private List<int> finishedList;
	// Use this for initialization
	void Start () {
	createWishList();
	createInventory();
	}

	public void updateWishList(string itemName) {
		if (itemName != null){
			foreach (GameObject go in wishList) {
				if (itemName.Contains(go.name)) {
					go.GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}

	// 					item.GetComponent<SpriteRenderer>().color = Color.red;

	public void addInventory(string inventoryItemName) {
		if (inventoryItemName != null){
			inventory.Add(inventoryItemName);
			foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
				if (c.name.Contains(inventoryItemName) && !c.enabled) {
					// Debug.Log("c.name.Contains(inventoryItemName) = " + c.name + " contains " + inventoryItemName);
					c.enabled = true;
					break;
				} 
			}
			redrawWishlist();		
		}

	}
	public string removeInventory(string itemName) {
		string removeItem = null;
			foreach (string inventoryItemName in inventory) {
				if (itemName.Contains(inventoryItemName)) {
					// Debug.Log("aitemName.Contains(inventoryItemName) = " + itemName + " contains " + inventoryItemName);
					removeItem = inventoryItemName;
					foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
						if (c.name.Contains(inventoryItemName) && c.enabled) {
							c.enabled = false;
							break;
						}
					}
					break; 
				}
			}
			// Debug.Log("1 removeItems is " + removeItem); 
			if (removeItem != null) {
				inventory.Remove(removeItem);
		
			}
			redrawWishlist();
			// Debug.Log("2 removeItems is " + removeItem);
			return removeItem;
				
	
	}

	public bool isInInventory(string itemName) {
		foreach (string inventoryItemName in inventory) {
			if (itemName.Contains(inventoryItemName)) {
				return true;
			}
		}
		return false;
	}

	public void redrawWishlist() {
		foreach (GameObject go in wishList) {
			if (isInInventory(go.name)) {
				go.GetComponent<SpriteRenderer>().color = Color.red;
			}
			else {
				go.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	public string takeNeededItem(GameObject[] neededItems) {
		string removeItem = null;
		foreach (GameObject item in neededItems) {
			// Debug.Log("checking:  " + item.name);
			removeItem = removeInventory(item.name);
			if (removeItem != null) {
				break;
			}


		}	
		// if there were no items, then just take one
		if (removeItem == null && inventory.Count > 0) {
			removeItem = (string) inventory[0];
			inventory.RemoveAt(0);
			foreach(SpriteRenderer c in GetComponentsInChildren<SpriteRenderer>()) {
				if (c.name.Contains(removeItem)) {
					c.enabled = false;
					break;
				}
			}						
		}
		return removeItem;
	}
	public bool hasNeededItem(GameObject[] neededItems) {
		foreach (GameObject item in neededItems) {
			// Debug.Log("checking:  " + item.name);
			foreach (string inventoryItemName in inventory) {
				if (item.name.Contains(inventoryItemName)) {
					return true;
				}
			}
		}
		return false;
	}
	public void createInventory() {
		for( int i = 3; i < 5; i++) {
			addInventory(itemList2[finishedList[i]]);	
		}
		
	}

public void createWishList() {
	// randomly generate wish list
	uniqueNumbers = new List<int>();
	finishedList = new List<int>();
	GenerateRandomList();
	for (int i = 0; i < 3;i++) {
		// Debug.Log(itemList2[finishedList[i]]);
		var wish = (GameObject) Instantiate(Resources.Load("prefab/" + itemList2[finishedList[i]]), transform.position + offsetVector[i], transform.rotation) ;	
		wish.transform.parent = transform;
		wishList[i] = wish;
	}	
}
 public void GenerateRandomList(){
    for(int i = 0; i < 5; i++){
       uniqueNumbers.Add(i);
    }
    for(int i = 0; i< 5; i ++){
      int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
      finishedList.Add(ranNum);
      uniqueNumbers.Remove(ranNum);
    } 
    //Done.
 }
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator postFightTimer() {
        yield return new WaitForSeconds (1f);
        // Debug.Log("Stopping cart " + cartName);
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
		// Debug.Log("Starting fight check");
		yield return new WaitForSeconds (2f);
		float distance = Vector3.Distance (transform.position, targetCart.position);
		// Debug.Log("distance: " + distance + " , " + cartDistance);
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
		// TODO: randomly select invetory item to lose 
	}



	IEnumerator cartAggroed() {
		yield return new WaitForSeconds (0.5f);
		followCart = true;

	}
	void OnTriggerEnter2D(Collider2D coll){
		// Debug.Log("coll.gameObject.tag: " + coll.gameObject.name);
		if (iAmEnemy && coll.gameObject.tag == "item") {
			// Debug.Log(cartName + ":  coll.gameObject.tag: " + coll.gameObject.name);
			itemQueue.Enqueue(coll.gameObject.transform);
		}
		if (iAmEnemy && !followCart && targetItem == null && !coolingDown && !inFight) {
			// move to item
			// Debug.Log("Moving toward item");
			if (itemQueue.Count > 0 && targetItem == null) {
				targetItem = itemQueue.Dequeue();
			}				
		}


		if ( (coll.gameObject.tag == "cart" &&  coll.gameObject.GetComponent<FightCart>().hasNeededItem(wishList) && !inFight )) {
			// StopAllCoroutines();
			firstContact = true;
			collidedCart = coll.gameObject;
			// Debug.Log ("Cart("+cartName+") is aggroed by " + collidedCart.GetComponent<FightCart>().cartName);
			
			// GetComponent<SpriteRenderer> ().material.SetColor ("_Color", Color.red);
			// GetComponent<SpriteRenderer> ().flipY = true;
			StartCoroutine (cartAggroed ());
			targetCart = coll.transform;
			StartCoroutine (fightCheck ());
		}
	}

	void FixedUpdate () {

		if (iAmEnemy && followCart && !inFight) {
			transform.position = Vector3.MoveTowards(transform.position, targetCart.position, speed);			
			float distance = Vector3.Distance (transform.position, targetCart.position);
			// Debug.Log("dist: " + distance + ", " + cartDistance);
			if (distance < cartDistance) {
				// Debug.Log ("Cart caught");
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
