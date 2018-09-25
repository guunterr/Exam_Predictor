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
}
