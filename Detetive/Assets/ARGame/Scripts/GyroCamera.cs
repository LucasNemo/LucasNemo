using UnityEngine;
using System.Collections;

public class GyroCamera : MonoBehaviour
{
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;


    private Vector3 m_defeaultCameraAngles;

    

    void Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;

        //get the initial camera rotation - to be used as rrefernce.
        m_defeaultCameraAngles = transform.localRotation.eulerAngles;
    }

    void Update()
    {

        verticalAngle += Input.gyro.rotationRate.y;
        verticalAngle %= 360f;

        horizontalAngle += Input.gyro.rotationRate.x;
        horizontalAngle %= 360f;

        transform.rotation = Quaternion.Euler(-horizontalAngle, -verticalAngle, transform.rotation.z);

        
    }
    float verticalAngle = 0;
    float horizontalAngle = 0;

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 300, 0, 300, 150), "Calibrar AR"))
        {
            verticalAngle = 0;
            horizontalAngle = 0;
        }

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


        transform.rotation =  Quaternion.LookRotation(Input.acceleration);

        //Apply the device rotation to camera

        //var gyro = Input.gyro.rotationRate;

        //print(GetGyroInfo());

        //var degreeAngle = (gyro * Mathf.Rad2Deg).normalized;

        //print(degreeAngle);

        //transform.Rotate(new Vector3(-degreeAngle.x, -degreeAngle.y, 0) );


        //var attitude = Input.gyro.attitude;

        ////The phone does not support local space coordinates

        //if (attitude.eulerAngles.magnitude == 0)
        //{
        //    var rot = Input.gyro.rotationRate;
        //    transform.Rotate(new Vector3(-rot.x, -rot.y, 0f) * 2f);  // GyroToUnity(Input.gyro.attitude);
        //    var r = transform.rotation.eulerAngles;
        //    transform.rotation = Quaternion.Euler(r.x, r.y, 0);
        //}
        //else
        //{
        //    transform.rotation = GyroToUnity(Input.gyro.attitude);
        //}

        
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