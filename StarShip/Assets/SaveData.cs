using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour {

	public int health = 10, difficulty;
	public float time = 7f, bulletTime = 10f;

	void Start() {
		DontDestroyOnLoad (gameObject);
	}

	void OnGUI() {
		if (GUILayout.Button ("Easy Mode"))
			Load ();

		if (GUILayout.Button ("Medium Mode")) {
			health = 5;
			time = 5f;
			bulletTime = 2f;
			difficulty = 1;
			Load();

		}
		if (GUILayout.Button ("Hard Mode")) {
			health = 1;
			time = 3f;
			bulletTime = 3f;
			difficulty = 2;
			Load();

		}
	}

	public void Load() {
		Application.LoadLevel (1);
	}
}
