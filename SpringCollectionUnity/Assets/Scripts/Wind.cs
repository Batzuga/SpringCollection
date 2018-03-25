using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {

    GameObject Player;
    Rigidbody rb;
    ParticleSystem ps;

    public float windy = 0;
    public int wdir = 0;
    float playerRotationY;
    float randomDuration;
    public float waitTime = 0;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = Player.GetComponent<Rigidbody>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
	}

    private void Awake()
    {
        randomDuration = Random.Range(1.0f, 2.0f);
        waitTime = Random.Range(15f, 25f);
    }

    // Update is called once per frame
    void Update () {
        wind(wdir);

    }




    void wind(int w)
    {     
        switch (w)
        {
            case 0:
                if (waitTime > 0) waitTime -= 0.05f;
                if (waitTime <= 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        wdir = Random.Range(1, 3);                     
                        if (wdir > 2) wdir = 2;
                    }
                }
                break;
            case 1:  
                windy += Random.Range(0.0025f, 0.01f);
                Player.transform.Rotate(new Vector3(0, windy * 0.85f));
                rb.AddForce(new Vector3(windy, 0, - windy * windy));
                if (windy >= randomDuration) wdir = 3;
             break;
            case 2:
                windy -= Random.Range(0.0025f, 0.01f);
                Player.transform.Rotate(new Vector3(0, windy * 0.85f));
                rb.AddForce(new Vector3(windy, 0,  windy * windy));
                if (windy <= randomDuration * -1) wdir = 3;
                break;

            case 3:
                windy = 0;
                randomDuration = Random.Range(1.0f, 3.0f);
                waitTime = Random.Range(15f, 25f);
                wdir = 0;
                break;

             
        }

        

    }




}
