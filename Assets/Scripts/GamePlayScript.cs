using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayScript : MonoBehaviour {

	public PlayerInformationClass PIC;

	// Use this for initialization
	void Start () {
		GamePlayHelper.Instance = this;
		PIC = new PlayerInformationClass();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public static class GamePlayHelper
{
	public static GamePlayScript Instance;
}
