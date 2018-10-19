using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleEvent();
public delegate void SendTestEvent(Test test);
public delegate void SelectDropdownEvent(int index);

public static class AppEventManager{


    public static SimpleEvent OnAppStart;
    public static SimpleEvent OnAppClose;
    public static SimpleEvent OnMyTestClick;
    public static SimpleEvent OnGetPredictionClick;
    public static SimpleEvent OnHomeButtonClick;
    public static SimpleEvent OnFinishAddTestClick;
    public static SimpleEvent OnFinishGetPredictionClick;
    public static SimpleEvent OnAddTestClick;

    public static SendTestEvent OnTestDetailsClick;
    public static SendTestEvent OnRemoveTestButtonClick;

    public static SelectDropdownEvent OnRecommendedBehaviourSubjectDropdownSelect;

}
