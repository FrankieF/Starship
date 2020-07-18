using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;
	public Vector3 direction;
	public bool item;

	void Start ()
	{
		if(item)
			direction = Vector3.forward;
		else
			direction = transform.forward;
		GetComponent<Rigidbody>().velocity = direction * speed;
	}
}
