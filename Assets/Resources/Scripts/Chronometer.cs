using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    private static Chronometer _instance;
    private float timeFrameWithScale = 0f;
    private float timeInSecondsToShow = 0f;
    private float scaleTimeToPause;
    private float scaleTimeInitial;
    public int initialYear = 2020;
    public int initialDay = 20;
    public int initialMonth = 2;
    public bool isPaused;
    private float currentTime;
    [Tooltip("Tiempo inicial en segundos")]
    public float timeInitial;

    [Tooltip("Escala del tiempo in game")]
    [Range(0.0f, 100.0f)]
    public float scaleTime = 25;

    private float _hourActual = -1;
    private float _minuteActual = -1;
    private float _secondActual = -1;
    private int _numDayActual = -1;
    private string _dayText;
    public Calendar calendar;
    public string DayText { get => _dayText; }
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        calendar = new Calendar(day : initialDay, month : initialMonth, year : initialYear);        
        _dayText = calendar.GetTimeInText(); 
    }
    public static Chronometer GetInstance()
    {
        return _instance;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeFrameWithScale = Time.deltaTime * scaleTime;
        timeInSecondsToShow = timeInSecondsToShow + timeFrameWithScale;
        updateChronometer(ref timeInSecondsToShow);

    }

    public void updateChronometer(ref float timeInSeconds)
    {
        int hour = 0;
        int minutes = 0;
        int sec = 0;
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        minutes = (int)timeInSeconds / 60;
        sec = (int)timeInSeconds % 60;
        if (minutes >= 60)
        {
            hour = minutes / 60;
            minutes = minutes % 60;
        }
        if (hour >= 24)
        {
            timeInSeconds = minutes * 60;
            hour = hour % 24;
            timeInSeconds = timeInSeconds + hour * 60 * 60;
            // Update the day
            calendar.UpdateTime();
            _dayText = calendar.GetTimeInText();
            // dayText.text = calendar.GetTimeInText();
        }

        this._hourActual = hour;
        this._minuteActual = minutes;
        this._secondActual = sec;
        // hourText.text = hour.ToString("00") + ":" + minutes.ToString("00") + ":" + sec.ToString("00");
        // hourText.text = hour.ToString("00") + ":" + minutes.ToString("00");
    }

    public string GetInformationHour()
    {
        return _hourActual.ToString("00") + ":" + _minuteActual.ToString("00");
    }

}
