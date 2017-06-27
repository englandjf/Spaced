using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class EnemyMoveScript : MonoBehaviour {

	public string UnitName;
	public ParticleSystem PSTrail;
	public ParticleSystem PSDeath;
	// Use this for initialization
	private DamageScript DS;
	private EnemyValue EV;

	void Start () {
		GetComponent<BoxCollider>().isTrigger = true;
		EV = GameDataContainer.Instance.GDC.EnemyValues.Find(o => o.Name == UnitName);
		if(EV != null)
		{
			DS = new DamageScript(EV.Health,OnDeath);
		}
		else
		{
			Debug.LogWarning("NO DATA FOUND");
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = Vector3.MoveTowards(this.transform.position,HelperScript.PlayerRef.transform.position,Time.deltaTime * EV.Speed);
		this.transform.LookAt(HelperScript.PlayerRef.transform.position,this.transform.up);
	}

	private void OnDeath()
	{
		GamePlayHelper.Instance.PIC.PlayerScore++;
		Debug.Log(GamePlayHelper.Instance.PIC.PlayerScore);
		if(PSDeath != null)
		{
			Instantiate(PSDeath,this.transform.position,this.transform.rotation);
		}
		Destroy(this.gameObject);
	}

	public void HasBeenHit(int amount)
	{
		DS.TakeDamage(amount);
	}

}
