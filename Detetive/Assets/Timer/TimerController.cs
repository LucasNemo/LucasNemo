using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {


    public UnityEngine.UI.Text timeLabel;

    public IEnumerator StartTimer(float time, System.Action ontimerCompleted)
    {

        do
        {
            time -= Time.deltaTime;
            time = (float)System.Math.Round(time, 2);
            timeLabel.text =  ((int)time).ToString() + "s";
            yield return null;
        } while (time > 0);

        time = 0;


        ontimerCompleted();
    }

}
