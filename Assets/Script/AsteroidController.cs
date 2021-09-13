using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public interface ITeleport
{
	public void OnTeleport();
}

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour , ITeleport 
{
	private Rigidbody2D _rigidbody;
	private Collider2D _collider;

	private float _speed;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();

		IEnumerator Do()
		{
			yield return new WaitForSeconds(.8f);
			_collider.enabled = true;
		}

		StartCoroutine(Do());
	}

	public void Move(Vector3 direction, float speed)
	{
		_speed = speed;
		_rigidbody.AddForce(direction * speed);
	}

	public void Split()
	{
		var targetScale = transform.localScale.x / 2;
		if (targetScale > 0.6)
		{
			var prefab = AsteroidManager.Instance.asteroidPrefab;
			for (int i = 0; i < 2; i++)
			{
				var asteroidController = Instantiate(prefab, transform.position, quaternion.identity);
				asteroidController.transform.localScale = transform.localScale / 2;

				var direction = _rigidbody.velocity.normalized + new Vector2(Random.value, Random.value);
				asteroidController.Move(direction, _speed*5);
			}
		}

		Destroy(gameObject);
	}

	public void OnTeleport()
	{
		_collider.enabled = false;
		IEnumerator Do()
		{
			yield return new WaitForSeconds(1);
			_collider.enabled = true;
		}

		StartCoroutine(Do());
	}
}