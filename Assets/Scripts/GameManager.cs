using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	

	public float m_speedIncreaseRate;

	public Text m_scoreUIText;
	private float m_score;
	private float m_gameSpeed;

	public GameObject[] m_prefabSections;

	public float[] m_prefabSectionsLength;

	public float GameSpeed{get {return m_gameSpeed;}}

	private static GameManager m_instance = null;
	public static GameManager Instance{get{return m_instance;}}

	private int m_currentPrefab = 2;
	private float m_currentPrefabPos = 0;

	private int m_numPrefabs; // 0 sm, 1 med, 2 long


	//obstacles

	public float nextObstacleMin = 1.0f;
	public float nextObstacleMax = 5.0f;
	public float nextObstacleTimer;

	public GameObject[] m_prefabObstacles;

	// Use this for initialization
	void Awake(){
		if (m_instance != null && m_instance != this){
			Destroy(gameObject);
			return;
		}
		else{
			m_instance = this;
		}
		DontDestroyOnLoad(gameObject);

		m_numPrefabs = m_prefabSections.Length;
	}

	void Start () {
		m_score = 0f;
		m_speedIncreaseRate = 0.1f;
		m_gameSpeed = 1f;

		AddSection();

		nextObstacleTimer = Random.Range(nextObstacleMin,nextObstacleMax);
	}
	
	// Update is called once per frame
	void Update () {
		m_gameSpeed += m_speedIncreaseRate * Time.deltaTime;
		m_score += Time.deltaTime;

		nextObstacleTimer -= Time.deltaTime;
		if (nextObstacleTimer <= 0.0f){
			CreateObstacle();
		}

		if (m_gameSpeed > 4.0f){
			m_gameSpeed = 4.0f;
		}

		//TODO: remove debugging information
		m_scoreUIText.text = "Score: " + Mathf.Floor(m_score);
	}

	public void AddSection(){
		int randomSection = (int)Random.Range(0,m_numPrefabs);

		CreateSection(randomSection);
	}

	private void CreateSection(int s){
		Vector3 pos = Vector3.zero;
		pos.z = -1;
		pos.x = m_prefabSectionsLength[m_currentPrefab] + m_currentPrefabPos;
		GameObject tmp = Instantiate(m_prefabSections[s], pos, Quaternion.identity) as GameObject;
		tmp.name = "Section - " +s;
		m_currentPrefab = s;
		m_currentPrefabPos = pos.x;
	}

	public void RestartLevel(){
		m_gameSpeed = 0.0f;
		m_score = 0;
	}

	public void RestartSpeed(){
		m_gameSpeed = 1f;
	}

	public int GetScore(){
		return (int)Mathf.Floor(m_score);
	}

	public void AddBonus(int pointsToAdd){
		m_score += pointsToAdd;
	}
	void CreateObstacle(){
//		Debug.Log("create obstacle");
		nextObstacleTimer = Random.Range(nextObstacleMin,nextObstacleMax);
		Vector3 pos = Vector3.zero;
		
		pos.x = m_currentPrefabPos;
		pos.y = 0.25f;
		pos.z = -1.0f;
		GameObject tmp = Instantiate(m_prefabObstacles[0],pos,Quaternion.identity) as GameObject;
		
	}

	public void PlayerDied(){
		StateManager.Instance.GameOver();
		
	}
}
