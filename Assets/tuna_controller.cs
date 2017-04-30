using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuna_controller : MonoBehaviour {
	GameObject[] tuna_path;
	int tuna_index;

	// Use this for initialization
	void Start () {
		tuna_path = GameObject.FindGameObjectsWithTag("Point");
		tuna_index=0;
	}

	// Update is called once per frame
	void Update () {

		transform.LookAt(tuna_path[tuna_index].transform);

		transform.position = Vector3.Lerp(transform.position , tuna_path[tuna_index].transform.position, Time.deltaTime*(Random.value)* 0.02f);
		Vector3 currentPos = transform.position;
		float dist = Vector3.Distance(tuna_path[tuna_index].transform.position, currentPos);
		if(dist < 0.5 ){
			tuna_index++;
		}
		if( tuna_index > tuna_path.Length -1){
			tuna_index=0;
		}
	}
}
