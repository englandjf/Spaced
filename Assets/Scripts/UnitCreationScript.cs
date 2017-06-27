using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreationScript : MonoBehaviour {

	[Header("Enemies")]
	public EnemyMoveScript EMS;
	public EnemyMoveScript EMS2;

	// Use this for initialization
	void Start () {
		
		InvokeRepeating("CreateEnemy",5.0f,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateEnemy()
	{
		EnemyMoveScript EMSTemp = (EnemyMoveScript)Instantiate(EMS2,new Vector3(0,0,0),EMS2.transform.rotation);
		EMSTemp.UnitName = "Enemy1";

	}
}
