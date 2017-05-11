using UnityEngine;
using System.Collections;

public class GyroCamera : MonoBehaviour
{
    private float initialYAngle = 0f;
    private float appliedGyroYAngle = 0f;
    private float calibrationYAngle = 0f;


    private Vector3 m_defeaultCameraAngles;

    private Vector3 offsetRotation;
    

    void Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        initialYAngle = transform.eulerAngles.y;

        //get the initial camera rotation - to be used as rrefernce.
        m_defeaultCameraAngles = transform.localRotation.eulerAngles;

        offsetRotation = Vector3.zero;

     
    }

    void Update()
    {
        bool isTouching = TouchToRotate();

        if (!isTouching)
            DeviceAccelerometerRotation();
    }

    private void DeviceAccelerometerRotation()
    {
        //todo - become a better person...know more math!!
        var supportedAttitude = Input.gyro.attitude;

        if (System.Math.Round(supportedAttitude.eulerAngles.magnitude, 2) == 0)
        { 
            verticalAngle += Input.gyro.rotationRate.y;
            verticalAngle %= 360f;

            horizontalAngle += Input.gyro.rotationRate.x;
            horizontalAngle %= 360f;

            transform.rotation = Quaternion.Euler(-horizontalAngle, -verticalAngle, 0);
        }
        else
        {
            var angle = supportedAttitude.eulerAngles;
            offsetRotation = new Vector3(horizontalAngle - angle.x, verticalAngle - angle.y);
            transform.rotation = Quaternion.Euler(new Vector3(-supportedAttitude.eulerAngles.x , -supportedAttitude.eulerAngles.y,0));
        }
    }

    float verticalAngle = 0;
    float horizontalAngle = 0;

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(Screen.width - 300, 0, 300, 150), "Calibrar AR"))
    //    {
           
    //    }

    //    GUI.skin.label.fontSize = 22;
    //    GUI.Label(new Rect(0, 0, 500, 300), GetGyroInfo());
    //    GUI.Label(new Rect(0, 300, 800, 1000), GetRotInfo());

    //}



    private string GetGyroInfo()
    {
        return string.Format(" attitude {0}  gravity {1} rotation {2} acceleration {3} RotationRate {4} ", 
            Input.gyro.attitude.ToString(), 
            Input.gyro.gravity.ToString(),
            Input.gyro.rotationRate.ToString(), 
            Input.gyro.userAcceleration.ToString(),
            Input.gyro.rotationRateUnbiased.ToString());
    }


    private string GetRotInfo()
    {
        return string.Format(" euler{2} X{0} Y{1}  offset {3}", verticalAngle, horizontalAngle,Input.gyro.attitude.eulerAngles, offsetRotation);
    }


    public void CalibrateYAngle()
    {
        verticalAngle = 0;
        horizontalAngle = 0;
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


    private Vector2 firstPoint, secondpoint;
    private float xAngTemp, xAngle, yAngTemp, yAngle;


    private bool TouchToRotate()
    {
        xAngle = transform.rotation.eulerAngles.y;
        yAngle = transform.rotation.eulerAngles.x;

        //Check count touches
        if (Input.touchCount > 0)
        {
            //Touch began, save position
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstPoint = Input.GetTouch(0).position;
                xAngTemp = xAngle;
                yAngTemp = yAngle;
            }
            //Move finger by screen
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                secondpoint = Input.GetTouch(0).position;
                //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                xAngle = xAngTemp + (secondpoint.x - firstPoint.x) * 360f / Screen.width;
                yAngle = yAngTemp - (secondpoint.y - firstPoint.y) * 360f / Screen.height;

                //Rotate camera
                transform.rotation = Quaternion.Euler((float)yAngle,(float) xAngle, 0.0f);
            }

            verticalAngle = (float)-xAngle;
            horizontalAngle = (float)-yAngle;

            return true;
        }
        
        return false;
    }
}