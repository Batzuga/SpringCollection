using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P1Score : MonoBehaviour {

    public int score;
    public bool gameOver;
    public Text t;
	// Use this for initialization
	void Start ()
    {
        score = 0;
        t.text = "" + score;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddScore()
    {
        score++;
        t.text = "" + score;
        PlayerPrefs.SetInt("P1Score", score);
    }
}
