using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	[SerializeField] private WeaponController wController;
	[SerializeField] private BaseCharacter _character;
	[SerializeField] private UIPlayerController ui;

	[SerializeField] private SpriteRenderer sr;

	public BaseCharacter character {
		get { return _character; }
		set {
			_character = value;
			SetCharacter ();
		}
	}

	public bool isMeteor;
	public PolygonCollider2D polygon;

	void OnEnable ()
	{
		polygon = gameObject.AddComponent <PolygonCollider2D> ();
		polygon.isTrigger = true;

		if (!ui)
			return;

		ui.energy.MaxNum = _character.hp;
		ui.shield.MaxNum = _character.armor;
	}

	void OnDisable ()
	{
		Destroy (polygon);
	}

	public void TakeDamage (int damage, int armorBreak)
	{
		character.armor -= armorBreak;
		if (character.armor < 0) {
			character.hp -= damage;
			character.armor = 0;
		} else {
			character.armor -= Mathf.RoundToInt (damage / 2);
			if (character.armor < 0) {
				character.hp += character.armor * 2;
				character.armor = 0;
			}
		}

		Debug.Log (name + " after take damage. HP: " + character.hp + " Armor: " + character.armor);
		if (character.hp <= 0)
			DestroyPlayer ();
	}

	public void DestroyPlayer ()
	{
		enabled = false;
		Debug.LogWarning ("Kill " + transform.name + " or" + character.name);
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		string tag = hit.tag;
		int damage = 0, armorBreak = 0;
		switch (tag) {
		case "Laser":
			LaserController laser = hit.GetComponent<LaserController> ();
			damage = laser.laser.damage;
			armorBreak = laser.laser.armorBreak;

			laser.Disappeal ();
			break;
		case "Missile":
			MissileController missile = hit.GetComponent<MissileController> ();
			damage = missile.missile.damage;
			armorBreak = missile.missile.armorBreak;
		
			missile.Disappeal ();
			break;

		case "ItemDrop":
			if (!isMeteor) {
				ItemDropped itemDropped = hit.GetComponent<ItemDropped> ();
				GetItem (itemDropped);
			}
			break;
		default:
			break;
		}

		if (damage > 0 || armorBreak > 0) {
			TakeDamage (damage, armorBreak);
			SetUIPoint ();
		}
	}

	private void GetItem (ItemDropped itemDropped)
	{
		if (!itemDropped)
			return;

		switch (itemDropped.type) {
		case TypeItemDrop.Character:
			character = new BaseCharacter (itemDropped.baseCharacter);
			break;
		case TypeItemDrop.LaserWeapon:
			wController.mainWeapon = itemDropped.lwEntity;
			break;
		case TypeItemDrop.MissileWeapon:
			wController.secondWeapon = itemDropped.mwEntity;
			break;
		default:
			break;
		}
	}

	private void SetCharacter ()
	{
		sr.sprite = _character.sprite;
	}

	private void SetUIPoint ()
	{
		if (!ui)
			return;

		ui.energy.current = _character.hp;
		ui.shield.current = _character.armor;
	}
}
