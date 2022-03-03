using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[Header("Fire")] [SerializeField] private BulletController bulletPrefab;
	[SerializeField] private Transform bulletNest;
	[Range(0, 1f)] [SerializeField] private float shootDelay;

	[Header("Move")] [Range(100, 1000)] [SerializeField]
	private float movePower;

	[Range(100, 1000)] [SerializeField] private float rotatePower;
	[Range(5, 80)] [SerializeField] private float speedLimit;

	[Header("Effect")] [SerializeField] private RocketParticleController rocketParticle;

	private Rigidbody2D _rigidbody;

	private bool IsPressFront => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

	private bool IsPressLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
	private bool IsPressRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
	private bool IsPressShoot => Input.GetMouseButton(0);

	private bool _isAllowShoot = true;
	
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (IsPressFront)
			_rigidbody.AddForce(transform.up * movePower * Time.deltaTime);

		if (IsPressRight || IsPressLeft)
		{
			var currentRotateSpeed = !IsPressRight ? rotatePower : -rotatePower;
			transform.Rotate(Vector3.forward * currentRotateSpeed * Time.deltaTime);
		}

		if (IsPressShoot && _isAllowShoot)
			Shoot();

		rocketParticle.Active(IsPressFront || IsPressRight || IsPressLeft);
	}

	private void FixedUpdate()
	{
		_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, speedLimit);
	}

	private void Shoot()
	{
		var bullet = Instantiate(bulletPrefab, bulletNest.position, quaternion.identity);
		bullet.Move(transform.up);

		_isAllowShoot = false;

		IEnumerator Do()
		{
			yield return new WaitForSeconds(shootDelay);
			_isAllowShoot = true;
		}

		StartCoroutine(Do());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Asteroid")) return;
		Debug.Log("GameOver");
		Time.timeScale = 0;
	}
}