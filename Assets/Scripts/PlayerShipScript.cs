using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipScript : MonoBehaviour {
	
	public ProjectileScript PSSlot1;
	public ProjectileScript PSSlot2;

	public Transform FLGun;
	public Transform FRGun;

	private Vector3 _MousePos;
	private bool _SecondFireDelay = false;

	bool LeftGunFire = true;

	// Use this for initialization
	void Start () {
		HelperScript.PlayerRef = this.gameObject;
	}

	void Update()
	{
		_MousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(Camera.main.transform.position.y);
		this.transform.LookAt(_MousePos,this.transform.up);
		this.transform.rotation = Quaternion.Euler(new Vector3(-90,this.transform.rotation.eulerAngles.y+180));

		if(Input.GetMouseButtonDown(0))
		{
			ProjectileScript PSTemp = Instantiate(PSSlot1,(LeftGunFire?FLGun:FRGun).position,PSSlot1.transform.rotation);
			LeftGunFire = !LeftGunFire;
			PSTemp.Setup(_MousePos,this.transform.rotation.eulerAngles,"",null);
		}
		if(Input.GetMouseButtonDown(1) && !_SecondFireDelay)
		{
			ProjectileScript PSTemp = Instantiate(PSSlot2,this.transform.position,Quaternion.Euler(new Vector3(0,180,0)));
			PSTemp.Setup(_MousePos,this.transform.rotation.eulerAngles +  new Vector3(90,0,0),"Missile1",DelayCoolback);
			_SecondFireDelay = true;
		}
	}

	private void DelayCoolback(int time)
	{
		Invoke("SecondaryDelay",(float)time);
	}

	private void SecondaryDelay()
	{
		_SecondFireDelay = false;
	}
}
