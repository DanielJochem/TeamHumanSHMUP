using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour {
    //Player 2 starting position
    public Vector3 playerTwoPosition = new Vector3(0, 0, 0);

    //The CharacterController
    CharacterController playerTwo;

    public float speed = 1f;

    void Start() {
        playerTwo = GameObject.FindGameObjectWithTag("Player 2").GetComponent<CharacterController>();
    }

    void Update() {
        if (this.gameObject.tag == "Player 2")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerTwoPosition.z = playerTwoPosition.z + 0.1f;
                playerTwoPosition = transform.TransformDirection(playerTwoPosition);
                playerTwoPosition *= speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerTwoPosition.x = playerTwoPosition.x - 0.1f;
                playerTwoPosition = transform.TransformDirection(playerTwoPosition);
                playerTwoPosition *= speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerTwoPosition.z = playerTwoPosition.z - 0.1f;
                playerTwoPosition = transform.TransformDirection(playerTwoPosition);
                playerTwoPosition *= speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerTwoPosition.x = playerTwoPosition.x + 0.1f;
                playerTwoPosition = transform.TransformDirection(playerTwoPosition);
                playerTwoPosition *= speed;
            }
        }
        
        playerTwo.Move(playerTwoPosition * Time.deltaTime);
    }

    //If one of the players collide with another object (including the other player), stop. This solves
    //a bug with the CharacterController.Move wanting to finish it's 'lerp' or whatever it is before
    //changing the movement direction. It is slightly weird that the only way I discovered to solve this
    //was to set a MoveTowards Vector... Also if I get rid of the checks for the players in the Update() 
    //loop, this part also doesn't work.
    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Wall") {
            playerTwoPosition = Vector3.MoveTowards(playerTwoPosition, Vector3.zero, 1.0f);
        }

        else if (hit.gameObject.tag == "Enemy") {
            Destroy(hit.gameObject);
        }
    }
}