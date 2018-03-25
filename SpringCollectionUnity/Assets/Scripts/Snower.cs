using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snower : MonoBehaviour {

    public GameObject player;
    ParticleSystem ps;
    
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        ps.transform.position = new Vector3(player.transform.position.x, 4f, player.transform.position.z);
        ps.transform.rotation = Quaternion.Euler(new Vector3(90.0f, player.transform.rotation.y, player.transform.rotation.z));
       


	}
}
