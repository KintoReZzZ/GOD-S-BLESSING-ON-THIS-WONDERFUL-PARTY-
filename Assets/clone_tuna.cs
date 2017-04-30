using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone_tuna : MonoBehaviour {
	public GameObject tuna;
	Vector3 pos;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float value = Random.value * 100.0f;
		if (  value > 50.0f && value < 52.0f){
			pos = transform.position;
      pos.x = pos.x + Random.value * 5;
			pos.z = pos.z + Random.value * 5;
			Instantiate(tuna, pos, transform.rotation);
		}
	}
}
