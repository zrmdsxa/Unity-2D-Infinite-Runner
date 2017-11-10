using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {


	public Text m_scoreUIText;
	private float m_score;

	// Use this for initialization
	void Start () {
		m_score = 0f;
		m_scoreUIText.text = "Score: " + m_score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
		m_score += Time.deltaTime;

		//TODO: remove debugging information
		m_scoreUIText.text = "Score: " + Mathf.Floor(m_score);
	}

	public void AddBonus(int pointsToAdd){
		m_score += pointsToAdd;
	}
}
