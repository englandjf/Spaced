using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class EnemyMoveScript : MonoBehaviour {

	public string UnitName;
	public ParticleSystem PSTrail;
	public ParticleSystem PSDeath;
	public ProjectileScript PS;
	// Use this for initialization
	private DamageScript _DS;
	private EnemyValue _EV;
	private EnemyPersonality _EP;
	private EnemyState _CurrentState = EnemyState.MOVE;
	private int ProjectilesFired = 0;
	private int _FireRate = 2;
	private PersonalityValue _PV;

	public enum EnemyPersonality
	{
		AGGRESSIVE = 0,
		DEFENSIVE,
		SUICIDAL
	}

	public enum EnemyState
	{
		MOVE,
		FIRE,
		FIREANDMOVE,
	}

	void Start () {
		GetComponent<BoxCollider>().isTrigger = true;
		_EV = GameDataContainer.Instance.GDC.EnemyValues.Find(o => o.Name == UnitName);
		if(_EV != null)
		{
			_DS = new DamageScript(_EV.Health,OnDeath);
			Debug.Log(_EV.Personality);
			if(_EV.Personality)
			{
				PickPersonality();
			}
		}
		else
		{
			Debug.LogWarning("NO DATA FOUND");
			Destroy(this.gameObject);
		}
	}

	private void PickPersonality()
	{
		_EP = (EnemyPersonality)(int)Random.Range(0,3);
		Debug.Log(_EP.ToString());
		_PV = GameDataContainer.Instance.GDC.PersonalityValues.Find(o => o.Name == _EP.ToString());
		if(_PV != null)
		{
			_FireRate = _PV.FireRate;
		}
		else
		{
			Debug.LogWarning("Issue finding personality");
		}
		Invoke("DecideNextMove",5.0f);
		Invoke("FireProjectile",_FireRate);
	}

	private void DecideNextMove()
	{
		EnemyState NextState = EnemyState.MOVE;
		int number = Random.Range(0,100);
		switch(_EP)
		{
		case EnemyPersonality.AGGRESSIVE:
			if(number <= 50)
			{
				NextState = EnemyState.FIREANDMOVE;
			}
			else if(number <= 75)
			{
				NextState = EnemyState.FIRE;
			}
			break;
		case EnemyPersonality.DEFENSIVE:
			if(number <= 50)
			{
				NextState = EnemyState.FIRE;
			}
			else if(number <= 75)
			{
				NextState = EnemyState.MOVE;
			}
			break;
		case EnemyPersonality.SUICIDAL:
			NextState = EnemyState.FIREANDMOVE;
			break;
		}
		_CurrentState = NextState;
		Invoke("DecideNextMove",5.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(_CurrentState == EnemyState.MOVE || _CurrentState == EnemyState.FIREANDMOVE)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,HelperScript.PlayerRef.transform.position,Time.deltaTime * _EV.Speed);
		}
		this.transform.LookAt(HelperScript.PlayerRef.transform.position,this.transform.up);
	}

	private void FireProjectile()
	{
		if(_CurrentState == EnemyState.FIRE || _CurrentState == EnemyState.FIREANDMOVE)
		{
			ProjectileScript PSTemp = Instantiate(PS,this.transform.position,PS.transform.rotation);
			PSTemp.Setup(HelperScript.PlayerRef.transform.position,this.transform.rotation.eulerAngles +  new Vector3(90,0,0),"",true,null);
		}
		Invoke("FireProjectile",_FireRate);
	}

	private void OnDeath()
	{
		GamePlayHelper.Instance.PIC.PlayerScore++;
		Debug.Log(GamePlayHelper.Instance.PIC.PlayerScore);
		if(PSDeath != null)
		{
			Instantiate(PSDeath,this.transform.position,this.transform.rotation);
		}
		CancelInvoke("DecideNextMove");
		CancelInvoke("FireProjectile");
		Destroy(this.gameObject);
	}

	public void HasBeenHit(int amount)
	{
		_DS.TakeDamage(amount);
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "player")
		{
			PlayerShipScript PPS = c.GetComponent<PlayerShipScript>();
			if(PPS != null)
			{
				PPS.DamageShip(1);
			}

		}
	}

}
