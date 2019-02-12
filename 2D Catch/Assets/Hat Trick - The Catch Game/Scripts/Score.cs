﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public Text scoreText;
	public int ballValue;

	private int score;

	void Start () 
	{
		score = 0;
		UpdateScore();
	}

	void OnTriggerEnter2D () 
	{
		score += ballValue;
		UpdateScore ();
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Bomb") 
		{
			score -= ballValue * 2;
			UpdateScore ();
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
