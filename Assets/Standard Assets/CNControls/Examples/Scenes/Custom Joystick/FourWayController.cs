using CnControls;
using UnityEngine;

namespace CustomJoystick
{
  [RequireComponent(typeof (Animator))]
  [RequireComponent(typeof (CapsuleCollider))]
  [RequireComponent(typeof (Rigidbody))]

    public class FourWayController : MonoBehaviour
    {
      public float animSpeed = 1.5f;				// animation speed
    	public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
    	public bool useCurves = true;				// Curve Mecanim
    	public float useCurvesHeight = 0.5f;		// variable to use the curve


    	public float forwardSpeed = 7.0f;
    	public float backwardSpeed = 2.0f;
    	public float rotateSpeed = 2.0f;
    	public float jumpPower = 3.0f;
    	private CapsuleCollider col;
    	private Rigidbody rb;
    	private Vector3 velocity;
    	private float orgColHight;
    	private Vector3 orgVectColCenter;

    	private Animator anim;							// キャラにアタッチされるアニメーターへの参照
    	private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照

    	private GameObject cameraObject;	// メインカメラへの参照

      // Basic Animation
    	static int idleState = Animator.StringToHash("Base Layer.Idle");
    	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    	static int jumpState = Animator.StringToHash("Base Layer.Jump");
    	static int restState = Animator.StringToHash("Base Layer.Rest");

      private Vector3[] directionalVectors = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

      private Transform _mainCameraTransform;

      void Start ()
    	{
    		// Setup the animation, that is requered
    		anim = GetComponent<Animator>();
    		// CapsuleCollider
    		col = GetComponent<CapsuleCollider>();
    		rb = GetComponent<Rigidbody>();
    		//control camera
    		cameraObject = GameObject.FindWithTag("MainCamera");
    		// CapsuleColliderコ
    		orgColHight = col.height;
    		orgVectColCenter = col.center;
    }

        private void Awake()
        {
            _mainCameraTransform = Camera.main.transform;
        }

        void animationUpdate (float v, float h, bool jump)
      	{
      		anim.SetFloat("Speed", v);							// Animator set the speed based in the vertical input
      		anim.SetFloat("Direction", h); 						// Animator set the direction based in the horizontal movement
      		anim.speed = animSpeed;								// Animator speed of the animation
      		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	// get current animation stage
      		rb.useGravity = true;//Set animation gravity

      		velocity = new Vector3(0, 0, v);		//Vertor to move the character

      		velocity = transform.TransformDirection(velocity);
      		// determinate if is going forward or backward
      		if (v > 0.1) {
      			velocity *= forwardSpeed;
      		} else if (v < -0.1) {
      			velocity *= backwardSpeed;
      		}

      		if (jump) {
      			if (currentBaseState.nameHash == locoState){
      				if(!anim.IsInTransition(0))
      				{
      						rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
      						anim.SetBool("Jump", true);		// Animator
      				}
      			}
      		}
      		transform.localPosition += velocity * Time.fixedDeltaTime;

      		transform.Rotate(0, h * rotateSpeed, 0);
      		if (currentBaseState.nameHash == locoState){
      			if(useCurves){
      				resetCollider();
      			}
      		}
      		else if(currentBaseState.nameHash == jumpState)
      		{
      			cameraObject.SendMessage("setCameraPositionJumpView");	//
      			if(!anim.IsInTransition(0))
      			{

      				if(useCurves){
      					float jumpHeight = anim.GetFloat("JumpHeight");
      					float gravityControl = anim.GetFloat("GravityControl");
      					if(gravityControl > 0)
      						rb.useGravity = false;

      					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
      					RaycastHit hitInfo = new RaycastHit();
      					if (Physics.Raycast(ray, out hitInfo))
      					{
      						if (hitInfo.distance > useCurvesHeight)
      						{
      							col.height = orgColHight - jumpHeight;
      							float adjCenterY = orgVectColCenter.y + jumpHeight;
      							col.center = new Vector3(0, adjCenterY, 0);
      						}
      						else{
      							resetCollider();
      						}
      					}
      				}
      				anim.SetBool("Jump", false);
      			}
      		}
      		else if (currentBaseState.nameHash == idleState)
      		{
      			if(useCurves){
      				resetCollider();
      			}
      			if (jump) {
      				anim.SetBool("Rest", true);
      			}
      		}
      		else if (currentBaseState.nameHash == restState)
      		{
      			if(!anim.IsInTransition(0))
      			{
      				anim.SetBool("Rest", false);
      			}
      		}
      	}

        private void Update()
        {
            var movementVector = new Vector3(CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
            animationUpdate (CnInputManager.GetAxis("Vertical"), CnInputManager.GetAxis("Horizontal"), false);
            if (movementVector.sqrMagnitude < 0.00001f) return;
        }

        void resetCollider()
        {
          col.height = orgColHight;
          col.center = orgVectColCenter;
        }
    }
}
