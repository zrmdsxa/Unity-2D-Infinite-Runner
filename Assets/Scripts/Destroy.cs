using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log(other.gameObject.name);
		if (other.gameObject.name == "LeftSide"){
			//Floors destroy gameobject
			//Obstacles destroy parent gameobject

			if (gameObject.tag == "Obstacle"){
				Destroy(transform.parent.gameObject);
			}
			else{
				Destroy(gameObject);
			}
			
		}
	}
}
