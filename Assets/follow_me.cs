using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_me : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {

	}

	void Update(){
		transform.position = Vector3.Lerp(transform.position , player.transform.position, Time.deltaTime * 2.0f);
		transform.rotation = player.transform.rotation;
	}
}
