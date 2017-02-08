using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerScript : MonoBehaviour {

	//declare GameObjects and create isShooting boolean.
	private GameObject gun;
	private GameObject spawnPoint;
	private bool isShooting;

	//Use this for Scoring
	public Text countText;
	public int count = 0;
	private float gameTimer = 60;

	// Use this for initialization
	void Start () {

		//only needed for IOS
		Application.targetFrameRate = 60;

		//create references to gun and bullet spawnPoint objects
		gun = gameObject.transform.GetChild (0).gameObject;
		spawnPoint = gun.transform.GetChild (0).gameObject;

		//set isShooting bool to default of false
		isShooting = false;
		setCountText ();

	}

	//Shoot function is IEnumerator so we can delay for seconds
	IEnumerator Shoot() {
		//set is shooting to true so we can't shoot continuosly
		isShooting = true;
		//instantiate the bullet
		GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
		//Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.rotation = spawnPoint.transform.rotation;
		bullet.transform.position = spawnPoint.transform.position;
		//add force to the bullet in the direction of the spawnPoint's forward vector
		rb.AddForce(spawnPoint.transform.forward * 500f);
		//play the gun shot sound and gun animation
		GetComponent<AudioSource>().Play ();
		gun.GetComponent<Animation>().Play ();
		//destroy the bullet after 1 second
		Destroy (bullet, 1);
		//wait for 0.2 second and set isShooting to false so we can shoot again
		yield return new WaitForSeconds (0.2f);
		isShooting = false;
	}

	// Update is called once per frame
	void Update () {
	
		gameTimer -= Time.deltaTime;

		//declare a new RayCastHit
		RaycastHit hit;
		//draw the ray for debuging purposes (will only show up in scene view)
		Debug.DrawRay (spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);
	
		//cast a ray from the spawnpoint in the direction of its forward vector
		if (Physics.Raycast (spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 250)) {
	
			//if the raycast hits any game object where its name contains "target" and we aren't already shooting we will start the shooting coroutine
			if (hit.collider.name.Contains ("target")) {
				if (!isShooting && gameTimer >= 0) {
					StartCoroutine ("Shoot");
					count = count + 1;
					setCountText ();
				}
				//also deactivate the object
				hit.collider.gameObject.SetActive (false);
				//instantiate a new target
				GameObject target = Instantiate (Resources.Load ("target", typeof(GameObject))) as GameObject;
	
				//set the coordinates for a new vector 3
				float randomX = UnityEngine.Random.Range (-10f, 10f);
				float constantY = 1f;
				float randomZ = UnityEngine.Random.Range (-10f, 10f);
				//set the targets position equal to these new coordinates
				target.transform.position = new Vector3 (randomX, constantY, randomZ);
	
				//if the target gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
				//so keep repositioning the target until it is greater than 3 scene units away. 
				while (Vector3.Distance (target.transform.position, Camera.main.transform.position) <= 3) {
	
					randomX = UnityEngine.Random.Range (-10f, 10f);
					randomZ = UnityEngine.Random.Range (-10f, 10f);
	
					target.transform.position = new Vector3 (randomX, constantY, randomZ);
				}
			}
			if (hit.collider.name.Contains ("bomb")) {
				if (!isShooting && gameTimer >= 0) {
					StartCoroutine ("Shoot");
					count = count - 2;
					setCountText ();
				}
			}
		}
	}
	
	void setCountText(){
		countText.text = "SCORE: " + count.ToString ();
	}
	
}