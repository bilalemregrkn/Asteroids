using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeleportCollider : MonoBehaviour
{
	private Collider2D _collider;

	[SerializeField] private Transform teleportObject;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		ResetCollider();
	}

	private void ResetCollider()
	{
		_collider.enabled = false;

		IEnumerator Do()
		{
			yield return new WaitForSeconds(.3f);
			_collider.enabled = true;
		}

		StartCoroutine(Do());
	}

	public void OnTeleport(Vector2 newPosition)
	{
		ResetCollider();
		teleportObject.position = newPosition;
		transform.localPosition = Vector2.zero;
	}
}