using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;

    void Update(){
        var time = DateTime.Now;
        hoursPivot.localRotation = 
            Quaternion.Euler(0f, 0f, (hoursToDegrees * time.Hour) - (time.Minute/2)); //makes the hour hand every minute as well. Every 2 minutes, add 1 degree to the hour degree
        minutesPivot.localRotation = 
            Quaternion.Euler(0f, 0f, minutesToDegrees * time.Minute);
        secondsPivot.localRotation = 
            Quaternion.Euler(0f, 0f, secondsToDegrees * time.Second);
    }
}
