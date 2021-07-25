using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ItemDroppedPooling : NetworkBehaviour
{
	public static ItemDroppedPooling pool;

	public GameObject itemDropped;
	public bool loadDone;

	public List<ItemDropped> items;

	void Start ()
	{
		items = new List<ItemDropped> ();
		pool = this;

		StartCoroutine (SpawnLaser (20));
	}

	public IEnumerator SpawnLaser (int number)
	{
		loadDone = false;
		for (int i = 0; i < number; i++) {
			GameObject go = (GameObject)Instantiate (itemDropped, transform);
			yield return new WaitForSeconds (0.005f);
			NetworkServer.Spawn (go);
			ItemDropped l = go.GetComponent<ItemDropped> ();
			if (l)
				ReturnLaser (l);
		}
		loadDone = true;
	}

	public ItemDropped GetLaser ()
	{
		ItemDropped result = null;

		if (items.Count > 0) {
			result = items [0];
			result.transform.SetParent (null);
			items.RemoveAt (0);
		} else {
			StartCoroutine (SpawnLaser (3));
		}

		return result;
	}

	public void ReturnLaser (ItemDropped l)
	{
		l.transform.SetParent (transform);
		l.gameObject.SetActive (false);
		items.Add (l);
	}
}
