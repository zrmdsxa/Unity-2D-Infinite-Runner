using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

	public enum m_states {MENU,PLAY,GAMEOVER,SCORE};
	public GameObject[] m_GameStates;
	private GameObject m_currentGameState;

	public m_states m_currentState;
	public m_states GetCurrentState{get{return m_currentState;}}

	private static StateManager m_instance = null;
	public static StateManager Instance{get{return m_instance;}}

	void Awake(){
		if (m_instance != null && m_instance != this){
			Destroy(gameObject);
			return;
		}
		else{
			m_instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {

		int numStates = m_GameStates.Length;
		foreach (GameObject go in m_GameStates){
			go.SetActive(false);
		}

		m_currentState = m_states.MENU;
		m_currentGameState = m_GameStates[(int)m_states.MENU];
		m_currentGameState.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayGame(){

		m_currentGameState.SetActive(false);
		m_currentGameState = m_GameStates[(int)m_states.PLAY];
		m_currentGameState.SetActive(true);
		m_currentState = m_states.PLAY;
	}

	public void GameOver(){
		m_currentGameState.SetActive(false);
		m_currentGameState = m_GameStates[(int)m_states.GAMEOVER];
		m_currentGameState.SetActive(true);
		m_currentState = m_states.GAMEOVER;
	}

	public void Restart(){
		Start();
		GameManager.Instance.RestartLevel();
	}
}
