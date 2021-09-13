using UnityEngine;

public class BorderController : MonoBehaviour
{
	[SerializeField] private Axis _axis;

	private void OnTriggerEnter2D(Collider2D other)
	{
		var teleport = other.GetComponent<ITeleport>();
		teleport.OnTeleport();

		var current = other.transform.position;

		if (_axis == Axis.X) current.x *= -1;
		else current.y *= -1;

		other.transform.position = current;
	}
}