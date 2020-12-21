using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f; //X speed
    [Tooltip("In M")] [SerializeField] float xRange = 40f; //X Range
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f; // Y speed
    [Tooltip("In M")] [SerializeField] float yRange = 20f; // Y range
    [SerializeField] GameObject[] guns;

    float xThrow;
    float yThrow;

    [Header("Screen-Position based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;
    
    [Header("Control-Throw based")]
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float controlPitchFactor = -20f;

    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //moves the ship on the X/Y axis relative to the camera within a certain range
    void CheckMovement()
    {
        //X movement
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPost = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPost, -xRange, xRange);

        //Y movement
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPost = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawNewYPost, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    
    // Rotate the ship for relative location
    void CheckRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    // Fire the guns bullets using the key
    void CheckFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            ActivateGuns();
        }
        else 
        {
            DeactivateGuns();
        }
    }

    // activate guns
    void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    // deactivate guns
    void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    //trigger on colision with "collision"
    void OnCollisionEnter(Collision collision)
    {
        print("something collided");
    }

    //Handling player death sequence
    void OnPlayerDeath() // called by string reference
    {
        print("Controls frozen!");
        isControlEnabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled) 
        {
            CheckMovement();
            CheckRotation();
            CheckFiring();
        }
        
    }
}
