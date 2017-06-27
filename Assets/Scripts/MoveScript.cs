//#define OLDMOVE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
	
	private Rigidbody _RB;
	private Vector3 _MousePos;

	bool LeftGunFire = true;

	private PlayerInformation _PlayerRef;

	// Use this for initialization
	void Start () {
		HelperScript.PlayerRef = this.gameObject;
		_RB = GetComponent<Rigidbody>();
		//_PlayerRef = GameDataContainer.Instance.GDC.PlayerInfo;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckForInput();
	}

	void Update()
	{
		_PlayerRef = GameDataContainer.Instance.GDC.PlayerInfo;
		_MousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(Camera.main.transform.position.y);
		this.transform.LookAt(_MousePos,this.transform.up);
		this.transform.rotation = Quaternion.Euler(new Vector3(-90,this.transform.rotation.eulerAngles.y+180,0));
	}

	//up == forward
	private void CheckForInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			#if OLDMOVE
			_RB.AddForce(this.transform.up * _PlayerRef.StartSpeed);
			#else
			_RB.AddForce(Vector3.forward * _PlayerRef.StartSpeed);
			#endif
		}
		if(Input.GetKey(KeyCode.A))
		{
			#if OLDMOVE
			_RB.AddForce(this.transform.right * _PlayerRef.StartSpeed/2 * -1);
			#else
			_RB.AddForce(Vector3.right * _PlayerRef.StartSpeed * -1);
			#endif
		}
		if(Input.GetKey(KeyCode.S))
		{
			#if OLDMOVE
			_RB.AddForce(this.transform.up *_PlayerRef.StartSpeed/2 * -1);
			#else
			_RB.AddForce(Vector3.forward *_PlayerRef.StartSpeed * -1);
			#endif
		}
		if(Input.GetKey(KeyCode.D))
		{
			#if OLDMOVE
			_RB.AddForce(this.transform.right *_PlayerRef.StartSpeed/2);
			#else
			_RB.AddForce(Vector3.right * _PlayerRef.StartSpeed );
			#endif
		}

	}
}
