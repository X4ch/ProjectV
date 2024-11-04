using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Rigidbody2D carRigidbody2D;

    private float steeringInput;
    private float accelerationInput;

    [Header("Car Settings")]
    public float driftFactor = 0.9f;
    public float accelerationFactor = 15.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f;
    public float dragFactor = 3.0f;
    public float backwardSpeedFactor = 0.5f;

    [Header("Drift Settings")]
    public GameObject driftMarkPrefab; // Drag DriftMarkPrefab here in the inspector
    public float driftAngleThreshold = 20f; // Angle threshold for drifting
    public float driftMarkLifetime = 1.5f; // Lifetime of drift marks in seconds

    private TrackManager trackManager;
    private GameObject audioManager;

    private float velocityVsUp;
    private float rotationAngle;

    private bool isDrifting = false;
    private bool isTrackRuning = false;

    public float getVelocity()
    {
        return velocityVsUp;
    }

    public void setVelocity(float velocity)
    {
        velocityVsUp = velocity;
        carRigidbody2D.velocity = Vector2.zero;
    }

    public float getRotation()
    {
        return rotationAngle;
    }

    public void setRotation(float rotation)
    {
        rotationAngle = rotation;
    }


    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void Start()
    {
        trackManager = FindObjectOfType<TrackManager>();
        rotationAngle = transform.rotation.eulerAngles.z;
    }

    private void OnEnable()
    {
        var carInput = new CarInput();
        carInput.Car.Enable();

        carInput.Car.Steering.performed += context => steeringInput = context.ReadValue<float>();
        carInput.Car.Steering.canceled += context => steeringInput = 0f;

        carInput.Car.Engine.performed += context => accelerationInput = context.ReadValue<float>();
        carInput.Car.Engine.canceled += context => accelerationInput = 0f;
    }

    public void setAccelerationInput(float input)
    {
        accelerationInput = input;
    }

    public float getAccelerationInput()
    {
        return accelerationInput;
    }

    private void FixedUpdate()
    {
        if (trackManager != null && trackManager.isTrackRunning)
        {
            // Capture the initial rotation angle before steering
            float initialRotationAngle = rotationAngle;

            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();

            float rotationChange = Mathf.Abs(rotationAngle - initialRotationAngle);
            if (Mathf.Abs(steeringInput) > 0 && rotationChange > driftAngleThreshold)
            {
                isDrifting = true;
                SpawnDriftMark();
            }
            else
            {
                isDrifting = false;
            }
        }

        
    }

    private void Update()
    {
        //Debug.Log("Speed:" + velocityVsUp);
        //if (velocityVsUp <= 0.1)
        //{
        //    audioManager.GetComponent<AudioManager>().PlayEngineIdle();
        //}
        //else if (velocityVsUp < maxSpeed / 3 && velocityVsUp > 0)
        //{
        //    audioManager.GetComponent<AudioManager>().PlayEngineLow();
        //}
        //else if (velocityVsUp >= maxSpeed / 3 && velocityVsUp < maxSpeed * 2 / 3)
        //{
        //    audioManager.GetComponent<AudioManager>().PlayEngineMid();
        //}
        //else if (velocityVsUp >= maxSpeed * 2 / 3)
        //{
        //    audioManager.GetComponent<AudioManager>().PlayEngineFast();
        //}

        isTrackRuning = trackManager.isTrackRunning;

        if (isTrackRuning)
        {
            if (velocityVsUp <= 0.1)
            {
                audioManager.GetComponent<AudioManager>().StopEngineTest();
                audioManager.GetComponent<AudioManager>().PlayEngineIdle();
            }
            else
            {
                audioManager.GetComponent<AudioManager>().ChangeEnginePitch(1.5f * velocityVsUp / maxSpeed);
                audioManager.GetComponent<AudioManager>().PlayEngineTest();
            }

            if (isDrifting)
            {
                audioManager.GetComponent<AudioManager>().PlayDrift();
            }
            else
            {
                audioManager.GetComponent<AudioManager>().StopDrift();
            }
        }
        else
        {
            audioManager.GetComponent<AudioManager>().StopEngineTest();
            audioManager.GetComponent<AudioManager>().StopDrift();
        }
        
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // Clamping the speed
        if (accelerationInput > 0 && velocityVsUp > maxSpeed)
            return;
        if (accelerationInput < 0 && velocityVsUp < -maxSpeed * backwardSpeedFactor) 
            return;
        if (accelerationInput > 0 && carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            return;

        // Apply drag if there is no acceleration input, so the car slows down when not accelerating
        if (Mathf.Abs(accelerationInput) < Mathf.Epsilon)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, dragFactor, Time.fixedDeltaTime * dragFactor);
        }
        else
        {
            carRigidbody2D.drag = 0f; // No drag when there's acceleration input
        }

        // Create a force for the engine based on acceleration input
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply the force to push the car forward or backward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        // Limit the car's ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8f);  //Calculate differently
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity() // Changing parameters to see the effect exactly
    {
        // Get the forward and right directions of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        // Kill the orthogonal (sideways) velocity to avoid sliding
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private void SpawnDriftMark()
    {
        GameObject driftMark = Instantiate(driftMarkPrefab, transform.position, transform.rotation);        
        Destroy(driftMark, driftMarkLifetime);
    }

    private void OnDisable()
    {
        var carInput = new CarInput();
        carInput.Car.Disable();
    }
}
