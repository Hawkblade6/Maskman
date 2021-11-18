using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {


	private bool entered = false;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool GetSide () {
		if (entered){
			return true;
		}else {
			return false;
		}
	}

	void OnTriggerEnter2D (Collider2D col){
		if(col.tag == "Player"){
			entered = true;
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if(col.tag == "Player"){
			entered = false;
		}
	}
}
