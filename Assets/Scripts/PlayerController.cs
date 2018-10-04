using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 7f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3.5f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = 30f;

    float xThrow, yThrow;
    bool movementsAreEnabled = true;
	
	// Update is called once per frame
	void Update ()
    {
        if (movementsAreEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            setGunsActive(true);
        }
        else
        {
            setGunsActive(false);
        }
    }

    private void setGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            ParticleSystem particleSystem = gun.GetComponent<ParticleSystem>();
            particleSystem.enableEmission = isActive; 
        }
    }

    void StopMovements()
    {
        movementsAreEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = positionPitchFactor * transform.localPosition.y;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = positionYawFactor * transform.localPosition.x;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clambedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clambedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clambedXPos, clambedYPos, transform.localPosition.z);
    }
}
