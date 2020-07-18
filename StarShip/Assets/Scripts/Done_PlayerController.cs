using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	private static Done_PlayerController controller = null;
	public static Done_PlayerController GetInstance {
		get {
			return controller == null ? controller = GameObject.Find ("Ship").GetComponent<Done_PlayerController> () : controller;
		}
	}

	public int health = 3, ammo = 5, ammoMax = 5;
	public float speed, ammoTime = 1;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public GameObject explosion;
	public GameObject[] bullets;
	public Slider healthBar;
	public bool ammoCheck, canTakeDamage = true;
	 
	private float nextFire;
	
	void Update ()
	{
		Fire ();

	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody> ().velocity = movement * speed;
	
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
		);
	
		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);

	}

	public void TakeDamage(int damage)
	{
		if (canTakeDamage) {
			health -= damage;
			healthBar.value -= damage;
			if (health <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);
				Done_GameController.GetInstance.GameOver ();
				Destroy (gameObject);
			}
		}
	}

	public void Fire() {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			if (ammoCheck) {
				if (ammo > 0) {
					ammo--;
					nextFire = Time.time + fireRate;
					Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
					GetComponent<AudioSource> ().Play ();
					bullets [ammo].gameObject.SetActive(false);
					Invoke ("Ammo", ammoTime);
				}
			} else {
				nextFire = Time.time + fireRate;
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void Ammo() {
			ammo++;
			bullets [ammo-1].SetActive (true);

	}

	public void Item(int item) {
		if (item == 1) {
			StartCoroutine("Invincibility");

		} else if (item == 2) {
			StartCoroutine("UnlimitedFire");
		}

	}

	public IEnumerator Invincibility() {
		while (true) {
			canTakeDamage = false;
			health += 1;
			yield return new WaitForSeconds (5f);
			canTakeDamage = true;
			break;
		}

	}

	public IEnumerator UnlimitedFire() {
		while (true) {
			StartCoroutine("Shooting");
			yield return new WaitForSeconds (5f);
			StopCoroutine("Shooting");
			break;
		}

	}

	public IEnumerator Shooting() {
		while (true) {
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (.2f);
		}

	}

}
