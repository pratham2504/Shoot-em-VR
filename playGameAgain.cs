using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playGameAgain : MonoBehaviour {
	
	//Use this for Timer
	public Text countDown;
	private float timer = 60;

	//Declare Button
	public Button playAgainButton;

	void Start() {
		//Set countDown value
		countDown = GetComponent<Text>();
	}

	void Update() {
		timer -= Time.deltaTime;
		countDown.text = timer.ToString ("f0");
		if (timer <= 0) {
			timer = 0;
			playAgainButton.gameObject.SetActive (true);
		} 
	}

	public void PlayAgain() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main Scene");
	}
}
