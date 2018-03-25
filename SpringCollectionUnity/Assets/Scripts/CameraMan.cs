using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour {

    [SerializeField] GameObject Player;
    [SerializeField] Camera cam;

    Vector3 playerPos;


	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

	}

    private void FixedUpdate()
    {
    


    }

    // Update is called once per frame
    void Update () {

        playerPos = Player.transform.position;
        transform.position = Player.transform.position;
        transform.rotation = Player.transform.rotation;

    }
}
