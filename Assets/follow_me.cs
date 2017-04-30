using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_me : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {

	}

	void Update(){
		Vector3 currentPos = transform.position;
		float dist = Vector3.Distance(player.transform.position, currentPos);
		if (dist > 1.5)
		{
			transform.position = Vector3.Lerp(transform.position , player.transform.position, Time.deltaTime * 2.0f);
			transform.rotation = player.transform.rotation;
	  }
	}
}
