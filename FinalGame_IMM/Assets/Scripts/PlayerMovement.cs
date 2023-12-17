using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 130f;

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed;

        // Apply the movement to the player character
        transform.Translate(movement * Time.deltaTime * moveSpeed);
    }
}