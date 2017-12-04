using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void start_level() {
		SceneManager.LoadScene("tut1");
	}
	

		public void start_title() {
		SceneManager.LoadScene("title");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
