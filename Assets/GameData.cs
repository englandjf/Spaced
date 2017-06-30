using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData : MonoBehaviour {

	public GameDataClass GDC;

	// Use this for initialization
	void Start () {
		GameDataContainer.Instance = this;
		StartCoroutine(LoadConfigValues());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadConfigValues()
	{
		TextAsset TA = Resources.Load("GameDataConfig") as TextAsset;
		yield return TA;
		Debug.Log(TA.text);
		if(TA != null)
		{
			Debug.Log("Starting");
			GDC = JsonUtility.FromJson<GameDataClass>(TA.text);
			yield return GDC;
			Debug.Log("Finishing");
			Debug.Log(JsonUtility.ToJson(GDC));
		}

	}
}

public static class GameDataContainer
{
	public static GameData Instance;
}

//Classes

[Serializable]
public class GameDataClass
{
	public List<EnemyValue> EnemyValues;
	public List<PersonalityValue> PersonalityValues;
	public PlayerInformation PlayerInfo;
	public List<ProjectileInformation> ProjectileInfo;
}

[Serializable]
public class EnemyValue
{
	public string Name;  
	public int Speed;
	public int Damage;
	public int Health;
	public bool Personality;
}

[Serializable]
public class PersonalityValue
{
	public string Name;
	public int FireRate;
}

[Serializable]
public class PlayerInformation
{
	public int StartSpeed;
	public int StartDamage;
	public int StartHealth;
	public List<string>OwnedProjectiles;
}

[Serializable]
public class ProjectileInformation
{
	public string Name;
	public int Damage;
	public int Speed;
	public int Cooldown;
}
	
	

