using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoContro : MonoBehaviour {

	AudioSource bitSound;
	//public AudioClip bits;

	const int MinLane = -2;
	const int MaxLane = 2;
	const float laneWidth = 1.0f;

	const int DefaultLife = 3;
	const float StunDuration = 0.5f;

	CharacterController controller;
	Animator animator;
	Vector3 moveDir = Vector3.zero;
	int targetLane;

	int life = DefaultLife;
	float recovertime = 0.0f;

	public float speedX;
	public float accelerZ;

	public float gravity;
	public float speedZ;
	public float speedJump;


	public int Life(){

		return life;
	}
	public bool IsStan(){

		return recovertime > 0.0f || life <= 0;
	}

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> (); 
		bitSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

//		if(controller.isGrounded){
//
//			if (Input.GetAxis ("Vertical") > 0.0f) {
//				moveDir.z = Input.GetAxis ("Vertical") * speedZ;
//			} else {
//				moveDir.z = 0;
//			}
//
//			transform.Rotate (0, Input.GetAxis ("Horizontal") * 3, 0);
//
//			//jump
//			if( Input.GetButton("Jump")){
//
//				moveDir.y = speedJump;
//				animator.SetTrigger ("jump");
//			}
//
//		}

		if (Input.GetKeyDown ("left"))
			MoveToLeft ();
		if (Input.GetKeyDown ("right"))
			MoveToRight ();
		if (Input.GetKeyDown ("space"))
			Jump ();
		
		if( IsStan()){

			moveDir.x = 0.0f;
			moveDir.z = 0.0f;
			recovertime -= Time.deltaTime;

		}else{

			float acceZ = moveDir.z + (accelerZ * Time.deltaTime);
			moveDir.z = Mathf.Clamp (acceZ, 0, speedZ);

			float ratioX = (targetLane * laneWidth - transform.position.x) / laneWidth;
			moveDir.x = ratioX * speedX;
		}

		moveDir.y -= gravity * Time.deltaTime;

		Vector3 globeDir = transform.TransformDirection (moveDir);
		controller.Move (globeDir * Time.deltaTime);

		if(controller.isGrounded){
			moveDir.y = 0;
		}
		animator.SetBool ("run", moveDir.z > 0.0f);

	}

	public void MoveToLeft(){

		if( IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane)
			targetLane--;
	}
	public void MoveToRight(){

		if( IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane)
			targetLane++;
	}
	public void Jump(){

		if( controller.isGrounded){
			moveDir.y = speedJump;

			animator.SetTrigger ("jump");
		}
	}

	void OnControllerColliderHit ( ControllerColliderHit hit){

		if( IsStan() )return;

		if( hit.gameObject.tag == "Robo"){

			//Debug.Log(bitSound.name);
			if (!bitSound.isPlaying){
				//播放音乐
				bitSound.Play();
			}

			life--;
			recovertime = StunDuration;
			animator.SetTrigger ("damage");

			Destroy (hit.gameObject);
		}
	}

}
