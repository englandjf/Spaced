using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleSystemHelper : MonoBehaviour {

	private ParticleSystem PS;

	// Use this for initialization
	void Start () {
		PS = GetComponent<ParticleSystem>();
		Invoke("KillMe",PS.main.duration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void KillMe()
	{
		Destroy(this.gameObject);
	}
}
