using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGems_Boxes : MonoBehaviour {

    private float speed = 30;
    private float forwardBound = 15;
    private PlayerControl playerControllerScript;

    // Start is called before the first frame update
    void Start() {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update() {
        if (playerControllerScript.gameOver == false) {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.x > forwardBound) {
            Destroy(gameObject);
        }
    }
}
