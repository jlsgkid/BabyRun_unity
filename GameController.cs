using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {

	public NejikoContro nejiko;
	public Text scoreLabel;
	public LifePanel lifePanel;
	AudioSource endSound;

	// Use this for initialization
	void Start () {
		endSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		int score = CalcScore ();
		scoreLabel.text = "Score : " + score + "m"; 

		lifePanel.UpdateLife ( nejiko.Life());

		if( nejiko.Life() <= 0){
			 
			endSound.Play ();
			enabled = false;

			if( PlayerPrefs.GetInt("HighScore") < score){

				PlayerPrefs.SetInt ("HighScore", score);
			}

			Invoke ( "ReturnToTitle" , 2.0f);
		}
	}

	int CalcScore(){

		return (int)nejiko.transform.position.z;
	}

	void ReturnToTitle(){

		SceneManager.LoadScene("Title");

	}
}
