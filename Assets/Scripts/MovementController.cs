using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float movingRange, inputRange, rotationRange;
    [SerializeField] float tweeningStrength = 0.5f;
    [SerializeField] float inputValue = 0f; // Current input value controlling movement (-5 to 5)
    private float currentPositionX, currentRotationZ; // Current x-position for smooth tweening
    [SerializeField] float targetPositionX, targetRotationZ;
    [SerializeField] Transform player;
    private Vector3 startPosition; // Initial position of the object
    //add an animation when you keep balacing for a while
    [SerializeField] Animator planeIdleAnim;
    [SerializeField] float idleTime, defaultIdleTime, cooldownTime;
    void Start()
    {
        inputValue = 0;
        cooldownTime=0;
        // Store the initial position
        startPosition = transform.position;
        currentPositionX = startPosition.x; // Set the initial current position
    }

    void Update()
    {
        // Handle input from arrow keys
        HandleInput();

        // Smoothly calculate the new position using exponential smoothing
        currentPositionX = currentPositionX + (targetPositionX - currentPositionX) * tweeningStrength;
        currentRotationZ = currentRotationZ + (targetRotationZ - currentRotationZ) * tweeningStrength;

        // Apply the new position
        transform.position = new Vector3(currentPositionX, startPosition.y, startPosition.z);
        player.rotation = Quaternion.Euler(player.rotation.x, player.rotation.y, targetRotationZ);

        //animHandler
        if (inputValue <= 0.05f && inputValue >= -0.05f)
        {
            idleTime -= Time.deltaTime;
        }
        else idleTime = defaultIdleTime;

        if (idleTime <= 0)
        {
            if (cooldownTime <= 0)
            {
                planeIdleAnim.ResetTrigger("isIdle");
                planeIdleAnim.SetTrigger("isIdle");
                idleTime = defaultIdleTime;
                cooldownTime = 10f;
            }
        }
        if (cooldownTime >= 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else cooldownTime = 0;
    }

    private void HandleInput()
    {
        inputValue = ArduinoReader.processedValue;

        /* Adjust inputValue with left/right arrow keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputValue -= 0.1f * Time.deltaTime * 10; // Reduce value
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            inputValue += 0.1f * Time.deltaTime * 10; // Increase value
        }
        inputValue = Mathf.Clamp(inputValue, -inputRange, inputRange);*/

        targetPositionX = Map(inputValue, -inputRange, inputRange, -movingRange, movingRange);
        targetPositionX = -targetPositionX;
        targetRotationZ = Map(inputValue, -inputRange, inputRange, -rotationRange, rotationRange);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
    }
}
