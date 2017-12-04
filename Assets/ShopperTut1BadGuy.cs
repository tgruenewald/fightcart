using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperTut1BadGuy : Shopper {
	
	public override void setHealth() {
		Debug.Log("Health is 3");
		health = 3;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
