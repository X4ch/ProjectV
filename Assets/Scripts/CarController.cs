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
    public AudioSource driftSound;

    [Header("Engine sounds")]
    public AudioSource engineIdle;
    public AudioSource engineSlow;
    public AudioSource engineMid;
    public AudioSource engineFast;

    private TrackManager trackManager;

    private float velocityVsUp;
    private float rotationAngle;


    public float getVelocity()
    {
        return velocityVsUp;
    }

    public void setVelocity(float velocity)
    {
        velocityVsUp = velocity;
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
        if (trackManager != null && trackManager.isTrackStarted)
        {
            // Capture the initial rotation angle before steering
            float initialRotationAngle = rotationAngle;

            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();

            float rotationChange = Mathf.Abs(rotationAngle - initialRotationAngle);
            if (Mathf.Abs(steeringInput) > 0 && rotationChange > driftAngleThreshold)
            {
                SpawnDriftMark();
            }
        }

        
    }

    private void Update()
    {
        Debug.Log("Speed:" + velocityVsUp);
        if (velocityVsUp <= 0 && !engineIdle.isPlaying)
        {
            engineIdle.Play();
        }
        else if (velocityVsUp < maxSpeed / 3 && velocityVsUp > 0 && !engineSlow.isPlaying)
        {
            engineSlow.Play();
        }
        else if (velocityVsUp >= maxSpeed / 3 && velocityVsUp < maxSpeed * 2 / 3 && !engineMid.isPlaying)
        {
            engineMid.Play();
        }
        else if (velocityVsUp >= maxSpeed * 2 / 3 && !engineFast.isPlaying)
        {
            engineFast.Play();
        }
        else if (velocityVsUp < 0 && !engineIdle.isPlaying)
        {
            engineIdle.Play();
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
        if (!driftSound.isPlaying) 
        { 
            driftSound.Play(); 
        }
        
        Destroy(driftMark, driftMarkLifetime);
    }

    private void OnDisable()
    {
        var carInput = new CarInput();
        carInput.Car.Disable();
    }
}
