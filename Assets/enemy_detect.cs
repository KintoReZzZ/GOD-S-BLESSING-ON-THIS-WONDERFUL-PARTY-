using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_detect : MonoBehaviour {

	public GameObject player;
	public AnimationClip run_clip;
	public AnimationClip attack_clip;
	public AnimationClip kill_clip;
	public AnimationClip idle_clip;
	public AnimationClip dead_clip;
	bool dead = false;

	Animation anim;
	public float minDist = Mathf.Infinity;
	Vector3 lastPosition = Vector3.zero;
	int kill_attack_counter=0;
	public int health = 1000;

	// Use this for initialization
	void Start () {
	 anim = GetComponent<Animation> ();
	  anim.AddClip(run_clip, "run");
		anim.AddClip(attack_clip, "attack");
		anim.AddClip(kill_clip, "kill");
		anim.AddClip(idle_clip, "idle");
		anim.AddClip(dead_clip, "dead");
	}

	// Update is called once per frame
	void Update () {
		if(health <= 0){
			 anim.Play("dead");
			if(player.GetComponent<FourWayController>().food < 10 && !dead){
			 	player.GetComponent<FourWayController>().food+=10;
				dead = true;
			}
			return;
		}
  float move = (transform.position - lastPosition).magnitude;
    Vector3 currentPos = transform.position;
		float dist = Vector3.Distance(player.transform.position, currentPos);
		if (dist < minDist)
			 {
				transform.LookAt(player.transform);
			  if(dist > 3)
				{
					transform.position += transform.forward*2.0f*Time.deltaTime;
					anim.Play ("run");
			 	}else{
					kill_attack_counter++;
					if (kill_attack_counter > 150){
						anim.Play ("kill");
						if (kill_attack_counter==200)
								kill_attack_counter =0; // Reset Kill attack
								if (player.GetComponent<FourWayController>().food > 0)
								player.GetComponent<FourWayController>().food-=5;
					}
					else{
						anim.Play ("attack");
						if(player.GetComponent<FourWayController>().food > 0)
						player.GetComponent<FourWayController>().food--;
					}
				}
			}
			else{
					anim.Play ("idle");
			}
	}
}
