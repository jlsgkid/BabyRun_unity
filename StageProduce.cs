using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProduce : MonoBehaviour {

	const int StageTipSize = 30;

	int currentTipIndex;
	public Transform character;
	public GameObject[] stageTips;
	public int startTipindex;
	public int preInstantiate;
	public List<GameObject> generateStagelist = new List<GameObject> ();


	// Use this for initialization
	void Start () {

		currentTipIndex = startTipindex - 1;
		UpdateStage (preInstantiate);
	}
	
	// Update is called once per frame
	void Update () {

		int charaPosIndex = (int)(character.position.z / StageTipSize);

		if(charaPosIndex + preInstantiate > currentTipIndex){

			UpdateStage (charaPosIndex + preInstantiate);
		}
	}

	void UpdateStage ( int toTipIndex){

		if( toTipIndex <= currentTipIndex){
			return;
		}

		for( int i = currentTipIndex +1; i<= toTipIndex; i++){

			GameObject stageObj = GenerteStage (i);
			generateStagelist.Add (stageObj);
		}

		while (generateStagelist.Count > preInstantiate + 2)
			DestroyOldStage ();

		currentTipIndex = toTipIndex;

	}

	GameObject GenerteStage( int tipIndex){

		int nextStageTip = Random.Range (0, stageTips.Length);

		GameObject stageObj = (GameObject)Instantiate (
			stageTips[nextStageTip],
			new Vector3(0, 0, tipIndex * StageTipSize),
			Quaternion.identity		
		);

		return stageObj;
	}

	void DestroyOldStage(){

		GameObject oldStage = generateStagelist [0];
		generateStagelist.RemoveAt (0);
		Destroy (oldStage);
	}

}
