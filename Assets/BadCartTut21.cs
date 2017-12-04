using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BadCartTut21 : FightCart {
	private Vector3 targetPosition;
	private bool isMousePositionSet = false;

	

	// Use this for initialization
	void Start () {
		initialize_cart();
	}

	
 	public override void createWishList() {

	var wish = (GameObject) Instantiate(Resources.Load("prefab/" + itemList2[0]), transform.position + offsetVector[0], transform.rotation) ;	
	wish.tag = "wish";
	wish.transform.parent = transform;
	wishList[0] = wish;
	Debug.Log("adding wish 1");

	wish = (GameObject) Instantiate(Resources.Load("prefab/" + itemList2[4]), transform.position + offsetVector[1], transform.rotation) ;	
	wish.tag = "wish";
	wish.transform.parent = transform;
	wishList[1] = wish;
	Debug.Log("adding wish 2");

	wish = (GameObject) Instantiate(Resources.Load("prefab/" + itemList2[2]), transform.position + offsetVector[2], transform.rotation) ;	
	wish.tag = "wish";
	wish.transform.parent = transform;
	wishList[2] = wish;	
	Debug.Log("adding wish 3");
}
   	public override void createInventory() {
		addInventory(itemList2[1]);
		addInventory(itemList2[3]);
		
	}
	// Update is called once per frame
	void Update () {
		
	}


	// void FixedUpdate () {

	// 	if (!inFight) {
	// 		if (Input.GetMouseButtonDown (0)) {
	// 			//targetPosition = Input.mousePosition;
	// 			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	// 			targetPosition.z = transform.position.z; // so it stays on the same z axis
	// 			isMousePositionSet = true;
	// 			GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
	// 		}
	// 		if (isMousePositionSet) {
	// 			shopper.GetComponent<Animator>().SetBool("isAggro", false);
	// 			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);			
	// //			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
	// //			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
	// 			//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	// 			float distance = Vector3.Distance (transform.position, targetPosition);
	// 			if (distance < 0.1f) {
	// 				isMousePositionSet = false;
	// 			}
	// 		}
	// 		// if (hasItem(wantedItem[wantedItemIndex])) {
	// 		// 	clearLastItem();
	// 		// 	nextWantedItem();
	// 		// }

	// 	}



	// }
}
