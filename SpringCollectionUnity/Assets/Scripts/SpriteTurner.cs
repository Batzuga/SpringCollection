using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTurner : MonoBehaviour {
    // The target marker.
    Transform target;

    // Angular speed in radians per sec.
    float speed;

  

	// Use this for initialization
	void Start () {
        speed = 5f;
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}


    // Update is called once per frame
    void Update () {
        Vector3 targetDir = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(new Vector3(transform.forward.x,0,transform.forward.z), -targetDir, step, 0.0f);
        
        Debug.DrawRay(transform.position, newDir, Color.red);
        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
