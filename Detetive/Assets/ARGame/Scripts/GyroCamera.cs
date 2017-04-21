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
        GyroModifyCamera();

        //ApplyGyroRotation();
        //ApplyCalibration();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Calibrate", GUILayout.Width(300), GUILayout.Height(100)))
        {
            Input.gyro.enabled = !Input.gyro.enabled;
            //CalibrateYAngle();
        }

        GUI.Label(new Rect(0, 0, 1000, 100), GetGyroInfo());
    }

    private string GetGyroInfo()
    {
        return string.Format(" attitude {0}  gravity {1} rotation {2} acceleration {3}  ", Input.gyro.attitude.ToString(), Input.gyro.gravity.ToString(), Input.gyro.rotationRate.ToString(), Input.gyro.userAcceleration.ToString());
    }

    public void CalibrateYAngle()
    {
        // Offsets the y angle in case it wasn't 0 at edit time.
        calibrationYAngle = appliedGyroYAngle - initialYAngle; 
    }


    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {

        var attitude = Input.gyro.attitude;

        if (attitude.eulerAngles.magnitude == 0)
        {
            var rot = Input.gyro.rotationRate;
            transform.Rotate(new Vector3(-rot.x, -rot.y, 0f) * 2f);  // GyroToUnity(Input.gyro.attitude);
            var r = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(r.x, r.y, 0);
        }
        else
        {
            transform.rotation = GyroToUnity(Input.gyro.attitude);
        }

        
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
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