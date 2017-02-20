// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;
using System;

public class WaveController : MonoBehaviour {

	// class under construction. Working on more procedural spawning system.

	[Header("Wave information")]
	[Tooltip("Amount of waves")][SerializeField] private WaveInfo[] _info;
	private int _wave = 0;

	private Spawner _spawner;
	private int _count = 0;

	private float _spawnInterval = 1f;
	private float _spawnIntervalDecreasement = 0.01f;
	private float _spawnIntervalMin = 0.2f;
	private float _spawnTimer = 0f;

	void Start () {
		_spawner = new Spawner();
	}

	void Update() {
		//Temp system for debugging and testing.
		if (_spawnTimer < _spawnInterval) {
			_spawnTimer += Time.deltaTime;

			if(_spawnTimer > _spawnInterval){
				_count++;
				_spawnTimer -= _spawnInterval;
				foreach (SpawnInfo spawnInfo in _info[_wave].spawnInfo) {
					_spawner.Spawn(spawnInfo, _count); 
				}
				if (_spawnInterval > _spawnIntervalMin) _spawnInterval -= _spawnIntervalDecreasement;
			}

		}
	}

	/// <summary>
	/// Initializes all timer variables
	/// </summary>
	private void InitSpawnTimer(){
		_spawnInterval = _info[_wave].spawnInterval;
		_spawnIntervalDecreasement = _info[_wave].spawnIntervalDecreasement;
		_spawnIntervalMin = _info[_wave].spawnIntervalMin;
		_spawnTimer = _info[_wave].spawnTimer;
	}
}

[Serializable]
public class WaveInfo{
	[Tooltip("Time to spawn.")] public float spawnInterval = 3f;
	[Tooltip("Decreasement of time to spawn.")] public float spawnIntervalDecreasement = 0.01f;
	[Tooltip("Minimumn time interval")] public float spawnIntervalMin = 1f;
	[Tooltip("Current start time")] public float spawnTimer = 0f;
	public SpawnInfo[] spawnInfo;
}

[Serializable]
public struct SpawnInfo{
	[Tooltip("Spawnable object.")] public GameObject gameObject;
	[Tooltip("Transform position to spawn at.")]public Transform spawnPosition;
	[Tooltip("Amount of objects.")]public int amount;
}