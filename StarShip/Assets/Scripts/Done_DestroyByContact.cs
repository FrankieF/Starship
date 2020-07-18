using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
	public int damage;
	public int health;
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
		if (other.tag == "Boundary" || other.tag == "Enemy") {
			return;
		} else if (other.tag == "Player") {
			Done_PlayerController.GetInstance.TakeDamage (damage);
		} else {
			
			if (explosion != null) {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			Destroy (other.gameObject);
		}
		if(other.tag == "Bullet")
			health--;
		if (health <= 0) {		
			Done_GameController.GetInstance.AddScore (scoreValue);
			if (gameObject.transform.parent != null) {				
				if (gameObject.transform.parent.parent != null)
					Destroy (gameObject.transform.parent.parent.gameObject);
				Destroy (gameObject.transform.parent.gameObject);
			}
			if (name.Contains ("oss")) {
				GetComponent<BossWeaponControll>().Died();
				Done_GameController.GetInstance.died = true;
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Boundary") {
			if (gameObject.transform.parent != null) {				
				if (gameObject.transform.parent.parent != null)
					Destroy (gameObject.transform.parent.parent.gameObject);
				Destroy (gameObject.transform.parent.gameObject);
			}
				Destroy (gameObject);
		}

	}
}