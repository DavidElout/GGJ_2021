using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTrailUI : MonoBehaviour
{
    [SerializeField] private TimeTrailBehaviour timeTrial;
    [SerializeField] private TextMeshProUGUI timerText;


    public void Update()
    {
        timerText.text = timeTrial.TimerToText;
        timerText.color = timeTrial.Damaging ? Color.red : Color.white;
    }
}
