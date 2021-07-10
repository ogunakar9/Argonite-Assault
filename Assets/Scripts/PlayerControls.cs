using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")] [SerializeField] private float controlSpeed = 10f;
    [Tooltip("How far player moves horizontally")] [SerializeField] private float xRange = 11.5f;
    [Tooltip("How far player moves vertically")] [SerializeField] private float yRange = 8f;
    
    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] private GameObject[] lasers;

    [Header("Screen position based on tuning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float positionYawFactor = 2f;
    
    [Header("Player input based tuning")]
    [SerializeField] private float controlPitchFactor = -10;
    [SerializeField] private float controlRollFactor = -20f;
    
    private float yThrow, xThrow;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        var localPosition = transform.localPosition;
        
        float pitchDueToPosition = localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawxPos = transform.localPosition.x + xOffset;
        float clampedxPos = Mathf.Clamp(rawxPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawyPos = transform.localPosition.y + yOffset;
        float clampedyPos = Mathf.Clamp(rawyPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedxPos, clampedyPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
