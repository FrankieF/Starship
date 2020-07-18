using UnityEngine;
using System.Collections;

public class BossWeaponControll : MonoBehaviour {

	public GameObject shot;
	public GameObject death;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
	public float lengthShot;
	public float health = 10;


	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		for(int i = 0; i < lengthShot; i++)
		{
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}

	}

	public void Died()
	{
		Vector3 v = new Vector3(Random.Range(0,1), 0f, Random.Range(0,1));
		Instantiate (death, transform.position + v, transform.rotation);
		v = new Vector3(Random.Range(0,1), 0f, Random.Range(0,1));
		Instantiate (death, transform.position + v, transform.rotation);
		v = new Vector3(Random.Range(0,1), 0f, Random.Range(0,1));
		Instantiate (death, transform.position + v, transform.rotation);
		v = new Vector3(Random.Range(0,1), 0f, Random.Range(0,1));
		Instantiate (death, transform.position + v, transform.rotation);
		v = new Vector3(Random.Range(0,1), 0f, Random.Range(0,1));
		Instantiate (death, transform.position + v, transform.rotation);

	}

}
