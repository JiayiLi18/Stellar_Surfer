using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    public string portName = "COM3"; // Change this to the correct port for your Arduino
    public int baudRate = 9600;      // Match this to the baud rate in your Arduino code
    private SerialPort serialPort;   // Serial port object
    [SerializeField] float receivedValue = 0;   // Variable to store the received value
    public static float processedValue;

    void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate)
        {
            ReadTimeout = 50, // Short timeout to prevent blocking
        };
        try
        {
            serialPort.Open(); // Open the serial port
            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        // Read data from the serial port
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine(); // Read a line of data
                if (float.TryParse(data, out float value))
                {
                    receivedValue = value; // Parse and store the integer value
                    ComputingValue();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Error reading from serial port: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        // Close the serial port when the application quits
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed.");
        }
    }

    public void ComputingValue()
    {
        if (receivedValue <= 0.05f && receivedValue >= -0.05f)
        {
            processedValue = 0;
        }
        else
        {
            processedValue = Mathf.Clamp(receivedValue, -0.3f, 0.3f);
        }
    }
}
