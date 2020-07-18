using UnityEngine;
using System.Collections;

public class ItemMover : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
	public int value;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		if (Done_GameController.GetInstance.difficulty == 1) {
			scoreValue *= 2;
		} else if (Done_GameController.GetInstance.difficulty == 2) {
			scoreValue *= 3;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			if (explosion != null) {
				Instantiate (explosion, transform.position, transform.rotation);
			}		
			Done_GameController.GetInstance.AddScore (scoreValue);
			Done_PlayerController.GetInstance.Item(value);
			Destroy (gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Boundary") {
			Destroy (gameObject);
		}

	}
}