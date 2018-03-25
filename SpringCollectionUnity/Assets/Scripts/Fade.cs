using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public Color c1;
    public Color c2;
    public bool fadeOut;
    public Image imgu;
	// Use this for initialization
	void Start ()
    {
        imgu = GetComponent<Image>();
        StartCoroutine(BeginFade());
	}
	public void FadeOutIn(bool b)
    {
        fadeOut = b;
    }
	// Update is called once per frame
    IEnumerator BeginFade()
    {
        yield return new WaitForSeconds(1.5f);
        FadeOutIn(true);
    }
	void Update ()
    {
	    if(fadeOut)
        {
            imgu.color = Color.Lerp(imgu.color, c1, 2f * Time.deltaTime);
        }
        if (!fadeOut)
        {
            imgu.color = Color.Lerp(imgu.color, c2, 2f * Time.deltaTime);
        }
    }
}
