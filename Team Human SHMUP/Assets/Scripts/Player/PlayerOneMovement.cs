using UnityEngine;
using System.Collections;

public class PlayerOneMovement : MonoBehaviour {
    //Player 1 starting position
    public Vector3 playerOnePosition = new Vector3(0, 0, 0);

    //The CharacterController
    CharacterController playerOne;

    public float speed = 1f;

    void Start() {
        playerOne = GameObject.FindGameObjectWithTag("Player 1").GetComponent<CharacterController>();
    }

    void Update() {

        if (Input.GetKey("w"))
        {
            playerOnePosition.z = playerOnePosition.z + 0.1f;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }
        else if (Input.GetKey("a"))
        {
            playerOnePosition.x = playerOnePosition.x - 0.1f;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }
        else if (Input.GetKey("s"))
        {
            playerOnePosition.z = playerOnePosition.z - 0.1f;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }
        else if (Input.GetKey("d"))
        {
            playerOnePosition.x = playerOnePosition.x + 0.1f;
            playerOnePosition = transform.TransformDirection(playerOnePosition);
            playerOnePosition *= speed;
        }

        playerOne.Move(playerOnePosition * Time.deltaTime);
    }

    //If one of the players collide with another object (including the other player), stop. This solves
    //a bug with the CharacterController.Move wanting to finish it's 'lerp' or whatever it is before
    //changing the movement direction. It is slightly weird that the only way I discovered to solve this
    //was to set a MoveTowards Vector... Also if I get rid of the checks for the players in the Update() 
    //loop, this part also doesn't work.
    void OnControllerColliderHit(ControllerColliderHit hit)  {
        if (hit.gameObject.tag == "Wall") {
            playerOnePosition = Vector3.MoveTowards(playerOnePosition, Vector3.zero, 1.0f);
        }

        else if (hit.gameObject.tag == "Enemy")  {
            Destroy(hit.gameObject);
        }
    }
}