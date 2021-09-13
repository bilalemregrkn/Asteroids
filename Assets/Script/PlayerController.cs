using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour , ITeleport
{
	[SerializeField] private BulletController bulletPrefab;
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float shootDelay;

	private Rigidbody2D _rigidbody;
	private Collider2D _collider;

	private bool IsPressFront => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

	private bool IsPressLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
	private bool IsPressRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
	private bool IsPressShoot => Input.GetMouseButton(0);

	private bool _isAllowShoot = true;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();
	}

	private void Update()
	{
		if (IsPressFront)
			_rigidbody.AddForce(transform.up * speed);

		if (IsPressRight || IsPressLeft)
		{
			var currentRotateSpeed = !IsPressRight ? rotateSpeed : -rotateSpeed;
			transform.Rotate(Vector3.forward * currentRotateSpeed);
		}

		if (IsPressShoot && _isAllowShoot)
			Shoot();
	}

	private void Shoot()
	{
		var bullet = Instantiate(bulletPrefab, transform.position, quaternion.identity);
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
		if (other.CompareTag("Asteroid"))
		{
			Debug.Log("GameOver");
		}
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