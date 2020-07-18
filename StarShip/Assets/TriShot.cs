using UnityEngine;
using System.Collections;

public class TriShot : MonoBehaviour {

	public GameObject shot;
	public Transform[] triShot;
	//public Transform shotSpawn;
	public float fireRate;
	public float delay;
	private Vector3 pos;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		for (int i = 0; i < triShot.Length; i++) {
			Instantiate(shot, triShot[i].position, triShot[i].rotation);
		}
		GetComponent<AudioSource>().Play();



		//Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		//GetComponent<AudioSource>().Play();
	}
}
