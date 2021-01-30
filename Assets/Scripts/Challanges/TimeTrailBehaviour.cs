using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrailBehaviour : MonoBehaviour
{
    [SerializeField] private float timeToEscapeSec = 180;
    [SerializeField] private PlayerStatus playerStatus;
    private float timer = 0;

    public float TimeLeft => Mathf.Abs(timeToEscapeSec - timer);
    public string TimeSec => (TimeLeft % 60 < 10 ? "0" : "") + Mathf.FloorToInt(TimeLeft % 60).ToString();
    public string TimerToText => string.Format("{0}:{1}", Mathf.FloorToInt(TimeLeft / 60), TimeSec);
    public bool Damaging => damageTimer > 0;

    [SerializeField] private float damageOverTime = 10;
    private float damageTimer = 0;

    private void Update()
    {
        DoTimer();
    }

    private void DoTimer()
    {
        timer += Time.deltaTime;
        if (timer > timeToEscapeSec)
        {
            DoDamage();
        }
    }

    private void DoDamage()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer > damageOverTime)
        {
            damageTimer = 0;
            playerStatus.Sanity -= 1;
        }
    }
}
