﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Cart : FightCart {
	private Vector3 targetPosition;
	private bool isMousePositionSet = false;

	

	// Use this for initialization
	void Start () {
		cartName = "me"; 
		hatColor = Color.black;
		origSize = shopper.transform.localScale;
		hat.GetComponent<SpriteRenderer>().color = hatColor;
		speed = .06f;
		iAmEnemy = false;
		createWishList();
		createInventory();
		// nextWantedItem() ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void playPickup() {
		playSound("pickup");
		
	}

	public override void playDrop() {
		playSound("drop");
	}

	public override void playPunch() {
		playSound("hit");
	}
	public override void playMove() {
		playSound("move");
	}
	public void playSound(string sound) {
		GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Music/" + sound), .5f);		
	}	

	void FixedUpdate () {

		if (!inFight) {
			if (Input.GetMouseButtonDown (0)) {
				//targetPosition = Input.mousePosition;
				targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				targetPosition.z = transform.position.z; // so it stays on the same z axis
				isMousePositionSet = true;
				GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			}
			if (isMousePositionSet) {
				shopper.GetComponent<Animator>().SetBool("isAggro", false);
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);			
	//			Rigidbody2D rb = GetComponent<Rigidbody2D> ();
	//			rb.velocity = Vector3.Normalize (targetPosition - transform.position) * speed;
				//transform.position = Vector3.MoveTowards (transform.position, target.position, step);
				float distance = Vector3.Distance (transform.position, targetPosition);
				if (distance < 0.1f) {
					isMousePositionSet = false;
				}
			}
			// if (hasItem(wantedItem[wantedItemIndex])) {
			// 	clearLastItem();
			// 	nextWantedItem();
			// }

		}



	}
}
