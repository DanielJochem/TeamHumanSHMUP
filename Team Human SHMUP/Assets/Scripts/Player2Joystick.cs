using UnityEngine;
using System.Collections;

public class Player2Joystick : MonoBehaviour {

	private Transform myTransform;
	private float tiltX;
	private float tiltZ;
	private Vector3 playerPosition;
	public float moveSpeed = 100f;
	
	//Player starting positions
	public Vector3 playerTwoPosition = new Vector3(0, 0, 0);
	
	//The CharacterController on both players
	public CharacterController playerTwo;
	
	// Use this for initialization
	void Start () 
	{
		myTransform = this.transform;
		
		playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		JoystickMovement2 ();
	}
	
	private void JoystickMovement2()
	{
		playerPosition = myTransform.position;
		if (Input.GetAxis ("LeftJoystickVertical2") != 0) {
			float axis = Input.GetAxis ("LeftJoystickVertical2");
			playerPosition.z += axis * moveSpeed * Time.deltaTime;
			tiltZ = axis * 20f;
		} else {
			tiltZ = 0.0f;
		}
		if (Input.GetAxis ("LeftJoystickHorizontal2") != 0) {
			float axis = Input.GetAxis ("LeftJoystickHorizontal2");
			playerPosition.x += axis * this.moveSpeed * Time.deltaTime;
			tiltX = axis * 20f;
		}
		myTransform.position = playerPosition;
	}
}