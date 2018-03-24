using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedProjectile : MonoBehaviour { 

    public float launchSpeed;
    public float multiplier;

    public Rigidbody2D rb;
    public ParticleSystem ps;
    public Vector3 v3;
    public GameObject flower;

    public P1Score p1s;

    // Use this for initialization
	void Start ()
    {
        p1s = GameObject.Find("ScriptBlock").GetComponent<P1Score>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        v3 = transform.position;	
        
    }
    public void Launch(float f, float f2)
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.mass = f2;
        multiplier = f;
        Debug.Log(multiplier);
        rb.AddForce(Vector2.up * launchSpeed * multiplier * 10f);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "mound")
        {
            if (rb.velocity.y <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if(col.gameObject.tag == "mudspraw")
        {
            if(rb.velocity.y > 0)
            {
                //mudspraw
            }
            if(rb.velocity.y <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "mound")
        {
            if(rb.velocity.y <= 0)
            {
                Instantiate(ps, new Vector3(v3.x, v3.y, -1.2f), Quaternion.identity);
                Instantiate(flower, new Vector3(col.transform.GetChild(0).transform.position.x, col.transform.GetChild(0).transform.position.y + 0.5f, -1.0f), Quaternion.identity);
                p1s.AddScore();
                Destroy(this.gameObject);
            } 
        }
    }
}
