using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log(other.tag);
		if (other.tag == "Ground"){
			GameManager.Instance.AddSection();
		}
	}

}
