using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public enum Axis
{
	X,
	Y
}

public class AsteroidManager : MonoBehaviour
{
	public static AsteroidManager Instance { get; private set; }

	public AsteroidController asteroidPrefab;

	[SerializeField] private Vector2 spawnDelayRange;
	[SerializeField] private Vector2 speedRange;
	[SerializeField] private Vector2 scaleRange;

	[SerializeField] private MyPointManager spawnPoint;
	[SerializeField] private MyPointManager targetPoint;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		StartSpawn();
	}

	private void StartSpawn()
	{
		IEnumerator Do()
		{
			while (true)
			{
				var spawnPosition = spawnPoint.GetPosition();

				var current = Instantiate(asteroidPrefab, spawnPosition, quaternion.identity);
				current.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);

				var speed = Random.Range(speedRange.x, speedRange.y);

				var direction = targetPoint.GetPosition() - spawnPosition;
				current.Move(direction, speed);

				var delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
				yield return new WaitForSeconds(delay);
			}
			// ReSharper disable once IteratorNeverReturns
		}

		StartCoroutine(Do());
	}
}