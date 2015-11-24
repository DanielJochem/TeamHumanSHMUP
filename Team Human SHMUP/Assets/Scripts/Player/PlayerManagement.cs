using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
    //Player starting positions
    public Vector3 playerOnePosition = new Vector3(0, 0, 0);
    public Vector3 playerTwoPosition = new Vector3(0, 0, 0);

    //The CharacterController on both players
    public CharacterController playerOne;
    public CharacterController playerTwo;

    public gameManager GameManager;

    //Weapons System Array
    public GameObject[] muzzle;

    //Lazor Weapon
    public GameObject lazor;
    private float lazorFireTime;
    private float lazorFireRate = 0.1f;

    //Missile Weapon
    public GameObject missile;
    private float missileFireTime;
    private float missileFireRate = 5.0f;

    public SpawnSpaceStuff spaceObjects;

    public float keySpeed = 8.0f;
    public float joySpeed = 100.0f;

    void Start() {
       GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

       playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
       playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void Update() {
        //Keyboard used
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) {
            playerOnePosition = new Vector3(Input.GetAxis("P1_Horizontal"), 0, Input.GetAxis("P1_Vertical")).normalized;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= keySpeed;
        }
            
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) {
            playerTwoPosition = new Vector3(Input.GetAxis("P2_Horizontal"), 0, Input.GetAxis("P2_Vertical")).normalized;
            playerTwoPosition = transform.TransformDirection(playerTwoPosition);
            playerTwoPosition *= keySpeed;
        }

        //Controllers used
        if (Input.GetAxis("LeftJoystickVertical") != 0 || Input.GetAxis("LeftJoystickHorizontal") != 0) {
            playerOnePosition = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickHorizontal")).normalized;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= joySpeed;
        }

        if (Input.GetAxis("LeftJoystickVertical2") != 0 || Input.GetAxis("LeftJoystickHorizontal2") != 0) {
            playerTwoPosition = new Vector3(Input.GetAxis("LeftJoystickHorizontal2"), 0, Input.GetAxis("LeftJoystickHorizontal2")).normalized;
            playerTwoPosition = transform.TransformDirection(playerTwoPosition);
            playerTwoPosition *= joySpeed;
        }

        /*if (Input.GetAxis("LeftJoystickVertical") != 0)
        {
            float axis = Input.GetAxis("LeftJoystickVertical");
            playerOnePosition.z += axis * joySpeed * Time.deltaTime;
            tiltZ = axis * 20f;
        }
        else
        {
            tiltZ = 0.0f;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") != 0)
        {
            float axis = Input.GetAxis("LeftJoystickHorizontal");
            playerOnePosition.x += axis * this.joySpeed * Time.deltaTime;
            tiltX = axis * 20f;
        }*/

        playerOne.Move(playerOnePosition * Time.deltaTime);
        playerTwo.Move(playerTwoPosition * Time.deltaTime);

        fireLazors();
        fireMissiles();

        //Add to final score??
        GameManager.timeSurvivedP1 = Time.time;
        GameManager.timeSurvivedP2 = Time.time;
    }

    /*void JoystickMovement()
    {
        playerPosition = transform.position;
        if (Input.GetAxis("LeftJoystickVertical") != 0)
        {
            float axis = Input.GetAxis("LeftJoystickVertical");
            playerPosition.z += axis * joySpeed * Time.deltaTime;
            tiltZ = axis * 20f;
        }
        else
        {
            tiltZ = 0.0f;
        }
        if (Input.GetAxis("LeftJoystickHorizontal") != 0)
        {
            float axis = Input.GetAxis("LeftJoystickHorizontal");
            playerPosition.x += axis * this.joySpeed * Time.deltaTime;
            tiltX = axis * 20f;
        }
        transform.position = playerPosition;
    }*/

    void fireLazors() {
        if (Input.GetMouseButton(0) && Time.time > lazorFireTime) {
            for (int i = 0; i < muzzle.Length; i++) {
                Instantiate(lazor, muzzle[i].transform.position, muzzle[i].transform.rotation);
            }

            lazorFireTime = Time.time + lazorFireRate;
        }
    }

    void fireMissiles() {
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0) && Time.time > missileFireTime) {
            spaceObjects.Spawn();
            for (int i = 0; i < muzzle.Length; i++) {
                Instantiate(missile, muzzle[i].transform.position, muzzle[i].transform.rotation);
            }

            missileFireTime = Time.time + missileFireRate;
        }
    }
}