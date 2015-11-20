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
    private float missileFireRate = 3f;

    public SpawnSpaceStuff spaceObjects;

    public float speed = 8.0f;

   void Start() {
       GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();

       playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
       playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void Update() {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) {
            playerOnePosition = new Vector3(Input.GetAxis("P1_Horizontal"), 0, Input.GetAxis("P1_Vertical")).normalized;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }
            
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) {
            playerTwoPosition = new Vector3(Input.GetAxis("P2_Horizontal"), 0, Input.GetAxis("P2_Vertical")).normalized;
            playerTwoPosition = transform.TransformDirection(playerTwoPosition);
            playerTwoPosition *= speed;
        }

        playerOne.Move(playerOnePosition * Time.deltaTime);
        playerTwo.Move(playerTwoPosition * Time.deltaTime);

        fireLazors();
        fireMissiles();

        //Add to final score??
        GameManager.timeSurvivedP1 = Time.time;
        GameManager.timeSurvivedP2 = Time.time;
    }

    private void fireLazors()
    {
        if (Input.GetMouseButton(0) && Time.time > lazorFireTime)
        {

            for (int i = 0; i < muzzle.Length; i++)
            {
                Instantiate(lazor, muzzle[i].transform.position, muzzle[i].transform.rotation);
            }

            lazorFireTime = Time.time + lazorFireRate;
        }
    }

    private void fireMissiles()
    {
        if (Input.GetMouseButton(1) && Time.time > missileFireTime)
        {
            spaceObjects.Spawn();
            for (int i = 0; i < muzzle.Length; i++)
            {
                Instantiate(missile, muzzle[i].transform.position, muzzle[i].transform.rotation);
            }

            missileFireTime = Time.time + missileFireRate;
        }
    }
}