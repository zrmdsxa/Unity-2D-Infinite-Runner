using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {


	float m_moveSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (StateManager.Instance.GetCurrentState == StateManager.m_states.PLAY){
			Vector3 pos = transform.position;
			pos.x += m_moveSpeed * GameManager.Instance.GameSpeed * Time.deltaTime;
			transform.position = pos;
			//Debug.Log(GameManager.Instance.GameSpeed);
		}
	}

}
