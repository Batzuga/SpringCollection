using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float movementSpeed = 5f;
    public float turningSpeed = 80f;


    Rigidbody rb;
    public float timer;
    public GameObject tonttu;
    public bool wiggleDir;

    private void Start()
    {
        movementSpeed = 5f;
        turningSpeed = 80f;
        rb = GetComponent<Rigidbody>();
        tonttu = GameObject.FindGameObjectWithTag("sprite");
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        Wiggle();
    }

    void Wiggle()
    {
        timer += 1f * Time.deltaTime;


        if (Input.GetKey("w"))
        {
            if (timer >= 0.18f)
            {
                if (wiggleDir)
                {

                    tonttu.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    timer = 0f;
                    wiggleDir = false;
                    Debug.Log("Wiggle1");
                    return;
                }
                if (!wiggleDir)
                {

                    tonttu.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    timer = 0f;
                    wiggleDir = true;
                    Debug.Log("Wiggle2");
                    return;
                }
            }

        }

    }
}
