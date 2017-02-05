using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TiteController : MonoBehaviour {

	public Text highScore;
	public AudioSource btSound;
	AudioSource stSound;

	// Use this for initialization
	void Start () {
		stSound = GetComponent<AudioSource> ();

		highScore.text = "High Score :" + PlayerPrefs.GetInt ("HighScore") + "m";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartButtonClicked(){

		if (!stSound.isPlaying){
			//播放音乐
			stSound.Stop();
		}
		btSound.Play();

		//Application.loadedLevel
		SceneManager.LoadScene("Main");
	}
}
