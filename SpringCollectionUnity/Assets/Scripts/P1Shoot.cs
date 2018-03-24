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

    public GameObject seed;
    public SeedProjectile sp;
    public bool canshoot;

    public GameObject seedSpawn;
    public GameObject tempSeed;
    public float newValue;
    public int seedsLeft;
    public Text t1;
    public Text t2;

   // public SeedScript seedScript;

	// Use this for initialization
	void Start ()
    {
        canshoot = true;
        camerra = GameObject.Find("Main Camera");
        groundPiece = Resources.Load("Prefabs/Ground") as GameObject;
        seedSpawn = GameObject.Find("SeedLaunchPos");
        seed = Resources.Load("Prefabs/Seed") as GameObject;
        GenerateMap();
        newValue = Random.Range(1.0f, 20.0f);
        seedsLeft = 20;
	}
	void GenerateMap()
    {
        float f = 0;
        Instantiate(groundPiece, new Vector3(f, 2f, 0f), Quaternion.identity);
        for (int i = 0; i < 150; i++)
        {
            f += 4.8f;
            Instantiate(groundPiece, new Vector3(f, 2f, 0f), Quaternion.identity);
            Instantiate(groundPiece, new Vector3(-f, 2f, 0f), Quaternion.identity);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        GetMovement();
        if(canshoot)
        {
            GetTouch();
        }
       if(Input.GetKeyDown("f"))
        {
            TestshooT();
        }
        AnimateTontu();
        camerra.transform.position = Vector3.Lerp(camerra.transform.position, new Vector3(transform.position.x, camerra.transform.position.y, -10), 2f * Time.deltaTime);
        t1.text = "" + seedsLeft;
        t2.text = "" + newValue.ToString("0");
    }
    
    void GetTouch()
    {

        for(int i = 0; i < Input.touchCount; i++)
        {
            
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if(raycastHit.collider.name == "GetToch")
                    {
                        startTouchPos = Input.GetTouch(0).position;
                        touchPos = Input.GetTouch(0).position;
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
                        spring.transform.localPosition = new Vector3(0, 1f * -dammage + 0.7f, 0.45f);
                    }
                }
            }
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == "GetToch")
                    {
                        Shoot();
                        spring.transform.localPosition = new Vector3(0,0.7f, 0.45f);
                        canshoot = false;
                        StartCoroutine(Waitshoot());
                        //boomshakalaka
                    }
                } 
            }
        }
        
       
        // distanssi = Calculate();
        distanssi = Vector2.Distance(startTouchPos, touchPos);
    }
    IEnumerator Waitshoot()
    {
        yield return new WaitForSeconds(0.5f);
        canshoot = true;
    }
    void TestshooT()
    {
        if (seedsLeft > 0)
        {
            tempSeed = Instantiate(seed, seedSpawn.transform.position, Quaternion.identity);
            sp = tempSeed.GetComponent<SeedProjectile>();
            sp.multiplier = dammage;
            sp.Launch(0.8f, newValue);
            newValue = Random.Range(1.0f, 20.0f);
            seedsLeft--;
        }
    }
    void Shoot()
    {
        if(seedsLeft > 0)
        {
            tempSeed = Instantiate(seed, seedSpawn.transform.position, Quaternion.identity);
            sp = tempSeed.GetComponent<SeedProjectile>();
            sp.multiplier = dammage;
            sp.Launch(dammage, newValue);
            newValue = Random.Range(1.0f, 20.0f);
            seedsLeft--;
        }
       
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
    void AnimateTontu()
    {
        Debug.Log("AnimateTontu");
        timer += 1f * Time.deltaTime;
        
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
