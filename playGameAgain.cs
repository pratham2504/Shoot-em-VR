using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playGameAgainScript : MonoBehaviour {

	public void PlayAgain() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game Scene");
	}
}
