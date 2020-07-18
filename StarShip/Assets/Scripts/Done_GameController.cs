using UnityEngine;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	private static Done_GameController controller = null;
	public static Done_GameController GetInstance {
		get {
			return controller == null ? controller = GameObject.Find ("Manager").GetComponent<Done_GameController> () : controller;
		}
	}

	public GameObject[] hazards, items;
	public Vector3 spawnValues;
	public int hazardCount, bossScore, healthincrease;
	public float spawnWait, itemWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	
	public bool gameOver, died;
	private bool restart;
	private bool addHighScore = true;
	public int score;
	public BestTimes bt;
	public GameObject boss;
	public Transform bossSpawn;
	public int difficulty;
	
	void Start ()
	{
		if (GameObject.Find ("Data") != null) {
			SaveData data = GameObject.Find ("Data").GetComponent<SaveData> ();
			data.enabled = false;
			SetValues (data);
		}
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnItems ());
	}
	
	void Update ()
	{
		if (restart)
			Application.LoadLevel (Application.loadedLevel);
	}

	private void SetValues (SaveData data) {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		waveWait = data.time;
		difficulty = data.difficulty;
		Done_PlayerController.GetInstance.health = data.health;
		Done_PlayerController.GetInstance.healthBar.maxValue = data.health;
		Done_PlayerController.GetInstance.ammoTime = data.bulletTime;
		if (waveWait < 7) {
			Done_PlayerController.GetInstance.ammoCheck = true;
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			if(score >= bossScore) {
				StartCoroutine(BossBattle());
				break;
			}
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	IEnumerator SpawnItems ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			int i = Random.Range(0,10);
			GameObject item = i <= 6 ? items[0] : i <= 8 ? items[1] : items[2];
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
			Quaternion spawnRotation = new Quaternion(90f, 0f, 0f, 0f);
			Instantiate (item, spawnPosition, new Quaternion(0f, 90f, 90f, 0f));
			
			yield return new WaitForSeconds (itemWait);
		}
	}

	IEnumerator BossBattle() 
	{
		GameObject o = (GameObject)Instantiate(boss, bossSpawn.position, Quaternion.identity);
		o.GetComponent<BossWeaponControll>().health += healthincrease;

		while(true)
		{
			if (died) {
				died = false;
				healthincrease += 2;
				bossScore += score + (int)(score * .5) + (int)(bossScore * 2);
				StartCoroutine(SpawnWaves());
				break;
			}
			yield return null;
		}
	}

	public void OnGUI ()
	{
		if (gameOver) {
			//scoreText.enabled = false;
//			if (addHighScore) {
//				addHighScore = false;
//				bt.NewTime (score);
//			}
			//bt.ShowBestTimes();	
			ShowRestart();
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		if (!gameOver) {
			score += newScoreValue;
			UpdateScore ();
		}
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void ShowRestart()
	{
		restartText.text = "Press R to play again";
		if(Input.GetKeyDown(KeyCode.R))
		{
			gameOver = false;
			restart = true;
		}
	}

	public void GetScore ()
	{
		
	}

}