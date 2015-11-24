using UnityEngine;
using System.Collections;

public class Player1Joystick : MonoBehaviour {
	
	private Transform myTransform;
	private float tiltX;
	private float tiltZ;
	private Vector3 playerPosition;
	public float moveSpeed = 100f;
	
	//The CharacterController on both players
	public CharacterController playerOne;
	
	public gameManager GameManager;

	// Use this for initialization
	void Start () 
	{
		GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
		myTransform = this.transform;
		
		playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		JoystickMovement ();
	}

	void JoystickMovement()
	{
		playerPosition = myTransform.position;
		if (Input.GetAxis ("LeftJoystickVertical") != 0) {
			float axis = Input.GetAxis ("LeftJoystickVertical");
			playerPosition.z += axis * moveSpeed * Time.deltaTime;
			tiltZ = axis * 20f;
		} else {
			tiltZ = 0.0f;
		}
		if (Input.GetAxis ("LeftJoystickHorizontal") != 0) {
			float axis = Input.GetAxis ("LeftJoystickHorizontal");
			playerPosition.x += axis * this.moveSpeed * Time.deltaTime;
			tiltX = axis * 20f;
		}
		myTransform.position = playerPosition;
	}
}