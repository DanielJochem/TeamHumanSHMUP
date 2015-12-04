using UnityEngine;
using System.Collections;

public class PlayerManagement : MonoBehaviour {
    
    //The Character Controllers on the Players.
	public CharacterController playerOne;
	public CharacterController playerTwo;

    //Health and lives
    public int health = 100;
    public int lives = 3;

    //Player starting positions
    public Vector3 playerOnePosition = new Vector3(0, 40, 0);
    public Vector3 playerTwoPosition = new Vector3(0, 0, 0);

    public Quaternion p1Rotate;
	public Quaternion p2Rotate;
    
    //Weapons fire from here
    public GameObject p1Muzzle;
	public GameObject p2Muzzle;

	//Weapon Selected
	public int p1WeaponSelected = 1;
	public int p2WeaponSelected = 1;

    //Lazor Weapon
    public GameObject machineGun;
    private float p1MachineGunFireTime;
	private float p2MachineGunFireTime;
    private float machineGunFireRate = 0.1f;

    //Missile Weapon
    public GameObject rocketLauncher;
    private float p1RocketLauncherFireTime;
	private float p2RocketLauncherFireTime;
    private float rocketLauncherFireRate = 5.0f;

    //Shotgun Weapon
    public GameObject shotgun;
    private float p1ShotgunFireTime;
	private float p2ShotgunFireTime;
    private float shotgunFireRate = 2.0f;

    public float keySpeed = 8.0f;

