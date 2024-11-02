using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private TrackManager trackManager;
    [SerializeField] private ScreenShake screenshakeManager;

    [Header("Car Parameters")]
    private GameObject car;
    [SerializeField] private TMP_Text maxSpeedPlaceholderText;
    [SerializeField] private TMP_Text accelerationPlaceholderText;
    [SerializeField] private TMP_Text maxBackspeedMultiplierPlaceholderText;
    [SerializeField] private TMP_Text turnFactorPlaceholderText;
    [SerializeField] private TMP_Text driftFactorPlaceholderText;
    [SerializeField] private TMP_Text driftMarkLifetimePlaceholderText;
    [SerializeField] private TMP_Text bouncinessPlaceholderText;

    [Header("Track Parameters")]
    [SerializeField] private TMP_Text numberTotalLapsPlaceholderText;

    [Header("Screen Shake Parameters")]
    [SerializeField] private TMP_Text shakeDurationPlaceholderText;
    [SerializeField] private TMP_Text baseShakeMagnitudePlaceholderText;
    [SerializeField] private TMP_Text speedToShakeMultiplierPlaceholderText;

    public void ReadMaxSpeedStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().maxSpeed = value/3.6f;
        maxSpeedPlaceholderText.text = value.ToString();
    }

    public void ReadAccelerationStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().accelerationFactor = value;
        accelerationPlaceholderText.text = value.ToString();
    }

    public void ReadMaxBackspeedMultiplierStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().backwardSpeedFactor = value;
        maxBackspeedMultiplierPlaceholderText.text = value.ToString();
    }

    public void ReadTurnFactorStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().turnFactor = value;
        turnFactorPlaceholderText.text = value.ToString();
    }

    public void ReadDriftFactorStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().driftFactor = value;
        driftFactorPlaceholderText.text = value.ToString();
    }

    public void ReadDriftMarkLifetimeStringInput(string input)
    {
        float value = float.Parse(input);
        car.GetComponent<CarController>().driftMarkLifetime = value;
        driftMarkLifetimePlaceholderText.text = value.ToString();
    }

    public void ReadBouncinessStringInput(string input)
    {
        /*float value = float.Parse(input);
        car.GetComponent<CarController>().bounciness = value;
        bouncinessPlaceholderText.text = value.ToString();*/
    }

    public void ReadNumberTotalLapsStringInput(string input)
    {
        int value = int.Parse(input);
        trackManager.numberOfLapsTotal = value;
        numberTotalLapsPlaceholderText.text = value.ToString();
    }

    public void ReadShakeDurationStringInput(string input)
    {
        float value = float.Parse(input);
        screenshakeManager.baseShakeDuration = value;
        shakeDurationPlaceholderText.text = value.ToString();
    }

    public void ReadBaseShakeMagnitudeStringInput(string input)
    {
        float value = float.Parse(input);
        screenshakeManager.baseShakeMagnitude = value;
        baseShakeMagnitudePlaceholderText.text = value.ToString();
    }

    public void ReadSpeedToShakeMultiplierStringInput(string input)
    {
        float value = float.Parse(input);
        screenshakeManager.speedToShakeMultiplier = value;
        speedToShakeMultiplierPlaceholderText.text = value.ToString();
    }

    public void Start()
    {
        if (car == null) car = GameObject.FindGameObjectWithTag("Player");

        maxSpeedPlaceholderText.text = (car.GetComponent<CarController>().maxSpeed*3.6).ToString();
        accelerationPlaceholderText.text = car.GetComponent<CarController>().accelerationFactor.ToString();
        maxBackspeedMultiplierPlaceholderText.text = car.GetComponent<CarController>().backwardSpeedFactor.ToString();
        turnFactorPlaceholderText.text = car.GetComponent<CarController>().turnFactor.ToString();
        driftFactorPlaceholderText.text = car.GetComponent<CarController>().driftFactor.ToString();
        driftMarkLifetimePlaceholderText.text = car.GetComponent<CarController>().driftMarkLifetime.ToString();
        //bouncinessPlaceholderText.text = car.GetComponent<CarController>().bounciness.ToString();

        numberTotalLapsPlaceholderText.text = trackManager.numberOfLapsTotal.ToString();

        shakeDurationPlaceholderText.text = screenshakeManager.baseShakeDuration.ToString();
        baseShakeMagnitudePlaceholderText.text = screenshakeManager.baseShakeMagnitude.ToString();
        speedToShakeMultiplierPlaceholderText.text = screenshakeManager.speedToShakeMultiplier.ToString();
    }

}
