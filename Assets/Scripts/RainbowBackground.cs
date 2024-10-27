using UnityEngine;
using UnityEngine.UI;

public class RainbowBackground : MonoBehaviour
{
    [SerializeField] private Image endBackgroundImage;
    [SerializeField] private float colorChangeSpeed = 0.3f;
    [SerializeField] private float darkness = 0.3f;
    [SerializeField] private float transparency = 0.99f;

    void Update()
    {
        // Smoothly transition hue value using PingPong for back-and-forth effect
        float hueValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1f);

        // Convert hue to RGB, using 'darkness' for brightness
        Color color = Color.HSVToRGB(hueValue, 1f, darkness);
        color.a = transparency; // Apply transparency

        // Set color to background image
        endBackgroundImage.color = color;
    }
}
