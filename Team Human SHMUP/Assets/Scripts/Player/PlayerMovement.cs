using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    //Player starting positions
    public Vector3 playerOnePosition = new Vector3(0, 0, 0);
    public Vector3 playerTwoPosition = new Vector3(0, 0, 0);

    //The CharacterController on both players
    public CharacterController playerOne;
    public CharacterController playerTwo;

    public float speed = 8.0f;

   void Start() {
        playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
        playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void Update() {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) {
            playerOnePosition = new Vector3(Input.GetAxis("P1_Horizontal"), 0, Input.GetAxis("P1_Vertical"));
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }
            
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) {
            playerTwoPosition = new Vector3(Input.GetAxis("P2_Horizontal"), 0, Input.GetAxis("P2_Vertical"));
            playerTwoPosition = transform.TransformDirection(playerTwoPosition);
            playerTwoPosition *= speed;
        }

        playerOne.Move(playerOnePosition * Time.deltaTime);
        playerTwo.Move(playerTwoPosition * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Enemy") {
            Destroy(hit.gameObject);
        }
    }
}