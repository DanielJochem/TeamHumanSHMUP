using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {

    //Player starting positions
    public Vector3 playerOnePosition = new Vector3(0, 40, 0);
    public Vector3 playerTwoPosition = new Vector3(0, 0, 0);

    //The CharacterController on both players
    public CharacterController playerOne;
    public CharacterController playerTwo;

    public gameManager GameManager;
    public Quaternion rotate;
    
    //Health and lives
    public int health = 100;
    public int lives = 3;

    //Weapons fire from here
    public GameObject muzzle;

    //Lazor Weapon
    public GameObject machineGun;
    private float machineGunFireTime;
    private float machineGunFireRate = 0.1f;

    //Missile Weapon
    public GameObject rocketLauncher;
    private float rocketLauncherFireTime;
    private float rocketLauncherFireRate = 5.0f;

    //Shotgun Weapon
    public GameObject shotgun;
    private float shotgunFireTime;
    private float shotgunFireRate = 2.0f;

    public float keySpeed = 8.0f;
    public float joySpeed = 100.0f;

    void Start() {
       GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
       rotate = muzzle.transform.rotation;

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

        //if players are alive
        if (playerOne != null)
        {
            playerOne.Move(playerOnePosition * Time.deltaTime);
            GameManager.timeSurvivedP1 = Time.time;
        }

        if (playerTwo != null) {
            playerTwo.Move(playerTwoPosition * Time.deltaTime);
            GameManager.timeSurvivedP2 = Time.time;
        }

        fireMachineGun();
        fireRocketLauncher();
        fireShotgun();

        //Kill Check
        if (health <= 0) {
            if (this.gameObject.tag == "Player 1") {
                GameManager.p1LivesRemaining--;
                if (GameManager.p1LivesRemaining > 0) {
                    health = 100;
                    this.transform.position = new Vector3(4.63f, 1.585f, -10.7f);

                    GameManager.p1HealthRemaining = 100;
                } else {
                    GameManager.playerOneDead = true;
                    Destroy(this.gameObject);
                    //For later use
                    //Instantiate(deathExplosion, transform.position, transform.rotation);
                }
            }

            if (this.gameObject.tag == "Player 2") {
                GameManager.p2LivesRemaining--;
                if (GameManager.p2LivesRemaining > 0) {
                    health = 100;
                    this.transform.position = new Vector3(18.63f, 1.585f, -10.7f);
                    GameManager.p2HealthRemaining = 100;
                }  else {
                    GameManager.playerTwoDead = true;
                    Destroy(this.gameObject);
                    //For later use
                    //Instantiate(deathExplosion, transform.position, transform.rotation);
                }
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(this.gameObject.tag == "Player 1")
        {
            GameManager.p1HealthRemaining = health;
        } else {
            GameManager.p2HealthRemaining = health;
        }
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

    void fireMachineGun() {
        if (Input.GetMouseButton(0) && Time.time > machineGunFireTime) {
            AudioManager.Instance.LazerFireAudioSound();
            Instantiate(machineGun, muzzle.transform.position, muzzle.transform.rotation);
            machineGunFireTime = Time.time + machineGunFireRate;
        }
    }

    void fireRocketLauncher() {
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0) && Time.time > rocketLauncherFireTime) {
            AudioManager.Instance.RocketFireAudioSound();
            Instantiate(rocketLauncher, muzzle.transform.position, muzzle.transform.rotation);
            rocketLauncherFireTime = Time.time + rocketLauncherFireRate;
        }
    }

    void fireShotgun() {
        if (Input.GetMouseButton(2) && (!Input.GetMouseButton(1) && !Input.GetMouseButton(0)) && Time.time > shotgunFireTime) {
            AudioManager.Instance.RocketFireAudioSound();
            Instantiate(shotgun, muzzle.transform.position, rotate);
            rotate.z += 20.0f;
            rotate.x += 0.1f;
            Instantiate(shotgun, muzzle.transform.position, rotate);
            rotate.z -= 40.0f;
            rotate.x -= 0.2f;
            Instantiate(shotgun, muzzle.transform.position, rotate);

            shotgunFireTime = Time.time + shotgunFireRate;
        }
    }
}