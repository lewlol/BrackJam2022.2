using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShake : UnityEngine.MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform planetTransform;

	// How long the object should shake for.
	public float shakeDuration = 0.1f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.4f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (planetTransform == null)
		{
			planetTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = planetTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			planetTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			planetTransform.localPosition = originalPos;
			shakeDuration = 0f;
		}
	}
}
