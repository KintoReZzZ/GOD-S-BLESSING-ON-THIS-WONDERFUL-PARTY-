using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_detect : MonoBehaviour {

	public GameObject player;
	public float minDist = Mathf.Infinity;
	Animator anim;
	Vector3 lastPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
  float move = (transform.position - lastPosition).magnitude;
    Vector3 currentPos = transform.position;
		float dist = Vector3.Distance(player.transform.position, currentPos);
		if (dist < minDist)
			 {
				transform.LookAt(player.transform);
				transform.position += transform.forward*2.0f*Time.deltaTime;
			 }
		anim.SetFloat("speed", 1);
	}
}
