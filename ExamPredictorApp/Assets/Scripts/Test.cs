using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test")]
public class Test: ScriptableObject {
    //TODO :remove scriptable object inheritance for release, chenga public to public readonly and create a contructor
    public int id;
    public float score;
    public float hoursSleep;
    public float hoursStudy;
    public float hoursTutition;
    public float sleepRange;
    public Subject subject;

    public Test()
    {
        id = 0;
        score = 50;
        hoursSleep = 8;
        hoursStudy = 3;
        hoursTutition = 1;
        sleepRange = 3;
    }

    public Test(int _id, float _score, float _hoursSleep, float _hoursStudy, float _hoursTuition, float _sleepRange, Subject _subject)
    {
        id = _id;
        score = _score;
        hoursSleep = _hoursSleep;
        hoursTutition = _hoursTuition;
        hoursStudy = _hoursStudy;
        sleepRange = _sleepRange;
        subject = _subject;
    }
}
