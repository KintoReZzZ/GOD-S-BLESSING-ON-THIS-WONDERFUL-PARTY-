using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_me : MonoBehaviour {

	public GameObject player;
	GameObject enemy;
	GameObject food;
	public AnimationClip run_clip;
	public AnimationClip attack_clip;
	public AnimationClip kill_clip;
	public AnimationClip idle_clip;
	public AnimationClip dead_clip;
	public AnimationClip fish_clip;
	Animation anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		 anim.AddClip(run_clip, "run");
		 anim.AddClip(attack_clip, "attack");
		 anim.AddClip(kill_clip, "kill");
		 anim.AddClip(idle_clip, "idle");
		 anim.AddClip(dead_clip, "dead");
		 anim.AddClip(fish_clip, "fish");
	}

	GameObject FindClosest(string search_by)
{
    GameObject[] gos;
    gos = GameObject.FindGameObjectsWithTag(search_by);
    GameObject closest = null;
    float distance = Mathf.Infinity;
    Vector3 position = transform.position;
    foreach (GameObject go in gos)
    {
        Vector3 diff = go.transform.position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < distance)
        {
            closest = go;
            distance = curDistance;
        }
    }
    return closest;
}

	void Update(){
		Vector3 currentPos = transform.position;
		float ally_dist = Vector3.Distance(player.transform.position, currentPos);
		float enemy_distance = 0.0f;
		float tuna_distance = 0.0f;
		float ally_enemy_distance = 0.0f;
		enemy = FindClosest("Enemy");
		ally_enemy_distance = Vector3.Distance(enemy.transform.position, player.transform.position);
    /* Search gameobject by tag enemy */
		 enemy_distance = Vector3.Distance(enemy.transform.position, currentPos);
		 food = FindClosest("Tuna");
		 tuna_distance = Vector3.Distance(food.transform.position, currentPos);
		 if (enemy_distance < 10 && ally_enemy_distance < 10)
		 {
			transform.position = Vector3.Lerp(transform.position , enemy.transform.position, Time.deltaTime * 1.0f);
			transform.LookAt(enemy.transform);
			if(enemy_distance < 2){
        anim.Play ("attack");
				enemy.GetComponent<enemy_detect>().health--;
			}else{
				anim.Play ("run");
			}
		 }else{
			 if(tuna_distance < 10 && enemy_distance > 10 && ally_dist <= 1.5 && player.GetComponent<FourWayController>().food < 10){
				 if (tuna_distance < 1)
				  {
						anim.Play ("fish");
						player.GetComponent<FourWayController>().food++;
				    Destroy(food);
					}else{
						anim.Play ("run");
						transform.LookAt(food.transform);
						transform.position = Vector3.Lerp(transform.position , food.transform.position, Time.deltaTime * 1.0f);
					}
			 }
		/*Attack enemy end*/
				else if (ally_dist > 1.5)
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
}
