using System;
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

    void Update()
    {
        if (_serial.BytesToRead > 2)
        { // Check if we have at least a header
            byte[] header = new byte[2];
            _serial.Read(header, 0, 2);

            byte type = header[0];
            byte length = header[1];

            if (_serial.BytesToRead >= length)
            { // Ensure full payload is available
                byte[] payload = new byte[length];
                _serial.Read(payload, 0, length);

                HandleMessage(type, payload);
            }
        }
    }

    private void HandleMessage(byte type, byte[] payload)
    {
        switch (type)
        {
            case 1: // Joystick data
                float x = BitConverter.ToSingle(payload, 0);
                float y = BitConverter.ToSingle(payload, 4);
                //Debug.Log($"Joystick: X={x}, Y={y}");
                if (x > 0.1f || x < -0.1f || y > 0.1f || y < -0.1f)
                    movingPlatform.Moving(x, y);
                break;

            case 2: // Button press
                //Debug.Log("Button Pressed");
                trackManager.Shockwave();
                break;

            default:
                //Debug.LogWarning($"Unknown message type: {type}");
                break;
        }
    }

    public void SendLEDCommand(int ledCommand)
    {
        byte[] message = new byte[] { 3, 1, (byte)ledCommand }; // Type 3, length 1
        _serial.Write(message, 0, message.Length);
    }

    public void SendSpeed(float speed)
    {
        byte[] speedBytes = BitConverter.GetBytes((int)speed);
        byte[] message = new byte[6]; // Header (2 bytes) + payload (4 bytes)

        message[0] = 4; // Type 4 for speed
        message[1] = 4; // Length of the payload (4 bytes for a float)

        Array.Copy(speedBytes, 0, message, 2, speedBytes.Length);

        _serial.Write(message, 0, message.Length);
    }


    private void OnDestroy()
    {
        if (_serial != null && _serial.IsOpen)
        {
            _serial.Close();
        }
    }
}
