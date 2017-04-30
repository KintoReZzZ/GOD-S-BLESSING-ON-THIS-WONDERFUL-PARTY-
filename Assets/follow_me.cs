using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_me : MonoBehaviour {

	public GameObject player;
	GameObject enemy;
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

	GameObject FindClosestEnemy()
{
    GameObject[] gos;
    gos = GameObject.FindGameObjectsWithTag("Enemy");
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
		float ally_enemy_distance = 0.0f;
    /* Search gameobject by tag enemy */
     enemy = FindClosestEnemy();
		 enemy_distance = Vector3.Distance(enemy.transform.position, currentPos);
		 ally_enemy_distance = Vector3.Distance(enemy.transform.position, player.transform.position);
		 if (enemy_distance < 10 && ally_enemy_distance < 10)
		 {
			transform.position = Vector3.Lerp(transform.position , enemy.transform.position, Time.deltaTime * 1.0f);
			transform.LookAt(enemy.transform);
			if(enemy_distance < 2){
        anim.Play ("attack");
			}else{
				anim.Play ("run");
			}
		 }else{
		/*Attack enemy end*/
				if (ally_dist > 1.5)
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
