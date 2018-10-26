using MathNet.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test{
    public long id;
    public Grade score;
    public float hoursSleep;
    public float hoursStudy;
    public float hoursTuition;
    public float sleepRange;
    public Subject subject;

    public Test()
    {
        id = 0;
        score = Grade.A;
        hoursSleep = 8;
        hoursStudy = 3;
        hoursTuition = 1;
        sleepRange = 3;
        subject = Subject.Computing;
    }

    public Test(long _id, Grade _score, float _hoursSleep, float _hoursStudy, float _hoursTuition, float _sleepRange, Subject _subject)
    {
        id = _id;
        score = _score;
        hoursSleep = _hoursSleep;
        hoursTuition = _hoursTuition;
        hoursStudy = _hoursStudy;
        sleepRange = _sleepRange;
        subject = _subject;
    }

    public Test(string[] parameters)
    {
        id = long.Parse(parameters[0]);
        score = (Grade)int.Parse(parameters[1]);
        hoursSleep = float.Parse(parameters[2]);
        hoursStudy = float.Parse(parameters[3]);
        hoursTuition = float.Parse(parameters[4]);
        sleepRange = float.Parse(parameters[5]);
        subject = (Subject)int.Parse(parameters[6]);
    }

    public string GetParams()
    {
        return id.ToString() + ","
            + ((int)score).ToString() + ","
            + hoursSleep.ToString() + ","
            + hoursStudy.ToString() + ","
            + hoursTuition.ToString() + ","
            + sleepRange.ToString() + ","
            + ((int)subject).ToString();
    }

    public bool Equals(Test other)
    {
        if (other == null) return false;
        if (score != other.score) return false;
        if (hoursSleep != other.hoursSleep) return false;
        if (hoursStudy != other.hoursStudy) return false;
        if (hoursTuition != other.hoursTuition) return false;
        if (sleepRange != other.sleepRange) return false;
        if (subject != other.subject) return false;
        return true;
    }
}