    void Start() {
        p1Rotate = p1Muzzle.transform.rotation;
        p1Rotate.z += 180;
		p2Rotate = p2Muzzle.transform.rotation;
		p2Rotate.z += 180;

        playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
        playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void FixedUpdate() {
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
			playerOnePosition = new Vector3(Input.GetAxis("LeftJoystickHorizontal"), 0, Input.GetAxis("LeftJoystickVertical")).normalized;
			playerOnePosition = transform.TransformDirection(playerOnePosition);
			playerOnePosition *= keySpeed;
		}

		if (Input.GetAxis ("LeftJoystickVertical2") != 0 || Input.GetAxis ("LeftJoystickHorizontal2") != 0) {
			playerTwoPosition = new Vector3 (Input.GetAxis ("LeftJoystickHorizontal2"), 0, Input.GetAxis ("LeftJoystickVertical2")).normalized;
			playerTwoPosition = transform.TransformDirection (playerTwoPosition);
			playerTwoPosition *= keySpeed;
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
            gameManager.Instance.timeSurvivedP1 = Time.time;
        }

        if (playerTwo != null) {
            playerTwo.Move(playerTwoPosition * Time.deltaTime);
            gameManager.Instance.timeSurvivedP2 = Time.time;
        }

		if(this.gameObject.tag == "Player 1") {
			P1SwtichWeapon();
            P1FireWeapons();
		}

		if (this.gameObject.tag == "Player 2") {
			P2SwtichWeapon();
            P2FireWeapons();
        }

        //Kill Check
        if (health <= 0) {
            if (this.gameObject.tag == "Player 1") {
                gameManager.Instance.p1LivesRemaining--; //
                if (gameManager.Instance.p1LivesRemaining > 0) {
                    health = 100;
                    this.transform.position = new Vector3(4.63f, 1.585f, -10.7f);

                    gameManager.Instance.p1HealthRemaining = 100;
                } else {
                    gameManager.Instance.playerOneDead = true;
                    Destroy(this.gameObject);
                    //For later use
                    //Instantiate(deathExplosion, transform.position, transform.rotation);
                }
            }

            if (this.gameObject.tag == "Player 2") {
                gameManager.Instance.p2LivesRemaining--;
                if (gameManager.Instance.p2LivesRemaining > 0) {
                    health = 100;
                    this.transform.position = new Vector3(18.63f, 1.585f, -10.7f);
                    gameManager.Instance.p2HealthRemaining = 100;
                }  else {
                    gameManager.Instance.playerTwoDead = true;
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
            gameManager.Instance.p1HealthRemaining = health;
        } else {
            gameManager.Instance.p2HealthRemaining = health;
        }
    }
	

	void P1SwtichWeapon() {
		if (Input.GetButtonDown("SwapButton1")) {
			if (p1WeaponSelected == 3) {
				p1WeaponSelected = 1;
			} else {
				p1WeaponSelected++;
			}
		}
	}

	void P2SwtichWeapon() {
		if (Input.GetButtonDown("SwapButton2")) {
			if (p2WeaponSelected == 3) {
				p2WeaponSelected = 1;
			} else {
				p2WeaponSelected++;
			}
		}
	}

    void P1FireWeapons() {
        if (p1WeaponSelected == 1 || Input.GetMouseButton(0))
        {
            if ((Input.GetAxis("RightTriggerFire1") > 0 || Input.GetMouseButton(0)) && Time.time > p1MachineGunFireTime)
            {
                AudioManager.Instance.LazerFireAudioSound();
                Instantiate(machineGun, p1Muzzle.transform.position, p1Rotate);
                p1MachineGunFireTime = Time.time + machineGunFireRate;
            }
        }

        if (p1WeaponSelected == 2 || (Input.GetMouseButton(2) && (!Input.GetMouseButton(1) && !Input.GetMouseButton(0))))
        {
            if ((Input.GetAxis("RightTriggerFire1") > 0 || (Input.GetMouseButton(2) && (!Input.GetMouseButton(1) && !Input.GetMouseButton(0)))) && Time.time > p1ShotgunFireTime)
            {
                AudioManager.Instance.RocketFireAudioSound();
                Instantiate(shotgun, p1Muzzle.transform.position, p1Rotate);
                p1Rotate.z += 20.0f;
                p1Rotate.x += 0.1f;
                Instantiate(shotgun, p1Muzzle.transform.position, p1Rotate);
                p1Rotate.z -= 40.0f;
                p1Rotate.x -= 0.2f;
                Instantiate(shotgun, p1Muzzle.transform.position, p1Rotate);

                p1ShotgunFireTime = Time.time + shotgunFireRate;
            }
        }

        if (p1WeaponSelected == 3 || (Input.GetMouseButton(1) && !Input.GetMouseButton(0)))
        {
            if ((Input.GetAxis("RightTriggerFire1") > 0 || (Input.GetMouseButton(1) && !Input.GetMouseButton(0))) && Time.time > p1RocketLauncherFireTime)
            {
                AudioManager.Instance.RocketFireAudioSound();
                Instantiate(rocketLauncher, p1Muzzle.transform.position, p1Rotate);
                p1RocketLauncherFireTime = Time.time + rocketLauncherFireRate;
            }
        }
    }

    void P2FireWeapons()
    {
        if (p2WeaponSelected == 1 || Input.GetMouseButton(0))
        {
            if ((Input.GetAxis("RightTriggerFire2") > 0 || Input.GetMouseButton(0)) && Time.time > p2MachineGunFireTime)
            {
                AudioManager.Instance.LazerFireAudioSound();
                Instantiate(machineGun, p2Muzzle.transform.position, p2Rotate);
                p2MachineGunFireTime = Time.time + machineGunFireRate;
            }
        }

        if (p2WeaponSelected == 2 || (Input.GetMouseButton(2) && (!Input.GetMouseButton(1) && !Input.GetMouseButton(0))))
        {
            if ((Input.GetAxis("RightTriggerFire2") > 0 || (Input.GetMouseButton(2) && (!Input.GetMouseButton(1) && !Input.GetMouseButton(0)))) && Time.time > p2ShotgunFireTime)
            {
                AudioManager.Instance.RocketFireAudioSound();
                Instantiate(shotgun, p2Muzzle.transform.position, p2Rotate);
                p2Rotate.z += 20.0f;
                p2Rotate.x += 0.1f;
                Instantiate(shotgun, p2Muzzle.transform.position, p2Rotate);
                p2Rotate.z -= 40.0f;
                p2Rotate.x -= 0.2f;
                Instantiate(shotgun, p2Muzzle.transform.position, p2Rotate);

                p2ShotgunFireTime = Time.time + shotgunFireRate;
            }
        }

        if (p2WeaponSelected == 3 || (Input.GetMouseButton(1) && !Input.GetMouseButton(0)))
        {
            if ((Input.GetAxis("RightTriggerFire2") > 0 || (Input.GetMouseButton(1) && !Input.GetMouseButton(0))) && Time.time > p2RocketLauncherFireTime)
            {
                AudioManager.Instance.RocketFireAudioSound();
                Instantiate(rocketLauncher, p2Muzzle.transform.position, p2Rotate);
                p2RocketLauncherFireTime = Time.time + rocketLauncherFireRate;
            }
        }
    }
}