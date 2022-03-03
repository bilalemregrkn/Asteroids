using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RocketParticleController : MonoBehaviour
{
	private ParticleSystem _particleSystem;
	
	private bool _isActive;

	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Active(bool enabled)
	{
		if (_isActive == enabled) return;
		_isActive = enabled;
		var emission = _particleSystem.emission;
		emission.enabled = enabled;
	}
}