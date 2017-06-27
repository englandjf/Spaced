using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(BoxCollider))]
public class ProjectileScript : MonoBehaviour {

	public List<ParticleSystem> _Trails;
	public ParticleSystem _DeathParticle;

	private Vector3 _Target;
	private Vector3 _EulerAng;
	private BoxCollider BC;
	private string _PT;
	private int _CooldownTime;
	private ProjectileInformation _PI;

	public void Setup(Vector3 Target,Vector3 CreationAngle,string ProjectileName,System.Action<int> DelayTime)
	{
		_Target = Target;
		_EulerAng = CreationAngle;
		if(!string.IsNullOrEmpty(ProjectileName))
		{
			_PT = ProjectileName;
			_PI = GameDataContainer.Instance.GDC.ProjectileInfo.Find(o => o.Name == _PT);
			if(_PI == null)
			{
				Debug.LogWarning("NO DATA FOUND");
				Destroy(this.gameObject);
			}
			DelayTime(_PI.Cooldown);
		}
	}

	// Use this for initialization
	void Start () {
		this.transform.rotation = Quaternion.Euler(_EulerAng);
		BC = GetComponent<BoxCollider>();
		BC.isTrigger = true;
		_Target.y = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(_Target != this.transform.position)
		{
			//this.transform.position = Vector3.MoveTowards(this.transform.position,_Target,Time.deltaTime * _Speed);
			if(string.IsNullOrEmpty(_PT))
			{
				this.transform.position += this.transform.up * Time.deltaTime * 10;
			}
			else if(_PT == "Missile1")
			{
				this.transform.position += this.transform.forward * Time.deltaTime * _PI.Speed * -1; //<- this is stupid
			}
		}
		else
		{

		}

		OutofBoundsCheck();
	}

	private void OutofBoundsCheck()
	{
		Vector3 BottomLeft = Camera.main.ScreenPointToRay(new Vector3(0,0)).GetPoint(Camera.main.transform.position.y);
		Vector3 TopRight = Camera.main.ScreenPointToRay(new Vector3(Screen.width,Screen.height)).GetPoint(Camera.main.transform.position.y);
		Debug.DrawLine(BottomLeft,TopRight);
		if(this.transform.position.x <= BottomLeft.x || this.transform.position.x >= TopRight.x || this.transform.position.z >= TopRight.z || this.transform.position.z <= BottomLeft.z)
		{
			Destroy(this.gameObject);
		}
	}
		
	void OnTriggerEnter(Collider a)
	{
		if(a.tag == "enemy")
		{
			a.GetComponent<EnemyMoveScript>().HasBeenHit((string.IsNullOrEmpty(_PT)?1:_PI.Damage));
			if(_DeathParticle != null)
			{
				Instantiate(_DeathParticle,this.transform.position,this.transform.rotation);
			}
			Destroy(this.gameObject);
		}
	}
}
