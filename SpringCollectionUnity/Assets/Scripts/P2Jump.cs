using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2Jump : MonoBehaviour {

    public Text t;
    public int score;
    public float maxJump;
    public float dammage;
    public float maxdistanssi;
    public float distanssi;
    public Vector2 startTouchPos;
    public Vector2 touchPos;
    public Rigidbody2D rb;
    public float testForce;
    public GameObject camra;
    public bool canJump;
    public bool drawingray;
    public Text t2;
    public float speed;
    public float tempFloat;
    public float jumppowerr;
    public GameObject block;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        t.text = "Score: 0";
        maxJump = 620f;
        speed = 2.8f;
        PlayerPrefs.SetInt("P2Score", score);
	}
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Fade").GetComponent<Fade>().FadeOutIn(false);
        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel("P3");
    }
	// Update is called once per frame
	void Update ()
    {
        
        GetTouch();
        if (Input.GetKey("r"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKey("j"))
        {
            jumppowerr += 1f * Time.deltaTime;
            if (jumppowerr > 1)
            {
                jumppowerr = 1;
            }         
        }

        if (Input.GetKey("k"))
        {
            jumppowerr -= 1f * Time.deltaTime;
            if (jumppowerr < 0)
            {
                jumppowerr = 0;
            }
        }

       if(Input.GetKeyDown("f"))
        {
            TestJump();
        }
        camra.transform.position = Vector3.Lerp(camra.transform.position, new Vector3(transform.position.x + 5.5f, 0f, -10f), 1f * Time.deltaTime);
        transform.Translate(-Vector2.right * Time.deltaTime * speed);
        
        t2.text = "Jump Power: " + jumppowerr.ToString("F");
	}
    void GetTouch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == "GetToch")
                    {
                        startTouchPos = Input.GetTouch(0).position;
                        touchPos = Input.GetTouch(0).position;
                        drawingray = true;
                    }
                }
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == "GetToch")
                    {
                        touchPos = Input.GetTouch(0).position;
                        if (distanssi > 500)
                        {
                            distanssi = 500;
                        }
                        dammage = 1f / maxdistanssi * distanssi;
                       
                    }
                }
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == "GetToch")
                    {
                        if(canJump)
                        {                           
                            rb.AddForce(Vector2.up * maxJump * dammage);
                            canJump = false;
                        }
                    }
                }
            }
        }


        // distanssi = Calculate();
        distanssi = Vector2.Distance(startTouchPos, touchPos);
    }
    void DrawRay()
    {
        
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector3(transform.position.x, transform.position.y - tempFloat, transform.position.z));

        if (hit.collider.tag == "Ground")
        {
            canJump = true;
        }
    }
    void TestJump()
    {
        if(canJump)
        {
            rb.AddForce(Vector2.up * maxJump * jumppowerr);
            canJump = false;
            jumppowerr = 0;
        }
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Points")
        {
            score++;
            t.text = "Score: " + score;
            Instantiate(block, new Vector3(transform.position.x + 27f, Random.Range(3f, -2f), 0), Quaternion.identity);
            PlayerPrefs.SetInt("P2Score", score);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if (col.gameObject.tag == "Pipe")
        {
            speed = 0f;
            StartCoroutine(GameOver());
        }
    }
    void OnColliderEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Pipe")
        {
            speed = 0f;
        }
    }
}
