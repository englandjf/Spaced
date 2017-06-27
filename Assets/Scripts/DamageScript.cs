using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript
{
	private int _HealthTotal;
	private System.Action _OnKill;

	public DamageScript(int Health,System.Action KillAction)
	{
		_HealthTotal = Health;
		_OnKill = KillAction;
	}

	public void TakeDamage(int Amount)
	{
		_HealthTotal-= Amount;
		if(_HealthTotal <= 0)
		{
			if(_OnKill != null)
			{
				_OnKill();
			}
			else
			{
				Debug.LogWarning("No Kill Action");
			}
		}
	}

}
