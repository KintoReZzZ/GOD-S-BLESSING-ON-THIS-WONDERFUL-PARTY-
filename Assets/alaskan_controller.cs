using UnityEngine;
using System.Collections;

public class alaskan_controller : MonoBehaviour
{
    Animator anim;
    int jumpHash = Animator.StringToHash("alaskan.rig|attack_1");
    int runStateHash = Animator.StringToHash("alaskan.rig|run");
		Vector3 lastPosition = Vector3.zero;


    void Start ()
    {
        anim = GetComponent<Animator>();
    }


    void Update ()
    {
        float move = (transform.position - lastPosition).magnitude;
				lastPosition = transform.position;
        anim.SetFloat("speed", 1);
    }
}
