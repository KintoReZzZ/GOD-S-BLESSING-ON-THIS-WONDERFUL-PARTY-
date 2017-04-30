using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_me : MonoBehaviour {

	public GameObject player;
	public AnimationClip run_clip;
	public AnimationClip attack_clip;
	public AnimationClip kill_clip;
	public AnimationClip idle_clip;
	public AnimationClip dead_clip;
	Animation anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		 anim.AddClip(run_clip, "run");
		 anim.AddClip(attack_clip, "attack");
		 anim.AddClip(kill_clip, "kill");
		 anim.AddClip(idle_clip, "idle");
		 anim.AddClip(dead_clip, "dead");

	}

	void Update(){
		Vector3 currentPos = transform.position;
		float dist = Vector3.Distance(player.transform.position, currentPos);
		if (dist > 1.5)
		{
			transform.position = Vector3.Lerp(transform.position , player.transform.position, Time.deltaTime * 2.0f);
			transform.rotation = player.transform.rotation;
			anim.Play ("run");
	  }
		else{
			anim.Play ("idle");
		}
	}
}
