using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
	private Rigidbody2D _rigidbody;

	[SerializeField] private float speed;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector3 direction)
	{
		_rigidbody.AddForce(speed * direction);

		IEnumerator Do()
		{
			yield return new WaitForSeconds(5);
			Destroy(gameObject);
		}

		StartCoroutine(Do());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag($"Asteroid"))
		{
			var asteroid = other.GetComponent<AsteroidController>();
			asteroid.Split();
			
			Destroy(gameObject);
		}
	}
}