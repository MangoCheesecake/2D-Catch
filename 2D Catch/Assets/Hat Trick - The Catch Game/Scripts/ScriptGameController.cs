using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptGameController : MonoBehaviour 
{
	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;

	private float maxWidth;
	private bool playing;

	void Start () 
	{
		playing = false;
		if (cam == null) 
		{
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		UpdateText ();
	}

	void FixedUpdate()
	{
		if (playing) 
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) 
			{
				timeLeft = 0;
			}
			UpdateText ();
		}
	}

	public void StartGame()
	{
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());

	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds (2.0f);
		playing = true;
		while (timeLeft > 0) 
		{
			GameObject ball = balls [Random.Range (0, balls.Length)];
			Vector3 spawnPosition = new Vector3 (
				Random.Range(-maxWidth, maxWidth),
				transform.position.y,
				0.0f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (ball, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range(1.0f,2.0f));
		}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
	}

	void UpdateText()
	{
		timerText.text = "Time Left: " + Mathf.RoundToInt(timeLeft);
	}
}
