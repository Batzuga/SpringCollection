using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyerOfWorlds : MonoBehaviour {

    public float f;
    public Text t;
	// Use this for initialization
	void Start ()
    {
	    if(f == 0)
        {
            f = 2f;
        }
        StartCoroutine (Destroyer());
	}
	
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(f);
        Destroy(gameObject);
    }
}
