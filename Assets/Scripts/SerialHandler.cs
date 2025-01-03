using System;
using System.Globalization;
using System.IO.Ports;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{
    private SerialPort _serial;

    // Common default serial device on a Windows machine
    [SerializeField] private string serialPort = "COM1";
    [SerializeField] private int baudrate = 115200;

    [SerializeField] private TrackManager trackManager;
    [SerializeField] private MovingPlatform movingPlatform;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        _serial = new SerialPort(serialPort, baudrate);
        // Guarantee that the newline is common across environments.
        _serial.NewLine = "\n";
        // Once configured, the serial communication must be opened just like a file : the OS handles the communication.
        _serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // Return early if not open, prevent spamming errors for no reason.
        if (!_serial.IsOpen) return;
        // Prevent blocking if no message is available as we are not doing anything else
        // Alternative solutions : set a timeout, read messages in another thread, coroutines, futures...
        if (_serial.BytesToRead <= 0) return;

        // Trim leading and trailing whitespaces, makes it easier to handle different line endings.
        // Arduino uses \r\n by default with `.println()`.
        var message = _serial.ReadLine().Trim();

        // Split the message on spaces, in case we want to pass a value as well.
        var messageParts = message.Split(' ');
        switch (messageParts[0])
        {
            case "button":
                trackManager.Shockwave();
                break;
            case "x":
                x = float.Parse(messageParts[1], CultureInfo.InvariantCulture);
                break;

            case "y":
                y = float.Parse(messageParts[1], CultureInfo.InvariantCulture);
                break;
            default:
                Debug.Log($"Unknown message: {message}");
                break;
        }
        movingPlatform.Moving(x, y);
    }

    public void SetGreenLed(bool newState)
    {
        if (!_serial.IsOpen) return;
        _serial.WriteLine(newState ? "GREEN LED ON" : "GREEN LED OFF");
    }

    public void SetOrangeLed(bool newState)
    {
        if (!_serial.IsOpen) return;
        _serial.WriteLine(newState ? "ORANGE LED ON" : "ORANGE LED OFF");
    }

    public void SetRedLed(bool newState)
    {
        if (!_serial.IsOpen) return;
        _serial.WriteLine(newState ? "RED LED ON" : "RED LED OFF");
    }

    public void SetSpeed(float speed)
    {
        if (!_serial.IsOpen) return;
        string speedMessage = $"speed {speed:F2}\n";
        _serial.Write(speedMessage);
    }


    private void OnDestroy()
    {
        if (!_serial.IsOpen) return;
        _serial.Close();
    }
}
