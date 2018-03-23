using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1Shoot : MonoBehaviour {

    public float maxForce;
    public float curForce;

    public GameObject groundPiece;

    public bool moveRight;
    public bool moveLeft;

    public Image nextSeed;

    public GameObject elf1;
    public GameObject elf2;

    public GameObject spring;

    public float springMinPos;
    public float springMaxPos;

    public bool animestate;

    public float timer;

    public GameObject camerra;

    public Vector2 startTouchPos;
    public Vector2 touchPos;
    public float distanssi;
    public float maxdistanssi;

    public float dammage;
    public Slider slidd;

	// Use this for initialization
	void Start ()
    {
        camerra = GameObject.Find("Main Camera");
        groundPiece = Resources.Load("Prefabs/Ground") as GameObject;
        GenerateMap();
	}
	void GenerateMap()
    {
        float f = 0;
        for(int i = 0; i < 150; i++)
        {
            f += 7f;
            Instantiate(groundPiece, new Vector3(f, 2f, 0f), Quaternion.identity);
            Instantiate(groundPiece, new Vector3(-f, 2f, 0f), Quaternion.identity);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        GetMovement();
        GetTouch();
        AnimateTontu();
        camerra.transform.position = Vector3.Lerp(camerra.transform.position, new Vector3(transform.position.x, camerra.transform.position.y, -10), 2f * Time.deltaTime);
    }
    
    void GetTouch()
    {

        for(int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPos = Input.GetTouch(0).position;
                touchPos = Input.GetTouch(0).position;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchPos = Input.GetTouch(0).position;
            }
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if(distanssi > 500)
                {
                    distanssi = 500;
                }
                if (distanssi < 500)
                {

                }
                dammage = 1f / maxdistanssi * distanssi;
                //boomshakalaka
            }
        }
        
       
        // distanssi = Calculate();
        distanssi = Vector2.Distance(startTouchPos, touchPos);
        Text o;
        o = GameObject.Find("shib").GetComponent<Text>();
        o.text = "" + distanssi;
    }
    float Calculate()
    {
        float f1 = 0f, f2 = 0f;
        if(startTouchPos.y < 0)
        {
            f1 = -startTouchPos.y;
        }
        if(startTouchPos.y > 0)
        {
            f1 = startTouchPos.y;
        }
        if(touchPos.y < 0)
        {
            f2 = -touchPos.y;
        }
        if(touchPos.y > 0)
        {
            f2 = touchPos.y;
        }
        return f1 + f2;
    }
    public void OnValueChange()
    {
        spring.transform.localPosition = new Vector3(0, 0.7f - slidd.value, 0.45f);
    }
    void AnimateTontu()
    {
        Debug.Log("AnimateTontu");
        timer += 1f * Time.deltaTime;
        Debug.Log(timer);
        
        if(timer > 0.15f)
        {
            if (moveLeft || moveRight)
            {
                if(animestate)
                {
                    elf1.transform.rotation = Quaternion.Euler(0, 180, -10f);
                    elf2.transform.rotation = Quaternion.Euler(0, 0, 10f);
                    animestate = !animestate;
                    timer = 0f;
                    return;
                }
                if(!animestate)
                {
                    elf1.transform.rotation = Quaternion.Euler(0, 180, 10f);
                    elf2.transform.rotation = Quaternion.Euler(0, 0, -10f);
                    animestate = !animestate;
                    timer = 0f;
                    return;
                }
                
            }
            else
            {
                elf1.transform.rotation = Quaternion.Euler(0, 180, 0);
                elf2.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
           
        }
        
        
    }
    void GetMovement()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 4f);
        }
        if (moveLeft)
        {
            transform.Translate(Vector2.right * Time.deltaTime * -4f);
        }
    }
    public void Move(int i)
    {
        if(i == 1)
        {
            moveRight = true;
        }
        if(i == 2)
        {
            moveLeft = true;
        }
    }
    public void NoMove(int i)
    {
        if (i == 1)
        {
            moveRight = false;
        }
        if (i == 2)
        {
            moveLeft = false;
        }
    }
}
