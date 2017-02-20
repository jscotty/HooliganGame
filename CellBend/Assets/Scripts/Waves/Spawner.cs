// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System;
using System.Collections.Generic;

public class Spawner {

	/// <summary>
	/// Spawn object
	/// </summary>
	/// <param name="info">SpawnInfo struct with all information included.</param>
	/// <param name="count">Count of spawned objects.</param>
	public void Spawn(SpawnInfo info ,int count){
		if(count > info.amount) return;
		GameObject spawnObject = (GameObject)GameObject.Instantiate(info.gameObject, info.spawnPosition.position, info.gameObject.transform.rotation);
		spawnObject.transform.SetParent(info.spawnPosition);
	}
}