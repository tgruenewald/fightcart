using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BadCartTut1 : FightCart {
	private Vector3 targetPosition;
	private bool isMousePositionSet = false;

	

	// Use this for initialization
	void Start () {
		initialize_cart();
	}

	
		public override GameObject create_shopper(Transform loc, Vector3 offsetVector) {
		return (GameObject) Instantiate(Resources.Load("prefab/shopper_fight_bad1"), loc.position + offsetVector, loc.transform.rotation) ;
	}
   	public override void createInventory() {
		addInventory(itemList2[2]);
		
	}
	public override void fight_cart_init() {
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
