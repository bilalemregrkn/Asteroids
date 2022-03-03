using UnityEngine;

public class BorderController : MonoBehaviour
{
	[SerializeField] private Axis axis;

	private void OnTriggerEnter2D(Collider2D other)
	{
		var teleport = other.GetComponent<TeleportCollider>();
		if(teleport == null) return;

		var current = other.transform.position;

		if (axis == Axis.X) current.x *= -1;
		else current.y *= -1;

		other.transform.position = current;
		
		teleport.OnTeleport(current);
	}
}