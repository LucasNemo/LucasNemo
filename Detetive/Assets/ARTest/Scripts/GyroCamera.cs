using UnityEngine;
using System.Collections;

public class GyroCamera : MonoBehaviour
{
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;

    void Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;
    }

    void Update()
    {
        ApplyGyroRotation();
        ApplyCalibration();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Calibrate", GUILayout.Width(300), GUILayout.Height(100)))
        {
            CalibrateYAngle();
        }
    }

    public void CalibrateYAngle()
    {
        // Offsets the y angle in case it wasn't 0 at edit time.
        calibrationYAngle = appliedGyroYAngle - initialYAngle; 
    }

    void ApplyGyroRotation()
    {
        transform.rotation = Input.gyro.attitude;
        // Swap "handedness" of quaternion from gyro.
        transform.Rotate(0f, 0f, 180f, Space.Self);
        // Rotate to make sense as a camera pointing out the back of your device.
        transform.Rotate(90f, 180f, 0f, Space.World);
        // Save the angle around y axis for use in calibration.
        appliedGyroYAngle = transform.eulerAngles.y; 
    }

    void ApplyCalibration()
    {
        // Rotates y angle back however much it deviated when calibrationYAngle was saved.
        transform.Rotate(0f, -calibrationYAngle, 0f, Space.World);
    }
}