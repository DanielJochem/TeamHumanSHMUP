using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    //Player starting positions
    public Vector3 playerOnePosition;
    public Vector3 playerTwoPosition;

    //The CharacterController on both players
    CharacterController playerOne;
    CharacterController playerTwo;

    public float speed = 10.0f;

    void Start() {
        playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
        playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void Update() { 
        if (this.gameObject.tag == "Player 1") {
            if (Input.GetKey("w")) {
                playerOnePosition.z = playerOnePosition.z + 1 * speed * Time.deltaTime;
            } else if (Input.GetKey("a")) {
                playerOnePosition.x = playerOnePosition.x - 1 * speed * Time.deltaTime;
            } else if (Input.GetKey("s")) {
                playerOnePosition.z = playerOnePosition.z - 1 * speed * Time.deltaTime;
            } else if (Input.GetKey("d")) {
                playerOnePosition.x = playerOnePosition.x + 1 * speed * Time.deltaTime;
            }

        } else if (this.gameObject.tag == "Player 2") {
            if (Input.GetKey(KeyCode.UpArrow)) {
                playerTwoPosition.z = playerTwoPosition.z + 1 * speed * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                playerTwoPosition.x = playerTwoPosition.x - 1 * speed * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.DownArrow)) {
                playerTwoPosition.z = playerTwoPosition.z - 1 * speed * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                playerTwoPosition.x = playerTwoPosition.x + 1 * speed * Time.deltaTime;
            }
        }

        playerOne.Move(playerOnePosition * Time.deltaTime);
        playerTwo.Move(playerTwoPosition * Time.deltaTime);
    }

    //If one of the players collide with another object (including the other player), stop. This solves
    //a bug with the CharacterController.Move wanting to finish it's 'lerp' or whatever it is before
    //changing the movement direction. It is slightly weird that the only way I discovered to solve this
    //was to set a MoveTowards Vector... Also if I get rid of the checks for the players in the Update() 
    //loop, this part also doesn't work.
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Wall") {
            if (hit.controller.tag == "Player 1") {
                playerOnePosition = Vector3.MoveTowards(playerOnePosition, Vector3.zero, 1.0f);
            } else if (hit.controller.tag == "Player 2") {
                playerTwoPosition = Vector3.MoveTowards(playerTwoPosition, Vector3.zero, 1.0f);
            }

        } else if (hit.gameObject.tag == "Enemy") {
            Destroy(hit.gameObject);
        }
    }
}