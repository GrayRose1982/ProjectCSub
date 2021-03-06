using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseCharacter
{
	public string name;
	public string id;
	public int hp;
	public int armor;
	public Sprite sprite;

	public BaseCharacter ()
	{
	}

	public BaseCharacter (BaseCharacter bc)
	{
		name = bc.name;
		id = bc.id;
		hp = bc.hp;
		armor = bc.armor;
		sprite = bc.sprite;
	}
}
