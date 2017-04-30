using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_tuna : MonoBehaviour {
	public GameObject tuna;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float value = Random.value;
		if (  value > 100.0f && value < 300.0f){
			Instantiate(tuna, transform.position, transform.rotation);
		}
	}
}
