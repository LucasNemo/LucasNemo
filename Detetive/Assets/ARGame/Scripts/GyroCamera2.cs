
using UnityEngine;


public class GyroCamera2 : MonoBehaviour
{
    private float initialYAngle = 0f;
    private Gyroscope gyro;
    private Quaternion gyroAttitude;
    private bool gyroSupport;
    private bool gyroEnabled;

    // sensitivity vars
    public float speed = 4.0f;
    public float sensitivity = 0.8f;

    void Start()
    {
        gyroSupport = SystemInfo.supportsGyroscope;

        if (gyroSupport)
        {
            gyro = Input.gyro;
            gyroEnabled = true;
            gyro.enabled = true;
        }
        else
        {
            gyroEnabled = false;
            print("NO GYRO");
        }

        ApplyCalibration();
    }

    void Update()
    {
        if (gyroEnabled)
        {
            ApplyGyroRotation();
        }
    }

    void ApplyGyroRotation()
    {
        Quaternion newRotation = Quaternion.Slerp(gyroAttitude,
                                                  Input.gyro.attitude,
                                                  Time.deltaTime * speed * sensitivity);

        transform.localRotation = newRotation;

        transform.Rotate(30f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
        transform.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.

        // save the current gyro rotation for the next update
        gyroAttitude = newRotation;
    }

    void ApplyCalibration()
    {
        transform.localRotation = Input.gyro.attitude;
        transform.Rotate(30f, 0f, 180f, Space.Self); // Swap "handedness" of quaternion from gyro.
        transform.Rotate(90f, 180f, 0f, Space.World); // Rotate to make sense as a camera pointing out the back of your device.

        initialYAngle = transform.eulerAngles.y; // Save the angle around y axis in order to rotate the player accordingly
    }

    /* Used to get the original Y Angle in order to rotate the player
     * in the positive Z direction regardless of which direction the user 
     * was facing when they started playing
     */
    public float GetYAngle()
    {
        ApplyCalibration();

        return initialYAngle;
    }
}
