using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calendar
{
    private int _day;
    private int _month;
    private int _year;
    private int _maxDays;
    private DateTime _date;

    // Setter and Getter
    public int Day { get => _day; set => _day = value; }
    public int Year { get => _year; set => _year = value; }
    public int Month { get => _month; set => _month = value; }
    public int MaxDays { get => _maxDays; set => _maxDays = value; }
    public DateTime Date { get => _date; set => _date = value; }

    public Calendar(int day = 1, int month = 1, int year = 1990)
    {
        int cantDays = System.DateTime.DaysInMonth(year, month);
        _date = new DateTime(year, month, day);
        _maxDays = cantDays;
    }

    public string GetTimeInText()
    {
        string nameDay = _date.ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
        string nameMonth = _date.ToString("MMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
        nameDay = nameDay.Substring(0, 1).ToUpper() + nameDay.Substring(1);
        nameMonth = nameMonth.Substring(0, 1).ToUpper() + nameMonth.Substring(1);
        return $"{nameDay} {_date.Day.ToString()} \n{_date.Year.ToString()} \n{nameMonth}";
    }

    public void NextDay(int aditionalDays = 1)
    {
        _date = _date.AddDays(aditionalDays);
    }

    public void UpdateTime(int aditionalDays = 1)
    {
        NextDay(aditionalDays);
    }
    // get number day

    public int GetDay()
    {
        return _date.Day;
    }

    public int GetMonth()
    {
        return _date.Month;
    }

}
